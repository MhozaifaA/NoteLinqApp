using Meteors.AspNetCore.Hosting.Schedule.DistributeTimer.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Hosting.Schedule.DistributeTimer
{
    public interface IMrDistributeTimer
    {
        ConcurrentDictionary<string, DistributeTimer> Schedule { get; }
        DistributeTimer this[string id] { get; }
        long NumberOfTasks { get; }
        event Action<DistributeTimer> OnExclusion;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        DistributeTimer SetOrReset(string id, MrDistributeTimerOption option);
        DistributeTimer? Find(string id);

        /// <summary>
        /// Remove Task with spacific id from Timer Schedule
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Exclusion(string id);

        /// <summary>
        /// add new Task to timer Schedule
        /// </summary>
        /// <param name="id"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        bool Set(string id, MrDistributeTimerOption option);

        /// <summary>
        /// change task`s option (with spacific id) to new options
        /// </summary>
        /// <param name="id"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        bool Reset(string id, MrDistributeTimerOption option);

        /// <summary>
        /// Rest task`s Excute date (with spacific id) by new value of date Execute
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Reset(string id , DateTime dateExecute);

        /// <summary>
        /// Rest task`s Excute milliseconds (with spacific id) by new value of milliseconds
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Reset(string id , int milliseconds);
       // bool ResetDateExcute(string id);
    }
}
