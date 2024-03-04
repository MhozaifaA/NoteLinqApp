using Meteors.AspNetCore.Infrastructure.ModelEntity.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Service.Options
{
    public class SortOptions
    { 
        public Sort Sort { get; set; }
        public string OrderByColumn { get; set; }
        public string ThenByColumn { get; set; }
    }
}
