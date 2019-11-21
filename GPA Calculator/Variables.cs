using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GPA_Calculator
{
    internal class Variables
    {
        internal static DateTime ReportingPeriod1 = new DateTime(2019, 10, 8); //end of first marking period etc.
        internal static DateTime ReportingPeriod2 = new DateTime(2019, 12, 20);
        internal static DateTime ReportingPeriod3 = new DateTime(2020, 3, 20);
        internal static Tuple<string, string, string> UserInfo = new Tuple<string, string, string>(null,null,null);
        internal static int NumberOfYears = 0;
        internal static List<CheckedListBox> AllCheckedListBoxes = new List<CheckedListBox>();
        internal static List<List<Course>> AllCourseLists = new List<List<Course>>(); //a list of all of the courses you took sorted by year
        internal static Dictionary<string, string> ClassScale = new Dictionary<string, string>() //classname, ap/pap/ol
        {
            {"GT HumanitiesI/PAP Eng", "PAP" },
            {"PAP Spanish II", "PAP" },
            {"Band A: YR 1", "OL" },
            {"A Band Inst. Ens. YR 1", "OL" },
            {"PAP Algebra II", "PAP" },
            {"PAP Biology", "PAP" },
            {"AP Human Geog- Grade 9", "AP" },
            {"PAP Computer Science", "PAP" },
            {"Algebra I - S1", "PAP" },
            {"HLTH ED", "OL" },
            {"GEOM", "PAP" },
            {"SPAN 1", "PAP" }
        };
    }
}
