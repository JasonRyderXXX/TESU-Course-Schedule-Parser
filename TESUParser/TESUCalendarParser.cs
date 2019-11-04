using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TESUParser.Enums;

namespace TESUParser
{
    public class TESUCalendarParser
    {
        const string Seprator = "—";
        public IList<TESUCalendarEvent> ClassAssignments { get; }
        public TESUCalendarParser(string className, DateTime startday, string data)
        {
            ClassName = className;
            ClassStartDay = startday;
            EndOfClass = startday.AddDays(90);
            ClassAssignments = GetEvents(String.Join("\r\n", data
                .Split("WEEK-BY-WEEK CALENDAR")[1]));

        }

        private readonly DateTime ClassStartDay;
        private readonly DateTime EndOfClass;
        private readonly string ClassName;
        private Regex eventparser = new Regex(@"(?<dayofweek>Monday|Tuesday|Wednesday|Thursday|Friday|Saturday|Sunday)?—?(?<assignmenttype>Introductions Forum|Discussion Forum|Written Assignment|Final paper|BEGIN MODULE|Week)?\s?(?<first>[1-9][1-9]|[0-9])?:? (?<second>[1-9][1-9]|[0-9])?", RegexOptions.ExplicitCapture);

        private List<Tuple<DOWEnum, TESUCalendarEventTypeEnum, int, int>> GetParsedEvents(string data)
        {
            List<Tuple<DOWEnum, TESUCalendarEventTypeEnum, int, int>> ret = new List<Tuple<DOWEnum, TESUCalendarEventTypeEnum, int, int>>();
            var result = eventparser.Match(data);
            while (result.Success)
            {
                ret.Add(new Tuple<DOWEnum, TESUCalendarEventTypeEnum, int, int>(result.Groups["dayofweek"].Value.ParseEnum<DOWEnum>(DOWEnum.AllWeek), result.Groups["assignmenttype"].Value.ParseEnum<TESUCalendarEventTypeEnum>(TESUCalendarEventTypeEnum.Unknown), result.Groups["first"].Value.ParseIntWithDefault(0), result.Groups["second"].Value.ParseIntWithDefault(0)));
                result = result.NextMatch();
            }
            return ret;
        }
        private DateTime CalcDateTime(int week, DOWEnum dow, bool eod = false) =>
            (eod ?
             ClassStartDay.Date.AddHours(23).AddMinutes(59).AddSeconds(59)
            : ClassStartDay.Date.AddHours(0).AddMinutes(0).AddSeconds(0))
            .AddDays((week - 1) * 7)
            .AddDays(((int)dow < 0) ? 0 : (int)dow);
        private List<TESUCalendarEvent> GetEvents(string data)
        {
            int module = 1;
            int week = 1;

            var parsedevents = GetParsedEvents(data)
                .Where(a => a.Item1 != DOWEnum.AllWeek && a.Item2 != TESUCalendarEventTypeEnum.Unknown)
                .ToList();
            List<TESUCalendarEvent> ret = new List<TESUCalendarEvent>();
            parsedevents.ForEach(a =>
            {
                (var dow, var et, var first, var second) = a;
                if (et == TESUCalendarEventTypeEnum.Module)
                    module = second;
                else if (et == TESUCalendarEventTypeEnum.Week)
                    week = second;
                ret.Add(new TESUCalendarEvent()
                {
                    EventType = et,
                    Module = module,
                    TESUClassName = ClassName,
                    WeekNumber = week,
                    StartDate = CalcDateTime(week, dow, false),
                    DueDate = CalcDateTime(week, dow, true),
                    First = (et == TESUCalendarEventTypeEnum.Posting) ? first : second,
                    Second = (et == TESUCalendarEventTypeEnum.Posting) ? second : 0
                });


            });
            return ret;
        }



    }
}
