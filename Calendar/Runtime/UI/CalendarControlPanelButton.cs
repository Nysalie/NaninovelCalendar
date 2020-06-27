using Naninovel;

namespace NaninovelCalendar
{
    public class CalendarControlPanelButton : ScriptableLabeledButton
    {
        private CalendarUI calendar;

        protected override void Awake ()
        {
            base.Awake();

            calendar = Engine.GetService<IUIManager>().GetUI<CalendarUI>();
        }

        protected override void OnButtonClick () => calendar.ToggleVisibility();
    }
}
