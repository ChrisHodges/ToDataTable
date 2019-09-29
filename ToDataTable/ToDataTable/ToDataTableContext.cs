using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ToDataTable
{
    public sealed class ToDataTableContext : IToDataTableContext
    {
        private static readonly Lazy<ToDataTableContext>
            Lazy =
                new Lazy<ToDataTableContext>
                    (() => new ToDataTableContext());

        private readonly IDictionary<Type, IEnumerable<DataRowBuilder>> _dataRowBuilderCollectionDictionary =
            new Dictionary<Type, IEnumerable<DataRowBuilder>>();

        private ToDataTableContext()
        {
        }

        public static ToDataTableContext Instance => Lazy.Value;

        IEnumerable<DataRowBuilder> IToDataTableContext.GetDataRowBuilders<T>()
        {
            var type = typeof(T);
            return _dataRowBuilderCollectionDictionary.ContainsKey(type)
                ? _dataRowBuilderCollectionDictionary[type]
                : null;
        }

        IEnumerable<DataRowBuilder> IToDataTableContext.SetDataRowBuilders<T>(PropertyDescriptorCollection collection)
        {
            var dataRowBuilders = CreateDataRowBuilderFromPropertyDescriptorCollection(collection, typeof(T));
            _dataRowBuilderCollectionDictionary.Add(typeof(T), dataRowBuilders);
            return dataRowBuilders;
        }

        private IEnumerable<DataRowBuilder> CreateDataRowBuilderFromPropertyDescriptorCollection(
            IEnumerable collection, Type type)
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
            var obj = Expression.Parameter(typeof(object), "obj");

            var expr =
                Expression.Lambda<Func<object, object>>(
                    Expression.Convert(
                        Expression.Call(
                            Expression.Convert(obj, method.DeclaringType),
                            method),
                        typeof(object)),
                    obj);

            return expr.Compile();
        }

        public void SetDataRowBuilders(PropertyDescriptorCollection collection, Type type)
        {
            var dict = CreateDataRowBuilderFromPropertyDescriptorCollection(collection, type);
            _dataRowBuilderCollectionDictionary.Add(type, dict);
        }

        public static void PrecompileMaps(Assembly assembly)
        {
            var types = assembly.GetTypes().Where(x =>
                x.GetCustomAttributes().Any(y => y.GetType() == typeof(PrecompileDataTableMapAttribute)));
            foreach (var type in types)
            {
                var descriptionCollection = TypeDescriptor.GetProperties(type);
                Instance.SetDataRowBuilders(descriptionCollection, type);
            }
        }
    }
}