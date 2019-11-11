using System.Collections.Generic;

namespace GPA_Calculator
{
    public class Calculator
    {
        public static double CalculateWeightedGPA()
        {
            var totalCourses = 0;
            foreach (var year in Variables.AllCourseLists) totalCourses += year.Count;

            double totalGPA = 0;
            foreach (var year in Variables.AllCourseLists)
            {
                foreach (var course in year)
                {
                    var scale = CalculateScale(Variables.ClassScale[course.course]) / 100;
                    var courseGPA = course.courseAverage * scale;
                    totalGPA += courseGPA;
                }
            }
            var avgGPA = totalGPA / totalCourses;
            return avgGPA;
        }

        public static double CalculateUnweightedGPA()
        {
            var totalCourses = 0;
            foreach (var year in Variables.AllCourseLists) totalCourses += year.Count;

            double totalGPA = 0;
            foreach (var year in Variables.AllCourseLists)
            {
                foreach (var course in year)
                {
                    var courseGPA = course.courseAverage * .04;
                    totalGPA += courseGPA;
                }
            }
            var avgGPA = totalGPA / totalCourses;
            return avgGPA;
        }

        public static double CalculateScale(string type)
        {
            double scale = 0;
            switch (type)
            {
                case "AP":
                    scale = 6;
                    break;
                case "PAP":
                    scale = 5.5;
                    break;
                case "OL":
                    scale = 5;
                    break;
            }

            return scale;
        }
    }
}
