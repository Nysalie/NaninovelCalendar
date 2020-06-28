using Naninovel;

namespace NaninovelCalendar
{
    public class CalendarControlPanelButton : ScriptableLabeledButton
    {
        private CalendarUI calendar;
        private IStateManager stateManager;

        protected override void Awake ()
        {
            base.Awake();

            calendar = Engine.GetService<IUIManager>().GetUI<CalendarUI>();
            stateManager = Engine.GetService<IStateManager>();
        }

        protected override void OnButtonClick()
        {
            calendar.ToggleVisibility();
            stateManager.PushRollbackSnapshot();
        } 
    }
}
