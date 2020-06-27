using UnityEngine;
using Naninovel;

namespace NaninovelCalendar
{
    [System.Serializable]
    public class CalendarConfiguration : Configuration
    {
        [Tooltip("The start time and date on new game. \nFormat: YYYY-MM-DD HH:MM:SS")]
        public string StartDate = "2020-01-01 12:00:00";
    }
}
