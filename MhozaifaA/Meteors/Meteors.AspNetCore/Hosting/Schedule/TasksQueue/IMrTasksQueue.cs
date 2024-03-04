using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Hosting.Schedule.TasksQueue
{
    public interface IMrTasksQueue
    {
        void SetTaskToQueue(Func<CancellationToken, Task> workItem);

        Task<Func<CancellationToken, Task>> DequeueAsync(
            CancellationToken cancellationToken);

        Task<Func<CancellationToken, Task>> PeekAsync(
          CancellationToken cancellationToken);
    }
}
