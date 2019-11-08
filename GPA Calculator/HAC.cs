using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
        public List<Course> getCourses(CookieContainer cookies, Uri requestUri)
        {
            string data = getRawGradeData(cookies, requestUri);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(data);

            var courseHtml = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                    .Equals("AssignmentClass")).ToList();
            Regex x = new Regex(@"\w+\s-\s\d\s");
            List<Course> assignmentList= new List<Course>();
            foreach (var courseHtmlItem in courseHtml)
            {
                var courseName = courseHtmlItem.Descendants("a")
                    .Where(node => node.GetAttributeValue("class", "")
                        .Equals("sg-header-heading")).FirstOrDefault().InnerText.Trim();
                courseName = x.Replace(courseName, @"").Trim();
                while (courseName.Substring(courseName.Length-2) == "S1" || courseName.Substring(courseName.Length - 2) == "S2")
                {
                    courseName = courseName.Replace(courseName.Substring(courseName.Length - 2), "");
                    while (courseName.LastOrDefault() == ' ' || courseName.LastOrDefault() == '-')
                    {
                        courseName = courseName.TrimEnd(courseName[courseName.Length - 1]);
                    }
                }
               var courseGrade = courseHtmlItem.Descendants("span")
                    .Where(node => node.GetAttributeValue("class", "")
                        .Equals("sg-header-heading sg-right")).FirstOrDefault().InnerText.Trim().Remove(0, 15).TrimEnd('%');
               Course course = new Course();
               course.course = courseName;
               course.courseAverage = double.Parse(courseGrade);
               assignmentList.Add(course);
            }

            return assignmentList;
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
