using Naninovel;
using Naninovel.Commands;
using System.Threading;
using UniRx.Async;

[Documentation("Adds time to game's calendar in the form of days and/or hours.")]
namespace NaninovelCalendar
{
    public class AddTime : Command
    {
        [Documentation("Hour count to add.")]
        public IntegerParameter Hours = 0;
        [Documentation("Day count to add.")]
        public IntegerParameter Days = 0;

        public override async UniTask ExecuteAsync (CancellationToken cancellationToken = default)
        {
            var uiManager = Engine.GetService<IUIManager>();
            var calendar = uiManager.GetUI<CalendarUI>();

            await calendar.AddTime(Hours, Days);
        }
    }
}