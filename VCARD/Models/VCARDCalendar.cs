using System;
using System.Collections.Generic;
using System.Text;
using VCARD.Interfaces;
using System.Linq;
namespace VCARD.Models
{
    public class VCARDCalendar
    {
        public string VERSION {get;} = "2.0";
        public string PRODID { get; set; }
        public List<VCARDEntry> Entries { get; set; }

        public override string ToString() =>
            $"BEGIN:VCALENDAR\n\r" +
            $"VERSION:{VERSION}\n\r" +
            Entries
            .Select(a => a.ToString())
            .ToArray()
            .Aggregate((a, b) => a + b) +
            "END:VCALENDAR";



    }
}
