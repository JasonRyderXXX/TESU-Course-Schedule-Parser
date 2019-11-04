using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TESUParser.Enums
{
    public enum TESUCalendarEventTypeEnum
    {
        Unknown = 0,
        Week = 1,
        [Description("BEGIN MODULE")]
        Module = 2,
        [Description("Discussion Forum")]
        Posting = 3,
        [Description("Written Assignment")]
        WrittenAssignment = 4,
        MidtermExam = 5,
        MidtermProject = 6,
        Quiz = 7,
        FinalExam = 8,
        [Description("Final paper")]
        FinalProject = 9,
        [Description("Introductions Forum")]
        IntroductionPosting = 10
    }
}
