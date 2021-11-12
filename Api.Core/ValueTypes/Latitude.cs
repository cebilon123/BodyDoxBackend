using System;

namespace Api.Core.ValueTypes
{
    public class Latitude : IEquatable<Latitude>
    {
        public double Value { get; }

        public Latitude(double value)
        {
            Value = value;
        }

        public bool Equals(Latitude other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Latitude);
        }

        public static implicit operator double(Latitude latitude)
        {
            return latitude.Value;
        }

        public static implicit operator Latitude(double value)
        {
            return new(value);
        }

        public bool IsValid()
        {
            return this >= -90 && this <= 90;
        }
    }
}