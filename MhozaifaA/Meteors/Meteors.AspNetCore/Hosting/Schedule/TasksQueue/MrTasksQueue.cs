﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Hosting.Schedule.TasksQueue
{
    public class MrTasksQueue : IMrTasksQueue
    {
        private ConcurrentQueue<Func<CancellationToken, Task>> _workItems =
           new ConcurrentQueue<Func<CancellationToken, Task>>();
        private SemaphoreSlim _signal = new SemaphoreSlim(0);

        public void SetTaskToQueue(
            Func<CancellationToken, Task> workItem)
        {
            if (workItem is null)
                throw new ArgumentNullException(nameof(workItem));

            _workItems.Enqueue(workItem);
            _signal.Release();
        }

        public async Task<Func<CancellationToken, Task>> DequeueAsync(
            CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _workItems.TryDequeue(out var workItem);

            return workItem;
        }

        public async Task<Func<CancellationToken, Task>> PeekAsync(
            CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _workItems.TryPeek(out var workItem);

            return workItem;
        }
    }
}
