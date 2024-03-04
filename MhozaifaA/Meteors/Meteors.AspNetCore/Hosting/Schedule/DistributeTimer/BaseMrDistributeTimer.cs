using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Meteors.AspNetCore.Hosting.Schedule.DistributeTimer
{
    public abstract class BaseMrDistributeTimer {

        //protected readonly static ConcurrentDictionary<string, DistributeTimer> _schedule;

        //static BaseMrDistributeTimer()
        //{
        //    _schedule = new ConcurrentDictionary<string, DistributeTimer>();
        //}

        protected readonly ConcurrentDictionary<string, DistributeTimer> _schedule;

        public BaseMrDistributeTimer()
        {
            _schedule = new ConcurrentDictionary<string, DistributeTimer>();
        }

    }
}
