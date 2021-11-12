using System;

namespace Api.Core.ValueTypes
{
    public class Longitude : IEquatable<Longitude>
    {
        public double Value { get; }

        public Longitude(double value)
        {
            Value = value;
        }

        public bool Equals(Longitude other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other;
        }

        public static implicit operator double(Longitude latitude)
        {
            return latitude.Value;
        }

        public static implicit operator Longitude(double value)
        {
            return new(value);
        }

        public bool IsValid()
        {
            return this >= -180 && this <= 180;
        }
    }
}