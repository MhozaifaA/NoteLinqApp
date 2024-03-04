using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Hosting.Schedule.TasksQueue.DependencyInjection
{
    public static class MrTasksQueueServiceCollectionExtensions
    {
        public static IServiceCollection AddMrTasksQueue(this IServiceCollection services)
        {
           return services.AddSingleton<IMrTasksQueue, MrTasksQueue>().AddHostedService<MrTasksQueueBackgroundService>();
        }
    }
}
