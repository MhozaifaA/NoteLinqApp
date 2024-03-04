using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.MVC.Filter.Options
{
    public class MrMvcFilterOptions
    {
        public MrExceptionFilterOptions ExceptionFilter { get; set; } = new MrExceptionFilterOptions();
    }
}
