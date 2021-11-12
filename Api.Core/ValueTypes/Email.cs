using System;

namespace Api.Core.ValueTypes
{
    /// <summary>
    /// ValueType
    /// </summary>
    public class Email : IEquatable<Email>
    {
        public string Value { get; }

        public Email(string value)
        {
            Value = value;
        }

        public bool Equals(Email other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Email);
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }

        public static implicit operator Email(string email)
        {
            return new(email);
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