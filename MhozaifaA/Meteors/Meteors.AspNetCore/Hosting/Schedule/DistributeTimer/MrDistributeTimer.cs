using Meteors.AspNetCore.Hosting.Schedule.DistributeTimer.Extensions;
using Meteors.AspNetCore.Hosting.Schedule.DistributeTimer.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Meteors.AspNetCore.Hosting.Schedule.DistributeTimer
{
    public class MrDistributeTimer : BaseMrDistributeTimer, IMrDistributeTimer
    {

        private readonly IServiceScopeFactory provider;
        private readonly MrDivDistributeTimerOption option;
        public MrDistributeTimer(IServiceScopeFactory provider , IOptions<MrDivDistributeTimerOption> option = null)
        {
            this.provider = provider;
            this.option = option?.Value ?? new MrDivDistributeTimerOption();
        }

        public DistributeTimer this[string id] => _schedule[id];

        private event Action<DistributeTimer> _OnExclusion;
        public event Action<DistributeTimer> OnExclusion { add => _OnExclusion += value; remove => _OnExclusion -= value; }//singelton

        public long NumberOfTasks => _schedule.LongCount();
        public ConcurrentDictionary<string, DistributeTimer> Schedule => _schedule;


        public virtual DistributeTimer SetOrReset(string id, MrDistributeTimerOption option)
        {
            if (IgnorPastInterval(option)) return null;

            return _schedule.AddOrUpdate(id, Activate(id, option), (key, value) => {
                value.Interval = option.Milliseconds;
                value.Option = option;
                return value;
            });
        }

        public virtual bool Set(string id, MrDistributeTimerOption option )
        {
            if (IgnorPastInterval(option)) return false;

            return _schedule.TryAdd(id, Activate(id, option));
        }

        public virtual bool Reset(string id, MrDistributeTimerOption option)
        {
            if (IgnorPastInterval(option)) return false;

            if (_schedule.TryGetValue(id, out DistributeTimer _timer))
                return _schedule.TryUpdate(id, Activate(id, option), _timer);
            return false;
        }

        public bool Reset(string id, DateTime dateExecute)
        {
            if (IgnorPastDateExcute(dateExecute)) return false;

            if (_schedule.TryGetValue(id, out DistributeTimer timer))
                return _schedule.TryUpdate(id, timer.ResetTimerDate(dateExecute), timer);
            return false;
        }

        public bool Reset(string id, int milliseconds)
        {
            if (IgnorPastMilliseconds(milliseconds)) return false;

            if (_schedule.TryGetValue(id, out DistributeTimer timer))
                return _schedule.TryUpdate(id, timer.ResetTimerDate(milliseconds), timer);
            return false;
        }

        public virtual DistributeTimer? Find(string id)
        {
            if (_schedule.TryGetValue(id, out DistributeTimer timer))
                return timer;
            return null;
        }

        public virtual bool Exclusion(string id)
        {
            if (_schedule.TryGetValue(id, out DistributeTimer timer))
            {
                timer.Enabled = false;
                timer.AutoReset = false;
                timer.Elapsed -= Elapsed;
                timer.Dispose();
                return _schedule.TryRemove(id, out _);
            }
            return false;
        }


        public virtual bool Exclusion(DistributeTimer timer)
        => Exclusion(timer.Option.Id);


        private async void Elapsed(object sender, ElapsedEventArgs e)
        {
            DistributeTimer timer = (DistributeTimer)sender;
            timer.Option.ExecutionCount++;
            if(option.IsProduction)
            await timer.Option.Execute(timer, provider);
            if (!timer.Option.AutoReset) { 
                Exclusion(timer);
                return;
            }

            if(timer.Option.AutoReset && timer.Option.NumberRepeating.HasValue)
            {
                if(timer.Option.ExecutionCount == timer.Option.NumberRepeating.Value ||
                    timer.Option.NumberRepeating.Value <=0 )
                {
                    Exclusion(timer);
                    return;
                }
            }
        }


        private DistributeTimer Activate(string id, MrDistributeTimerOption option)
        {
            option.SetId(id);
            DistributeTimer timer = option.GetRequierDistributeTimer();
            //init call disposed 
            timer.Disposed += (s, e) => _OnExclusion?.Invoke(timer);
            timer.Elapsed += Elapsed;
            return timer;
        }

        private bool IgnorPastInterval(MrDistributeTimerOption option)
        => IgnorPastDateExcute(option.DateExcute) || IgnorPastMilliseconds(option.Milliseconds);

        private bool IgnorPastMilliseconds(double milliseconds)
        => milliseconds < 0;

        private bool IgnorPastDateExcute(DateTime dateExcute)
        => dateExcute < DateTime.Now;

    }
}
