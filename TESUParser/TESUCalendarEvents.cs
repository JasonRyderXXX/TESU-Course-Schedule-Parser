using System;
using System.Collections.Generic;
using System.Text;
using TESUParser.Enums;

namespace TESUParser
{
    public class TESUCalendarEvent
    {
        /// <summary>
        /// Online Class Name IE FIN312
        /// </summary>
        public string TESUClassName { get; set; }
        /// <summary>
        /// What Type of assignment it is
        /// </summary>
        public TESUCalendarEventTypeEnum EventType { get; set; }
        /// <summary>
        /// When the it starts most of the time this is the same day as end day
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// When it is due(end date)
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// What Module it is in
        /// </summary>
        public int Module { get; set; }
        /// <summary>
        /// What Week Number it occurs in
        /// </summary>
        public int WeekNumber { get; set; }
        public int First { get; set; }
        public int Second { get; set; }
    }
}
