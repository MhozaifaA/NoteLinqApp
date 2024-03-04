using Meteors.AspNetCore.Hosting.Schedule.DistributeTimer.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Hosting.Schedule.DistributeTimer.Extensions
{
    public static class MrDistributeTimerExtensions
    {

        public static DistributeTimer GetRequierDistributeTimer(this MrDistributeTimerOption option)
        {
            return new DistributeTimer(option.Milliseconds)
            {
                Option = option,
                AutoReset = option.AutoReset,
                //Interval = option.Milliseconds,call from ctor
                Enabled = true, //equal start
            };
        }

        public static void OnIsDevelopment(this MrDivDistributeTimerOption option)
        {
           option.IsProduction = false;
        }

        public static IMrDistributeTimer InitSchedule(this IMrDistributeTimer distributeTimer, params MrDistributeTimerOption[] options)
        {
            foreach (var item in options)
                distributeTimer.SetOrReset(Guid.NewGuid().ToString(), item);
            return distributeTimer;
        }


        public static IMrDistributeTimer InitSchedule(this IMrDistributeTimer distributeTimer,string[] ids,  MrDistributeTimerOption[] options)
        {
            if (options.Length < ids.Length)
                throw new ArgumentException($"{nameof(DistributeTimer)} {nameof(InitSchedule)} length of {nameof(ids)} more than {nameof(options)}");

            foreach (var item in ids.Zip(options))
                distributeTimer.SetOrReset(item.First ?? Guid.NewGuid().ToString(), item.Second);
            return distributeTimer;
        }

        public static DistributeTimer ResetTimerDate(this DistributeTimer distributeTimer , DateTime dateExcute)
        {
            distributeTimer.Option.DateExcute = dateExcute;
            return distributeTimer;
        }
          public static DistributeTimer ResetTimerDate(this DistributeTimer distributeTimer , int milliseconds)
        {
            distributeTimer.Option.Milliseconds = milliseconds;
            return distributeTimer;
        }

    }
}
