using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GPA_Calculator
{
    internal class Variables
    {
        internal static DateTime ReportingPeriod1 = new DateTime(2019, 10, 8); //end of first marking period etc.
        internal static DateTime ReportingPeriod2 = new DateTime(2019, 12, 22);
        internal static DateTime ReportingPeriod3 = new DateTime(2020, 3, 20);
        internal static Tuple<string, string, string> UserInfo = new Tuple<string, string, string>(null,null,null);
        internal static int NumberOfYears = 0;
        internal static List<CheckedListBox> AllCheckedListBoxes = new List<CheckedListBox>();
        internal static List<List<Course>> AllCourseLists = new List<List<Course>>(); //a list of all of the courses you took sorted by year
        internal static Dictionary<string, string> ClassScale = new Dictionary<string, string>() //classname, ap/pap/ol
        {
            {"ELA11500", "PAP" }, //GT HumanitiesI/PAP Eng
            {"FLG22200", "PAP" }, //PAP Spanish II
            {"FNA25105", "OL" }, //Band A: YR 1
            {"FNA25125", "OL" }, //A Band Inst. Ens. YR 1
            {"MTH12200", "PAP" }, //PAP Algebra II
            {"SCI11200", "PAP" }, //PAP Biology
            {"SST11500", "AP" }, //AP Human Geog- Grade 9
            {"SUBM10011", "OL" }, //PE Sub Marching Band
            {"TEC21200", "PAP" }, //PAP Computer Science
            {"MTH11100", "PAP" }, //Algebra I 
            {"HPE18102", "OL" }, //HLTH ED
            {"MTH23200", "PAP" }, //GEOM
            {"FLG21100", "PAP" } //SPAN 1	
        };
        //internal static Dictionary<string, string> cmdDictionary = new Dictionary<string, string> //cmd, output
        //{
        //    {"help", Helpers.HelpCmd() },
        //    {"defaultUser", "Please type the name, username and password in the following format:\n" +
        //                    "name -- username -- password\n" +
        //                    "Ex: William Zhang -- Zhang.W -- Password123" },
        //    {"github", "Here is the github repository for this project!\n" +
        //               "https://github.com/Phytal/GPA-Calculator" }
        //};
    }
}
