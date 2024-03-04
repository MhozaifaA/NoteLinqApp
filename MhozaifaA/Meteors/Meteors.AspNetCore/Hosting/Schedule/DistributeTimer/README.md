# MrDistributeTimer

> under Hosting-Schedule

Used for date schedule Tasks  contain provider and much power [DI]([Dependency injection in .NET | Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection))

------



## Get Started

**Startup.cs**

```c#
public void ConfigureServices(IServiceCollection services)
{
   ...
   services.AddMrDistributeTimer();
   ...
}           
```



```C#
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    ...    

    app.UseMrDistributeTimer(distribute =>
            {
                var option = new DistributeTimerOption()
                {
                    DateExcute = DateTime.Now.AddMinutes(0.5),
                    Name = "any name",
                };
                option.Execute = (timer, provider) => {
                    Debug.WriteLine($"Execute {timer.Option.DateExcute}");
                    return Task.CompletedTask;
                };

                s.InitSchedule(option);  // fill init schedule
            });
	
    ...  
    
     //or
     app.UseMrDistributeTimer( (distribute ,provider ) => {  } );
     
     //or
     app.UseMrDistributeTimer<ProjectDbContext>(async (distribute ,context ) => {  } );
     
    ... 
    
}
```



## How to use

**any Class**

``` c#
  public class MyRepo : IMyRepo
  {
      private readonly IMrDistributeTimer DistributeTimer;
      Public MyRepo(IMrDistributeTimer distributeTimer)
      {
          DistributeTimer=distributeTimer;
      }
      
      public void Add()
      {
             var option = new DistributeTimerOption() {
                DateExcute =date,
                Name ="fire message",
            };

            option.Execute = async (timer, provider) => {
                await Context.....
            };

            distributeTimer.TryAddOrUpdate(Guid.NewGuid() , option);
      }
      
  }
```



## Feature

**useful**

>         ConcurrentDictionary<string, DistributeTimer> Schedule { get; }
>         DistributeTimer this[string id] { get; }
>         long NumberOfTasks { get; }
>         event Action<DistributeTimer> OnExclusion;
>         DistributeTimer SetOrReset(string id, MrDistributeTimerOption option);
>         DistributeTimer Find(string id);
>         bool Exclusion(string id);
>         bool Set(string id, MrDistributeTimerOption option);
>         bool Reset(string id, MrDistributeTimerOption option);
>
>         bool ResetDateExcute(string id);

*more*

``` c#
distributeTimer[id] // to get full DistributeTimer
```

```c#
public bool ResetAuto { get; set; } = false; // set to true in squence event case by default is false 
```



------

# `more description  coming  soon`