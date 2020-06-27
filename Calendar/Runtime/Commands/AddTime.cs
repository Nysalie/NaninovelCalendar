using Naninovel;
using Naninovel.Commands;
using System.Threading;
using UniRx.Async;

namespace NaninovelCalendar
{
    public class AddTime : Command
    {
        public IntegerParameter Hours = 0;
        public IntegerParameter Days = 0;

        public override async UniTask ExecuteAsync (CancellationToken cancellationToken = default)
        {
            var uiManager = Engine.GetService<IUIManager>();
            var calendar = uiManager.GetUI<CalendarUI>();

            await calendar.AddTime(Hours, Days);
        }
    }
}