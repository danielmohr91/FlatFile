﻿using System;
using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TypeConverters
{
    public class BooleanTypeConverter : ITypeConverter<bool>
    {
        public bool ConvertFromString(string stringValue)
        {
            try
            {
                return bool.Parse(stringValue.Trim());
            }
            catch (Exception e)
            {
                throw new ArgumentException("Input must be true / false (case insensitive)", nameof(stringValue), e);
            }
        }
    }
}