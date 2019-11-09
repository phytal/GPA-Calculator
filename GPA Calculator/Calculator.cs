using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA_Calculator
{
    public class Calculator
    {
        public double CalculateGPA(List<Course> courses)
        {
            var totalCourses = courses.Count;
            double totalGPA = 0;
            foreach (var course in courses)
            {

                var scale = CalculateScale(Variables.ClassScale[course.course]) / 100;
                var courseGPA = course.courseAverage * scale;
                totalGPA += courseGPA;
            }
            var avgGPA = totalGPA / totalCourses;
            return avgGPA;
        }

        public double CalculateScale(string type)
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
