using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace ToDataTable
{
    public sealed class ToDataTableContext : IToDataTableContext
    {
        private static readonly Lazy<ToDataTableContext>
            lazy =
                new Lazy<ToDataTableContext>
                    (() => new ToDataTableContext());

        public static ToDataTableContext Instance { get { return lazy.Value; } }

        private readonly IDictionary<Type, IEnumerable<DataRowBuilder>> _dataRowBuilderCollectionDictionary =
            new Dictionary<Type, IEnumerable<DataRowBuilder>>();

        private ToDataTableContext()
        {
        }

        public IEnumerable<DataRowBuilder> GetDataRowBuilders<T>()
        {
            var type = typeof(T);
            if (_dataRowBuilderCollectionDictionary.ContainsKey(type))
            {
                return _dataRowBuilderCollectionDictionary[type];
            }

            return null;
        }

        private IEnumerable<DataRowBuilder> CreateDataRowBuilderFromPropertyDescriptorCollection(
            PropertyDescriptorCollection collection, Type type)
        {
            var list = new List<DataRowBuilder>();
            foreach (PropertyDescriptor prop in collection)
            {
                var name = prop.Name;
                var propertyType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                var propertyInfo = type.GetProperty(name);
                list.Add(new DataRowBuilder
                {
                    Getter = BuildAccessor(propertyInfo.GetGetMethod()),
                    Name = name,
                    Type = propertyType
                });
            }

            return list;
        }
        
        private static Func<object, object> BuildAccessor(MethodInfo method)
        {
            ParameterExpression obj = Expression.Parameter(typeof(object), "obj");

            Expression<Func<object, object>> expr =
                Expression.Lambda<Func<object, object>>(
                    Expression.Convert(
                        Expression.Call(
                            Expression.Convert(obj, method.DeclaringType),
                            method),
                        typeof(object)),
                    obj);

            return expr.Compile();
        }

        public IEnumerable<DataRowBuilder> SetDataRowBuilders<T>(PropertyDescriptorCollection collection)
        {
            var dict = CreateDataRowBuilderFromPropertyDescriptorCollection(collection, typeof(T));
            _dataRowBuilderCollectionDictionary.Add(typeof(T), dict);
            return dict;
        }
    }
}