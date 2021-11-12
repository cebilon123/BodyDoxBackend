using System;

namespace Api.Core.ValueTypes
{
    /// <summary>
    /// ValueType. Implicitly converted is only value of this object. Salt need to be set or get by property.
    /// </summary>
    public class Password : IEquatable<Password>
    {
        public string Value { get; }

        public Password(string value)
        {
            Value = value;
        }

        public bool Equals(Password other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Password);
        }

        public static implicit operator string(Password password)
        {
            return password.Value;
        }

        public static implicit operator Password(string password)
        {
            return new(password);
        }

        public override int GetHashCode()
        {
            return !string.IsNullOrEmpty(Value) ? Value.GetHashCode() : 0;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}