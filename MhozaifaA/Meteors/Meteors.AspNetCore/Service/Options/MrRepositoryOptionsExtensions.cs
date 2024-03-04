using Meteors.AspNetCore.Core.Shared;
using Meteors.AspNetCore.Helper.ExtensionMethods.Object;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Service.Options
{
    public static class MrRepositoryOptionsExtensions
    {
        public static void Sort(this MrRepositoryOptions MrRepositoryOptions, Sort sort = Options.Sort.Ascending)
        => MrRepositoryOptions.SortOptions.Sort = sort;

        public static MrRepositoryOptions OrderBy(this MrRepositoryOptions MrRepositoryOptions, string orderByColumn)
        {
            MrRepositoryOptions.Sort();
            MrRepositoryOptions.SortOptions.OrderByColumn = orderByColumn;
            return MrRepositoryOptions;
        }

        public static MrRepositoryOptions OrderByDescending(this MrRepositoryOptions MrRepositoryOptions, string orderByColumn)
        {
            MrRepositoryOptions.Sort(Options.Sort.Descending);
            MrRepositoryOptions.SortOptions.OrderByColumn = orderByColumn;
            return MrRepositoryOptions;
        }

        public static void ThenBy(this MrRepositoryOptions MrRepositoryOptions, string thenByColumn)
        => MrRepositoryOptions.SortOptions.ThenByColumn = thenByColumn;

        public static void EnableGeneralTranslator(this MrRepositoryOptions MrRepositoryOptions)
        => MrRepositoryOptions.Translator = true;


        public static void Inject(this MrRepositoryOptions MrRepositoryOptions, params Type[] anyClassInAssembly)
        => MrRepositoryOptions.Assemblies = GetAbleNameSpaceToType(anyClassInAssembly);
        public static void Inject(this MrRepositoryOptions MrRepositoryOptions, params string[] anyClassInAssembly)
        => MrRepositoryOptions.Assemblies = GetAbleNameSpaceToType(anyClassInAssembly);
        public static void Inject(this MrRepositoryOptions MrRepositoryOptions, params Assembly[] anyClassInAssembly)
        => MrRepositoryOptions.Assemblies = GetAbleNameSpaceToType(anyClassInAssembly);

        public static void EnableLoggingExceptionHandler(this MrRepositoryOptions MrRepositoryOptions)
         => MrRepositoryOptions.LoggingExceptionHandler = true;



        public static void AutoInject(this MrRepositoryOptions MrRepositoryOptions)
        => MrRepositoryOptions.Assemblies = RepositoryDependencies.AbleNameSpaceToType();
        public static void AutoInject(this MrRepositoryOptions MrRepositoryOptions, params string[] anyClassInAssembly)
        => MrRepositoryOptions.Assemblies = GetAbleNameSpaceToType(RepositoryDependencies.Concat(anyClassInAssembly).Distinct().ToArray());


        private static List<Type> GetAbleNameSpaceToType<T>(T[] anyClassInAssembly)
        => anyClassInAssembly.AbleNameSpaceToType<T>();

        private static string[] RepositoryDependencies => Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory).
           Where(x => Path.GetExtension(x) == FixedCommonValue.DllExtension).Select(x => Path.GetFileNameWithoutExtension(x))
            .Where(x => x.Contains(FixedCommonValue.Repository, StringComparison.OrdinalIgnoreCase)).ToArray();
    }
}
