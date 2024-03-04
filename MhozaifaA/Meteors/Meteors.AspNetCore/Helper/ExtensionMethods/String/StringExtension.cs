using Meteors.AspNetCore.Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json;

namespace Meteors.AspNetCore.Helper.ExtensionMethods.String
{
    /// <summary>
    /// Basics extensions
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Indicates whether the specified string is null or an empty string ("").
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
            => System.String.IsNullOrEmpty(value);


        /// <summary>
        /// Indicates whether the specified string is not null or an not empty string ("").
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string value)
            => !System.String.IsNullOrEmpty(value);


        /// <summary>
        /// Converts the string representation of <see cref="{T}"/>
        /// enumerated constants to an equivalent enumerated object. A parameter specifies
        /// whether the operation is case-insensitive.
        /// <para>ignoreCase is auto active <see langword="true"/> .</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value)
            => (T)System.Enum.Parse(typeof(T), value, true);


        public static string IntoParentheses(params string[] vector)
          => "("+ System.String.Join(FixedCommonValue.Comma, vector) +")";

        public static string IntoParentheses(IEnumerable<string> vector)
       => "(" + System.String.Join(FixedCommonValue.Comma, vector) + ")";

        public static string IntoNewLines(this IEnumerable<string?> vector)
             =>  System.String.Join(Environment.NewLine, vector);



        public static T ChangeType<T>(this string value)
        {
            if (typeof(T) == typeof(Guid))
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(value);
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static T Deserialize<T>(this string value)
            => JsonSerializer.Deserialize<T>(value);
    }
}
