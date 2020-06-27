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

        public void ResetService()
        {
            Engine.GetService<IUIManager>().GetUI<CalendarUI>().ResetDate();
        }

        public void DestroyService()
        {
            Engine.GetService<IUIManager>().GetUI<CalendarUI>().ResetDate();
        }
    }
}
