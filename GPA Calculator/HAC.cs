using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace GPA_Calculator
{
    public class HAC
    {
        public HttpWebResponse login(string username, string password, out CookieContainer container)
        {
            container = new CookieContainer();
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(
                    "https://hac.friscoisd.org/HomeAccess/Account/LogOn?ReturnUrl=%2fHomeAccess%2fClasses%2fClasswork");

                request.KeepAlive = true;
                request.Headers.Set(HttpRequestHeader.CacheControl, "max-age=0");
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.Headers.Add("Origin", @"https://hac.friscoisd.org/");
                request.Headers.Add("Upgrade-Insecure-Requests", @"1");
                request.UserAgent = "Chrome/77.0.3865.120";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Referer = "https://hac.friscoisd.org/HomeAccess/Account/LogOn?ReturnUrl=%2fHomeAccess%2fClasses%2fClasswork";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.8");
                request.CookieContainer = container;
                request.Method = "POST";
                request.ServicePoint.Expect100Continue = false;

                string body = @"Database=10&LogOnDetails.UserName=" + username + "&LogOnDetails.Password=" + password;
                byte[] postBytes = Encoding.UTF8.GetBytes(body);
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

        public Task GetCourses(CookieContainer cookies, Uri requestUri)
        {
            try
            {
                //current courses
                //TODO: include report card and automatically calculate if a report card and how many report cards is/are needed
                string data = getRawGradeData(cookies, requestUri);

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(data);

                var courseHtml = htmlDocument.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                        .Equals("AssignmentClass")).ToList();
                Regex x = new Regex(@"\w+\s-\s\d\s");
                List<Course> assignmentList = new List<Course>();
                foreach (var courseHtmlItem in courseHtml)
                {
                    var courseName = courseHtmlItem.Descendants("a")
                        .Where(node => node.GetAttributeValue("class", "")
                            .Equals("sg-header-heading")).FirstOrDefault().InnerText.Trim();
                    courseName = x.Replace(courseName, @"").Trim();
                    while (courseName.Substring(courseName.Length - 2) == "S1" ||
                           courseName.Substring(courseName.Length - 2) == "S2")
                    {
                        courseName = courseName.Replace(courseName.Substring(courseName.Length - 2), "");
                        while (courseName.LastOrDefault() == ' ' || courseName.LastOrDefault() == '-')
                        {
                            courseName = courseName.TrimEnd(courseName[courseName.Length - 1]);
                        }
                    }

                    var courseGrade = courseHtmlItem.Descendants("span")
                        .Where(node => node.GetAttributeValue("class", "")
                            .Equals("sg-header-heading sg-right")).FirstOrDefault().InnerText.Trim().Remove(0, 15)
                        .TrimEnd('%');

                    assignmentList.Add(new Course {course = courseName, courseAverage = double.Parse(courseGrade)});
                }

                //past courses 
                string oldData = getRawOldGradeData(cookies, requestUri);

                var oldHtmlDocument = new HtmlDocument();
                oldHtmlDocument.LoadHtml(oldData); //gets all of the years
                byte numCourses = 0; //no one will exceed 255 
                for (byte i = 0; i >= 0; i++)
                    //what this does is tries to get as many courses as possible until it receives an error, then pass on that number to get that number of courses
                {
                    try //get number of years
                    {
                        var oldCourseHtml = oldHtmlDocument.DocumentNode
                            .Descendants("table") //initializes variable but isn't used purposely for testing purposes
                            .Where(node => node.GetAttributeValue("id", "")
                                .Equals($"plnMain_rpTranscriptGroup_dgCourses_{i}")).ToList();
                        if (oldCourseHtml.Count == 0) break;
                    }
                    catch (Exception)
                    {
                        break;
                    }

                    numCourses = i;
                }
                //note: numCourses will always be 1 less because of indexes
                for (byte i = 0; i <= numCourses-1; i++) //get all years (-1 to exclude the present year)
                {
                    List<Course> oldAssignmentList = new List<Course>();
                    var oldCourseHtml = oldHtmlDocument.DocumentNode.Descendants("table")
                        .Where(node => node.GetAttributeValue("id", "")
                            .Equals($"plnMain_rpTranscriptGroup_dgCourses_{i}")).ToList();
                    var oldCourseItemList = oldCourseHtml[0].Descendants("tr")
                        .Where(node => node.GetAttributeValue("class", "")
                            .Equals($"sg-asp-table-data-row")).ToList();
                    foreach (var courseHtmlItem in oldCourseItemList) //foreach course that is listed in a year
                    {
                        var courseName = courseHtmlItem.Descendants("td") //gets course name
                            .ElementAt(1).InnerText
                            .Trim(); //course name is stored at the second instance of td

                        for (byte j = 4; j <= 4 && j >1; j--) 
                            //gets grade, starts from last element, which is overall avg, if nothing, goes to second semester, then first semester
                        {
                            var courseGrade = "";//finalized course grade
                            var courseGradeHtml = courseHtmlItem.Descendants("td") //gets course grade
                                .ElementAt(j).InnerText;

                            if (courseGradeHtml == "&nbsp;")
                            {
                                continue; //if it is not a grade and is empty then retry
                            }
                            if (j == 3) break; //if the course avg is available then you dont need to get the semester grades
                            courseGrade = courseGradeHtml;
                            oldAssignmentList.Add(new Course
                            {
                                course = courseName,
                                courseAverage = double.Parse(courseGrade)
                            }); //turns the grade (string) received into a double 
                        }
                    }
                    Variables.AllCourseLists.Add(oldAssignmentList);
                }
                Variables.AllCourseLists.Add(assignmentList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return Task.CompletedTask;
        }

        private string getRawGradeData(CookieContainer cookies, Uri requestUri)
        {
            string s = string.Empty;
            foreach (Cookie cookie in cookies.GetCookies(requestUri))
            {
                s += (cookie.Name + "=" + cookie.Value + "; ");
            }
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("https://hac.friscoisd.org/HomeAccess/Content/Student/Assignments.aspx"));

                request.KeepAlive = true;
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.Headers.Add("Upgrade-Insecure-Requests", @"1");
                request.UserAgent = "Chrome/77.0.3865.120";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.8");
                request.Headers.Set(HttpRequestHeader.Cookie, s);
                return readResponse((HttpWebResponse)request.GetResponse()); //
            }
            catch
            {
                return null;
            }
        }

        private string getRawOldGradeData(CookieContainer cookies, Uri requestUri)
        {
            string s = string.Empty;
            foreach (Cookie cookie in cookies.GetCookies(requestUri))
            {
                s += (cookie.Name + "=" + cookie.Value + "; ");
            }
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("https://hac.friscoisd.org/HomeAccess/Content/Student/Transcript.aspx"));

                request.KeepAlive = true;
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.Headers.Add("Upgrade-Insecure-Requests", @"1");
                request.UserAgent = "Chrome/77.0.3865.120";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.8");
                request.Headers.Set(HttpRequestHeader.Cookie, s);
                return readResponse((HttpWebResponse)request.GetResponse()); //
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
    public class Course
    {
        public string course { get; set; }
        public double courseAverage { get; set; }
    }
}
