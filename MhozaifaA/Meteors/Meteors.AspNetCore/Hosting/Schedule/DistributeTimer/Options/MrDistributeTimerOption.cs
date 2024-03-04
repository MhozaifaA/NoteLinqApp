using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Hosting.Schedule.DistributeTimer.Options
{
    public sealed class MrDistributeTimerOption
    {
        public string Id { get; private set; }
        public string Name { get; set; }
        public bool AutoReset { get; set; } = false;

        /// <summary>
        /// Max Number of repeat execut <para> work with <see cref="AutoReset" langword="true"/>  </para>
        /// </summary>
        public Nullable<int> NumberRepeating { get; set; } = null;

        internal int ExecutionCount { get; set; } = 0;

        private DateTime dateExecute;
        public DateTime DateExcute
        {
            get { return dateExecute; }
            set
            {
                dateExecute = value;
                milliseconds = value.Subtract(DateTime.Now).TotalMilliseconds; //throw when -
            }
        }


        private double milliseconds;
        public double Milliseconds
        {
            get { return milliseconds; }
            set
            {
                milliseconds = value;
                dateExecute = DateTime.Now.AddMilliseconds(value);
            }
        }

        public Func<DistributeTimer, IServiceScopeFactory, Task> Execute{ get; set; }

        public void SetExecute(Func<DistributeTimer, IServiceScopeFactory, Task> execute)
         => Execute = execute;

        public void SetId(string id) => Id = id;

        public override string ToString() => Id;
    }

    public class MrDivDistributeTimerOption
    {
        internal bool IsProduction { get; set; } = true;
    }
}
