﻿using Ddd.ValueObjects;

namespace MyReleases.Domain.Shared
{
    public class Description : StringValueObject
    {
        protected Description(string value) : base(value)
        {
        }

        public static new Description Create(string value)
        {
            return new Description(value);
        }
    }
}
