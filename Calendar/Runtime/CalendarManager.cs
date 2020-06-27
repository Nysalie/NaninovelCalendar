using Naninovel;
using UniRx.Async;
using UnityEngine;

namespace NaninovelCalendar
{
    [InitializeAtRuntime]
    public class CalendarManager : IEngineService<CalendarConfiguration>
    {
        
        public CalendarConfiguration Configuration { get; }

        public CalendarManager(CalendarConfiguration config)
        {
            Configuration = config;
        }

        public UniTask InitializeServiceAsync() => UniTask.CompletedTask;

        public void ResetService() { }

        public void DestroyService() { }
    }
}
