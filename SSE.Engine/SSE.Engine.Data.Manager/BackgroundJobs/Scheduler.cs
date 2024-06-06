using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSE.Engine.Data.Manager.BackgroundJobs.Jobs;

namespace SSE.Engine.Data.Manager.BackgroundJobs
{
    public class Scheduler
    {
        public static async Task Schedule()
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            // Define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<ContentRankingJob>()
                .WithIdentity("RankContent", "ContentRankingGroup")
                .Build();

            // Trigger the job to run now, and then every 10 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("RateContentTrigger", "ContentRankingGroup")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            // Tell quartz to schedule the job using our trigger
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
