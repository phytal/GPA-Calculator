using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Web;

namespace RRISD_HAC_Access
{
    public class HAC
    {
        public HttpWebResponse login(String username, String password, out CookieContainer container)
        {
            container = new CookieContainer();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://hac.friscoisd.org/HomeAccess/Account/LogOn?ReturnUrl=%2fHomeAccess%2f");

                request.KeepAlive = true;
                request.Headers.Set(HttpRequestHeader.CacheControl, "max-age=0");
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.Headers.Add("Origin", @"https://accesscenter.roundrockisd.org");
                request.Headers.Add("Upgrade-Insecure-Requests", @"1");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.85 Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Referer = "https://hac.friscoisd.org/HomeAccess/Account/LogOn?ReturnUrl=%2fHomeAccess%2f";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.8");
                request.CookieContainer = container;
                request.Method = "POST";
                request.ServicePoint.Expect100Continue = false;

                string body = @"Database=10&LogOnDetails.UserName=" + username + "&LogOnDetails.Password=" + password;
                byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(body);
                request.ContentLength = postBytes.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(postBytes, 0, postBytes.Length);
                stream.Close();

                return (HttpWebResponse)request.GetResponse();
            }
            catch
            {
                return null;
            }
        }
        public List<Assignment> getAssignments(CookieContainer cookies, Uri requestUri)
        {
            Regex trimmer = new Regex(@"\s\s+", RegexOptions.Compiled);
            String data = getRawGradeData(cookies, requestUri);
            List<String> raw = new List<String>();
            List<Assignment> ret = new List<Assignment>();
            //past this line is absolutely horrid
            //you can thank my lack of regex knowledge for this
            int x = data.IndexOf(">", data.IndexOf("<a class=\"sg-header-heading\"")) + 1;
            int y = data.IndexOf("</a>", x);
            String course = data.Substring(x, y - x).Trim();
            int z = data.IndexOf("AVG ", x) + 4;
            double average = double.Parse(data.Substring(z, data.IndexOf("%", z) - z).Trim());
            data = data.Substring(y);
            bool flag = false;
            while (data.IndexOf("title=\"Title") != -1)
            {
                int a = data.IndexOf("title=\"Title");
                int index = data.IndexOf("<a class=\"sg-header-heading\"");
                if ((index == -1) && (!flag)) break;
                if (index != -1)
                {
                    x = data.IndexOf(">", index) + 1;
                }
                if (((a < x) || (flag && index == -1)))
                {
                    int b = data.IndexOf("\"", a + "title=".Length + 1);
                    String s = data.Substring(a + "title=".Length + 1, b - a - 7); //7 is the magic number
                    data = data.Substring(b);
                    int c = data.IndexOf("</td><td>");
                    data = data.Substring(c + 7);
                    c = data.IndexOf("</td><td>") + 10; //10 is also magic
                    int d = data.IndexOf("</td>", c + 5); //as is 5

                    String temp = data.Substring(c, d - c).Trim();
                    AssignmentStatus status = AssignmentStatus.Upcoming;
                    double points = -1;
                    if (temp.Equals("M"))
                    {
                        status = AssignmentStatus.Missing;
                    }
                    else if (temp.Equals("I"))
                    {
                        status = AssignmentStatus.Incomplete;
                    }
                    else if (temp.Equals("EXC"))
                    {
                        status = AssignmentStatus.Excused;
                    }
                    else if (double.TryParse(temp, out points))
                    {
                        status = AssignmentStatus.Complete;
                    }
                    else
                    {
                        points = -1;
                    }
                    //its about to get ugly
                    int e = s.IndexOf("Title:") + 6;
                    String title = s.Substring(e, s.IndexOf("\n", e) - e).Trim();
                    e = s.IndexOf("Classwork:") + 10;
                    String classwork = s.Substring(e, s.IndexOf("\n", e) - e).Trim();
                    e = s.IndexOf("Category:") + 9;
                    String category = s.Substring(e, s.IndexOf("\n", e) - e).Trim();
                    e = s.IndexOf("Due Date:") + 9;
                    DateTime date = DateTime.Parse(s.Substring(e, s.IndexOf("\n", e) - e).Trim());
                    date.AddHours(16); //set time due to 4:10 PM
                    date.AddMinutes(10);
                    e = s.IndexOf("Max Points:") + 11;
                    double maxPoints = double.Parse(s.Substring(e, s.IndexOf("\n", e) - e).Trim());
                    e = s.IndexOf("Can Be Dropped:") + 16;
                    bool droppable = s.Substring(e, s.IndexOf("\n", e) - e).Trim().Contains("Y");
                    e = s.IndexOf("Extra Credit:") + 14;
                    bool extraCredit = s.Substring(e, s.IndexOf("\n", e) - e).Trim().Contains("Y");
                    e = s.IndexOf("Has Attachments:") + 16;
                    bool attachments = s.Substring(e).Trim().Contains("Y");
                    //you can open your eyes now
                    ret.Add(new Assignment
                    {
                        title = HttpUtility.HtmlDecode(title),
                        course = trimmer.Replace(course, " "),
                        classwork = HttpUtility.HtmlDecode(classwork),
                        category = HttpUtility.HtmlDecode(category),
                        dueDate = date,
                        maxPoints = maxPoints,
                        points = points,
                        courseAverage = average,
                        canBeDropped = droppable,
                        extraCredit = extraCredit,
                        hasAttachment = attachments,
                        status = status
                    });
                }
                else
                {
                    x = data.IndexOf(">", data.IndexOf("<a class=\"sg-header-heading\"")) + 1;
                    y = data.IndexOf("</a>", x);
                    course = data.Substring(x, y - x).Trim();
                    z = data.IndexOf("AVG ", x) + 4;
                    average = double.Parse(data.Substring(z, data.IndexOf("%", z) - z).Trim());
                    data = data.Substring(y);
                    flag = true;
                }
            }
            //end horrid part
            return ret;
        }
        private String getRawGradeData(CookieContainer cookies, Uri requestUri)
        {
            String s = String.Empty;
            foreach (Cookie cookie in cookies.GetCookies(requestUri))
            {
                s += (cookie.Name + "=" + cookie.Value + "; ");
            }
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://accesscenter.roundrockisd.org/HomeAccess/Content/Student/Assignments.aspx");

                request.KeepAlive = true;
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.Headers.Add("Upgrade-Insecure-Requests", @"1");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.85 Safari/537.36";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, sdch");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.8");
                request.Headers.Set(HttpRequestHeader.Cookie, s);

