using Meteors.AspNetCore.Hosting.Schedule.DistributeTimer.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Meteors.AspNetCore.Hosting.Schedule.DistributeTimer
{
    public class DistributeTimer : Timer
    {
        public DistributeTimer(double interval) : base(interval) { }

        public MrDistributeTimerOption Option { get; internal set; }

        public int ExecuteCount => Option.ExecutionCount;
    }
}
