using Meteors.AspNetCore.Core.DataStructures;
using Meteors.AspNetCore.Infrastructure.ModelEntity.Interface;
using Meteors.AspNetCore.Infrastructure.ModelEntity.Localization;
using Meteors.AspNetCore.Localization.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Infrastructure.InfraExtensions
{
    public static class TranslateLanguagesExtensions
    {

        /// <summary>
        /// Key :  Prop name , Value: (value , info) 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Dictionary<string, (string value,ITranslateAttribute info)> GetTranslatePropertiesInfo<TEntity>(this TEntity entity) where TEntity : class
        {
            Dictionary<string, (string value, ITranslateAttribute info)> value = new();
            Type entotyType = typeof(TEntity);
            Type aentotyType = typeof(TranslateAttribute);
            foreach (var prop in entotyType.GetProperties(System.Reflection.BindingFlags.Public
                    | BindingFlags.Instance
                    | BindingFlags.DeclaredOnly))
            {
                object _atr = prop.GetCustomAttributes(false).FirstOrDefault(atr => atr is TranslateAttribute);
                if(_atr is ITranslateAttribute info)
                    value.Add(prop.Name, (prop.GetGetMethod().Invoke(entity, null)?.ToString() ?? "", info));
            }
            return value;
        }

        public static Dictionary<string,string> GetTranslateProperties<TEntity>(this TEntity entity) where TEntity : class
        {
            Dictionary<string, string> value = new();
            Type entotyType = typeof(TEntity);
            Type aentotyType = typeof(TranslateAttribute);
            foreach (var prop in entotyType.GetProperties(System.Reflection.BindingFlags.Public
                    | BindingFlags.Instance
                    | BindingFlags.DeclaredOnly))
            {
                if (prop.IsDefined(aentotyType, false))
                    value.Add(prop.Name, prop.GetGetMethod().Invoke(entity, null)?.ToString() ?? "");

            }
            return value;
        }

        public static Dictionary<string,string[]> GetCultureLanguagesProperty<TEntity>(this TEntity entity) where TEntity : class
        {
            Dictionary<string, string[]> value = new();
            Type entitytype = typeof(TEntity);
            if (entitytype.GetInterfaces().Contains(typeof(ILanguages)))
            {
                foreach (var prop in entitytype.GetProperties())
                {
                    foreach (var _atr in prop.GetCustomAttributes(false))
                    {
                        if (_atr is TranslateAttribute atr)
                            value.Add(prop.Name, atr.GetCodes);
                    }
                }
            }
            return value;
        }
    }
}
