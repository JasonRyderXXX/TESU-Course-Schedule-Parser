using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using TESUParser.Enums;

namespace TESUParser
{
    public static class EnumExtensions
    {
        public static T ParseEnum<T>(this string value, T defaultValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type");
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            foreach (T item in Enum.GetValues(typeof(T)))
            {
                if (item.ToString().ToLower().Equals(value.Trim().ToLower()))
                    return item;
                if (item.ParseEnumDescription<T>().ToLower() == value.Trim().ToLower())
                    return item;
            }
            return defaultValue;
        }
        public static DateTime CalculateDateTime(this DateTime datetime, DOWEnum dayOfWeek, bool IsEOD) =>
            (IsEOD) ?
            datetime.Date.AddDays((int)dayOfWeek).AddHours(23).AddMinutes(59).AddSeconds(59)
            :
            datetime.Date.AddDays((int)dayOfWeek).AddHours(0).AddMinutes(0).AddSeconds(0);
        public static int ParseIntWithDefault(this string inputvalue, int defaultvalue = 12)
        {
            int result;
            if (!int.TryParse(inputvalue, out result))
                result = defaultvalue;
            return result;
        }
        public static string ParseEnumDescription<t>(this t value) where t : struct, IConvertible
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

    }
}
