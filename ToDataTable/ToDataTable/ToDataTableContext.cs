using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ToDataTable.Tests")]
namespace ToDataTable
{
    public sealed class ToDataTableContext : IToDataTableContext
    {
        private static readonly Lazy<ToDataTableContext>
            Lazy =
                new Lazy<ToDataTableContext>
                    (() => new ToDataTableContext());

        private readonly ConcurrentDictionary<Type, IEnumerable<DataRowBuilder>> _dataRowBuilderCollectionDictionary =
            new ConcurrentDictionary<Type, IEnumerable<DataRowBuilder>>();

        private ToDataTableContext()
        {
        }

        public IEnumerable<Type> CachedTypes => _dataRowBuilderCollectionDictionary.Keys;

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
            var type = typeof(T);
            if (_dataRowBuilderCollectionDictionary.ContainsKey(type))
            {
                return _dataRowBuilderCollectionDictionary[type];
            }
            var dataRowBuilders = CreateDataRowBuilderFromPropertyDescriptorCollection(collection, type);
            _dataRowBuilderCollectionDictionary.TryAdd(type, dataRowBuilders);
            return dataRowBuilders;
        }

        internal DataRowBuilder CreateDataRowBuilderFromPropertyDescriptor(PropertyDescriptor prop, Type type)
        {
            var name = prop.Name;
            var propertyType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            var propertyInfo = type.GetProperty(name);
            return new DataRowBuilder
            {
                Getter = BuildAccessor(propertyInfo.GetGetMethod()),
                Name = name,
                Type = propertyType
            };
        }

        internal IEnumerable<DataRowBuilder> CreateDataRowBuilderFromPropertyDescriptorCollection(
            IEnumerable collection, Type type)
        {
            return (collection.Cast<PropertyDescriptor>()
                .Select(prop => CreateDataRowBuilderFromPropertyDescriptor(prop, type))).ToList();
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
            _dataRowBuilderCollectionDictionary.TryAdd(type, dict);
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