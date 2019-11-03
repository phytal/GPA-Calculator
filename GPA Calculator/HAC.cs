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

namespace FISD_HAC_Access
{
    public class HAC
    {
        public HttpWebResponse login(string username, string password, out CookieContainer container)
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
            string data = getRawGradeData(cookies, requestUri);
            List<string> raw = new List<string>();
            List<Assignment> ret = new List<Assignment>();
            //past this line is absolutely horrid
            //you can thank my lack of regex knowledge for this
            int x = data.IndexOf(">", data.IndexOf("<a class=\"sg-header-heading\"")) + 1;
            int y = data.IndexOf("</a>", x);
            string course = data.Substring(x, y - x).Trim();
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
                    string s = data.Substring(a + "title=".Length + 1, b - a - 7); //7 is the magic number
                    data = data.Substring(b);
                    int c = data.IndexOf("</td><td>");
                    data = data.Substring(c + 7);
                    c = data.IndexOf("</td><td>") + 10; //10 is also magic
                    int d = data.IndexOf("</td>", c + 5); //as is 5

                    //you can open your eyes now
                    ret.Add(new Assignment
                    {
                        course = trimmer.Replace(course, " "),
                        courseAverage = average,});
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
        private string getRawGradeData(CookieContainer cookies, Uri requestUri)
        {
            string s = string.Empty;
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
    public class Assignment
    {
        public string course { get; set; }
        public double courseAverage { get; set; }
    }
}
