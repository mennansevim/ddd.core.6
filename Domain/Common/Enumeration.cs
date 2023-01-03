using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Domain.Common
{
    public abstract class Enumeration<TValue> : IComparable
        where TValue : IComparable
    {
        public TValue Value { get; }

        protected Enumeration(TValue value)
        {
            Value = value;
        }

        public override string ToString() => Value.ToString();

        public static IEnumerable<T> GetAll<T>() where T : Enumeration<TValue>
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Enumeration<TValue> otherValue))
                return false;

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static T FromValue<T>(TValue value) where T : Enumeration<TValue>
        {
            var matchingItem = Parse<T>(value, item => item.Value.Equals(value));
            return matchingItem;
        }

        private static T Parse<T>(TValue value, Func<T, bool> predicate) where T : Enumeration<TValue>
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid in {typeof(T)}");

            return matchingItem;
        }

        public int CompareTo(object other) => Value.CompareTo(((Enumeration<TValue>) other).Value);
    }
}