                return readResponse((HttpWebResponse)request.GetResponse());
            }
            catch
            {
                return null;
            }
        }

        public bool isValidLogin(HttpWebResponse response)
        {
            return !readResponse(response).Contains("You have entered an invalid username or password");
        }
        private string readResponse(HttpWebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
            {
                Stream streamToRead = responseStream;
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    streamToRead = new GZipStream(streamToRead, CompressionMode.Decompress);
                }
                else if (response.ContentEncoding.ToLower().Contains("deflate"))
                {
                    streamToRead = new DeflateStream(streamToRead, CompressionMode.Decompress);
                }

                using (StreamReader streamReader = new StreamReader(streamToRead, Encoding.UTF8))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
    public enum AssignmentStatus
    {
        Upcoming,
        Complete,
        Incomplete,
        Missing,
        Excused
    }
    public class Student
    {
        public String id { get; set; }
        public List<Assignment> assignments { get; set; }
        public String name { get; set; }
        public String grade { get; set; }
    }
    public class Assignment
    {
        public String title { get; set; }
        public String course { get; set; }
        public String classwork { get; set; }
        public String category { get; set; }
        public DateTime dueDate { get; set; }
        public AssignmentStatus status { get; set; }
        public double maxPoints { get; set; }
        public double points { get; set; }
        public double courseAverage { get; set; } //meh, I prefer this to having a Course object with the classes, as the organizeAssignment function does just that
        public bool canBeDropped { get; set; }
        public bool extraCredit { get; set; }
        public bool hasAttachment { get; set; }
        public override String ToString()
        {
            return course + " : " + classwork + " : " + status + " : " + ((points == -1) ? "Due " + dueDate.ToShortDateString() : points + "/" + maxPoints);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Assignment))
                return false;
            Assignment assignment = (Assignment)obj;
            return (
                (title.Equals(assignment.title)) &&
                (course.Equals(assignment.course)) &&
                (category.Equals(assignment.category)) &&
                (dueDate.Equals(assignment.dueDate)) &&
                (status.Equals(assignment.status)) &&
                (points.Equals(assignment.points)) &&
                (canBeDropped.Equals(assignment.canBeDropped))
           );
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public class User
    {
        public String name { get; set; }
        public String phoneNumber { get; set; }
        public String username { get; set; }
        public String password { get; set; } //eep
        public SMSCarrier carrier { get; set; }
        public List<Student> students { get; set; }
    }
    public class UpdatedAssignment
    {
        public Assignment oldAssignment { get; set; }
        public Assignment newAssignment { get; set; }
        public String reason { get; set; }
        public bool positive { get; set; }
    }
}
