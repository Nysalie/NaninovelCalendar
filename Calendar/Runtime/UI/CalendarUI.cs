using System;
using System.Globalization;
using UnityEngine;
using Naninovel;
using Naninovel.UI;
using UniRx.Async;
using UnityEngine.UI;

namespace NaninovelCalendar
{
    public class CalendarUI : CustomUI
    {
        [System.Serializable]
        public new class GameState
        {
            public string CurrentDate;
        }

        [Header("Text Fields")]
        [Tooltip("Text component for Day.")]
        [SerializeField] private Text dayText = null;
        [Tooltip("Text component for Month.")]
        [SerializeField] private Text monthText = null;

        [Header("Buttons")]
        [Tooltip("Button to advance time in days.")]
        [SerializeField] private ScriptableButton addDays;
        [Tooltip("How many days should the button add.")]
        [SerializeField] private int daysToAdd;
        [Tooltip("Button to advance time in hours.")]
        [SerializeField] private ScriptableButton addHours;
        [Tooltip("How many hours should the button add.")]
        [SerializeField] private int hoursToAdd;

        private IStateManager stateManager;
        private CalendarManager calendarManager;
        private ICustomVariableManager varMan;

        private string date = "gameDate";
        private string day = "day";
        private string month = "month";
        private DateTime convertedDate;

        protected override void Awake()
        {
            base.Awake();
            this.AssertRequiredObjects(dayText, monthText);

            stateManager = Engine.GetService<IStateManager>();
            calendarManager = Engine.GetService<CalendarManager>();
            varMan = Engine.GetService<ICustomVariableManager>();

            varMan.SetVariableValue(date, calendarManager.Configuration.StartDate);

            addDays.OnButtonClicked += AddDays;
            addHours.OnButtonClicked += AddHours;

            ConvertDate();
            UpdateDate();
        }

        private void ConvertDate() => convertedDate = DateTime.Parse(varMan.GetVariableValue(date));

        protected UniTask UpdateDate()
        {
            varMan.SetVariableValue(day, convertedDate.Day.ToString());
            varMan.SetVariableValue(month, convertedDate.ToString("MMMM", new CultureInfo("en-US")).ToUpper());
            varMan.SetVariableValue(date, convertedDate.ToString("yyyy-MM-dd HH:mm:ss"));

            dayText.text = varMan.GetVariableValue(day);
            monthText.text = varMan.GetVariableValue(month);

            return UniTask.CompletedTask;
        }

        public void ResetDate()
        {
            varMan.SetVariableValue(date, calendarManager.Configuration.StartDate);
            ConvertDate();
            UpdateDate();
        }

        public async UniTask AddTime(int hours = 0, int days = 0)
        {
            if (hours > 0)
                convertedDate = convertedDate.AddHours(hours);

            if (days > 0)
                convertedDate = convertedDate.AddDays(days);

            await UpdateDate();
        }

        protected void AddHours()
        {
            AddTime(hours:hoursToAdd);
            stateManager.PushRollbackSnapshot();
        }

        protected void AddDays()
        { 
            AddTime(days:daysToAdd);
            stateManager.PushRollbackSnapshot();
        }

        protected override void SerializeState (GameStateMap stateMap)
        {
            base.SerializeState(stateMap);
        
            var state = new GameState() {
                CurrentDate = varMan.GetVariableValue(date)
            };
            stateMap.SetState(state);
        }
        
        protected override UniTask DeserializeState (GameStateMap stateMap)
        {
            base.DeserializeState(stateMap);
        
            var state = stateMap.GetState<GameState>();
            if (state is null) return UniTask.CompletedTask;
        
            varMan.SetVariableValue(date, state.CurrentDate);
            ConvertDate();
            UpdateDate();
        
            return UniTask.CompletedTask;
        }
    }
}
