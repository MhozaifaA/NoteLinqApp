using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Infrastructure.EntityFramework.Builder
{
    public static class ValueConversionExtensions
    {
        //public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> propertyBuilder) where T : class, new()
        //{
        //    ValueConverter<T, string> converter = new ValueConverter<T, string>
        //    (
        //        v => System.Text.Json.JsonSerializer.Serialize<T>(v, null),
        //        v => System.Text.Json.JsonSerializer.Deserialize<T>(v, null) ?? new T()
        //    );

        //    ValueComparer<T> comparer = new ValueComparer<T>
        //    (
        //        (l, r) => System.Text.Json.JsonSerializer.Serialize<T>(l, null) == System.Text.Json.JsonSerializer.Serialize<T>(r, null),
        //        v => v == null ? 0 : System.Text.Json.JsonSerializer.Serialize<T>(v, null).GetHashCode(),
        //        v => System.Text.Json.JsonSerializer.Deserialize<T>(System.Text.Json.JsonSerializer.Serialize<T>(v, null), null)
        //    );

        //    propertyBuilder.HasConversion(converter);
        //    propertyBuilder.Metadata.SetValueConverter(converter);
        //    propertyBuilder.Metadata.SetValueComparer(comparer);
        //    propertyBuilder.HasColumnType("NVARCHAR(MAX)");

        //    return propertyBuilder;
        //}

        public static PropertyBuilder HasJsonConversion<T>(this PropertyBuilder propertyBuilder) where T : class, new()
        {
            //ValueConverter<T, string> converter = new ValueConverter<T, string>
            //(
            //    v => System.Text.Json.JsonSerializer.Serialize<T>(v),
            //    v => System.Text.Json.JsonSerializer.Deserialize<T>(v) ?? new T()
            //);

            //ValueComparer<T> comparer = new ValueComparer<T>
            //(
            //    (l, r) => System.Text.Json.JsonSerializer.Serialize(l) == System.Text.Json.JsonSerializer.Serialize(r),
            //    v => v == null ? 0 : System.Text.Json.JsonSerializer.Serialize(v).GetHashCode(),
            //    v => System.Text.Json.JsonSerializer.Deserialize<T>(System.Text.Json.JsonSerializer.Serialize(v))
            //);

            //propertyBuilder.HasConversion(converter);
            //propertyBuilder.Metadata.SetValueConverter(converter);
            //propertyBuilder.Metadata.SetValueComparer(comparer);
            //propertyBuilder.HasColumnType("NVARCHAR(MAX)");

            return propertyBuilder;
        }

    }
}
