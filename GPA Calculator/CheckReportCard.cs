using System;
using System.Collections.Generic;
using System.Linq;
using GPA_Calculator;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace GPACalculator
{
    public class CheckReportCard
    {
        public static List<Course> CheckReportCardTask(HtmlDocument reportCardDocument)
        {
            List<Course> coursesFromReportCard = new List<Course>();
            DateTime now = DateTime.Now;


            if (now > Variables.ReportingPeriod1)
            {
                List<Course> reportingPeriod1Courses = ReportCardScraping(reportCardDocument, 1);
                foreach (Course course in reportingPeriod1Courses)
                {
                    coursesFromReportCard.Add(course);
                }
                
            }

            if (now > Variables.ReportingPeriod2)
            {
                List<Course> reportingPeriod2Courses = ReportCardScraping(reportCardDocument, 2);
                foreach (Course course in reportingPeriod2Courses)
                {
                    if (coursesFromReportCard.Contains(course))
                    {
                        var existingCourseIndex = coursesFromReportCard.FindIndex(x => x.courseID == course.courseID);
                        Course existingCourse = coursesFromReportCard[existingCourseIndex];
                        double newAvg = (existingCourse.courseAverage + course.courseAverage) / 2;
                        existingCourse.courseAverage = newAvg;
                    }
                    coursesFromReportCard.Add(course);
                }
                
            }

            if (now > Variables.ReportingPeriod3)
            {
                List<Course> reportingPeriod3Courses = ReportCardScraping(reportCardDocument, 3);
                foreach (Course course in reportingPeriod3Courses)
                {
                    if (coursesFromReportCard.Contains(course))
                    {
                        var existingCourseIndex = coursesFromReportCard.FindIndex(x => x.courseID == course.courseID);
                        Course existingCourse = coursesFromReportCard[existingCourseIndex];
                        double newAvg = (existingCourse.courseAverage + course.courseAverage) / 2;
                        existingCourse.courseAverage = newAvg;
                    }
                    coursesFromReportCard.Add(course);
                }
                

            }

            return coursesFromReportCard;
        }

        private static List<Course> ReportCardScraping(HtmlDocument reportCardDocument, int markingPeriod)
        {
            List<Course> reportCardAssignmentList = new List<Course>();
            var allReportCardHtml = reportCardDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                    .Equals($"sg-content-grid")).ToList();
            var reportCardCourseList = allReportCardHtml[0].Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                    .Equals($"sg-content-grid")).ToList();
            var reportCardCourseItemList = reportCardCourseList[0].Descendants("tr")
                .Where(node => node.GetAttributeValue("class", "")
                    .Equals($"sg-asp-table-data-row")).ToList();
            foreach (var reportCardCourse in reportCardCourseItemList) //foreach report card
            {
                var courseName = reportCardCourse.Descendants("a") //gets course name
                    .Where(node => node.GetAttributeValue("href", "")
                        .Equals($"#")).FirstOrDefault().InnerText.Trim();

                var courseID = reportCardCourse.Descendants("td") //gets course id
                    .FirstOrDefault().InnerText.Trim();

                var courseGrade = ""; //finalized course grade
                var courseGradeHtml = reportCardCourse.Descendants("a") //gets course grade
                    .Where(node => node.GetAttributeValue("onclick", "")
                        .Contains($"OpenAssingmentsFromRC")).FirstOrDefault().InnerText.Trim();

                if (courseGradeHtml == "")
                {
                    continue; //if it is not a grade and is empty then retry
                }

                while (courseName.Substring(courseName.Length - 2) == "S1" ||
                       courseName.Substring(courseName.Length - 2) == "S2")
                {
                    courseName = courseName.Replace(courseName.Substring(courseName.Length - 2), "");
                    while (courseName.LastOrDefault() == ' ' || courseName.LastOrDefault() == '-')
                    {
                        courseName = courseName.TrimEnd(courseName[courseName.Length - 1]);
                    }
                }

                courseID = courseID.Remove(courseID.Length - 4);

                //removes excess
                while (courseID.LastOrDefault() == ' ' || courseID.LastOrDefault() == '-' ||
                       courseID.LastOrDefault() == 'A' || courseID.LastOrDefault() == 'B')
                {
                    courseID = courseID.TrimEnd(courseID[courseID.Length - 1]);
                }

                courseGrade = courseGradeHtml;
                reportCardAssignmentList.Add(new Course
                {
                    courseID = courseID,
                    courseName = courseName,
                    courseAverage = double.Parse(courseGrade)
                }); //turns the grade (string) received into a double 
            }

            return reportCardAssignmentList;
        }
    }
}