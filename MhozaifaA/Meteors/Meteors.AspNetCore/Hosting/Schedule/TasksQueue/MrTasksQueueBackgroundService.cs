using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Hosting.Schedule.TasksQueue
{
    internal class MrTasksQueueBackgroundService : BackgroundService
    {
        private readonly IMrTasksQueue MrTasksQueue;


        public MrTasksQueueBackgroundService(IMrTasksQueue MrTasksQueue)
        {
            MrTasksQueue = MrTasksQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await MrTasksQueue.PeekAsync(stoppingToken);
                await MrTasksQueue.DequeueAsync(stoppingToken);
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            //Fill Queue
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            //handel
            return base.StopAsync(cancellationToken);
        }
    }
}
