using System;

namespace Api.Core.ValueTypes
{
    public class RefreshToken : IEquatable<RefreshToken>
    {
        public string Value { get; }

        public RefreshToken(string value)
        {
            Value = value;
        }

        public bool Equals(RefreshToken other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RefreshToken);
        }

        public override int GetHashCode()
        {
            return !string.IsNullOrEmpty(Value) ? Value.GetHashCode() : 0;
        }

        public static implicit operator string(RefreshToken token)
        {
            return token.Value;
        }

        public static implicit operator RefreshToken(string stringToken)
        {
            return new(stringToken);
        }
    }
}