using Meteors.AspNetCore.Core.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Infrastructure.ModelEntity.Localization
{
    public interface ILanguages
    {
        public CultureDictionary Languages { get; set; }
    }
}
