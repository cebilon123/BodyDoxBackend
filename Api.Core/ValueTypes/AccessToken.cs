using System;

namespace Api.Core.ValueTypes
{
    public class AccessToken : IEquatable<AccessToken>
    {
        public string Value { get; }

        public AccessToken(string value)
        {
            Value = value;
        }

        public bool Equals(AccessToken other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as AccessToken);
        }

        public override int GetHashCode()
        {
            return !string.IsNullOrEmpty(Value) ? Value.GetHashCode() : 0;
        }

        public static implicit operator string(AccessToken token)
        {
            return token.Value;
        }

        public static implicit operator AccessToken(string stringToken)
        {
            return new(stringToken);
        }
    }
}