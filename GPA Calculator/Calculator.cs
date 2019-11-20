using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GPA_Calculator
{

    public class Calculator
    {
        private static List<List<Course>> uncheckedCourses = new List<List<Course>>();
        public static double CalculateWeightedGPA(List<CheckedListBox> checkedListBoxesList)
        {
            var allCourseLists = Variables.AllCourseLists;
            double totalGPA = 0;
            //gets all of the boxes that are checked to be excluded from
            var checkedBoxes = Helpers.CheckForCheckedBoxes(checkedListBoxesList);
            var yearNumber = 0;
            if (checkedBoxes.Count != 0)
            {
                foreach (var year in allCourseLists)
                {
                    yearNumber++;
                    List<Course> subList = new List<Course>();
                    foreach (var course in year)
                    {
                        var checkedBoxIndex = yearNumber - 1;
                        //if it goes over it breaks to prevent errors
                        if (checkedBoxIndex > checkedBoxes.Count - 1)
                        {
                            subList.Add(course);
                            continue;
                        }
                        //if there are any overlapping classes
                        if (checkedBoxes[checkedBoxIndex].Classes.Contains(course.course))
                        {
                            continue;
                        }
                        subList.Add(course);
                    }
                    uncheckedCourses.Add(subList);
                }
            }

            var totalCourses = 0;
            //gets total number of courses
            foreach (var year in uncheckedCourses) totalCourses += year.Count;
            //foreach (even though its a for loop) year you took (-1 because of indexes)
            for (var year = 0; year <= uncheckedCourses.Count - 1; year++)
            {
                List<Course> courseForYear = uncheckedCourses[year];
                //foreach course in that year
                foreach (var course in courseForYear)
                {
                    var scale = CalculateScale(Variables.ClassScale[course.course]) / 100;
                    var courseGPA = course.courseAverage * scale;
                    totalGPA += courseGPA;
                }
            }
            var avgGPA = totalGPA / totalCourses;
            return avgGPA;
        }

        public static double CalculateUnweightedGPA(List<CheckedListBox> checkedListBoxesList)
        {
            //this won't need all the code from before because the results from getting the weighted gpa can be used here as well
            double totalGPA = 0;

            var totalCourses = 0;
            //gets total number of courses
            foreach (var year in uncheckedCourses) totalCourses += year.Count;
            //foreach (even though its a for loop) year you took (-1 because of indexes)
            for (var year = 0; year <= uncheckedCourses.Count - 1; year++)
            {
                List<Course> courseForYear = uncheckedCourses[year];
                //foreach course in that year
                foreach (var course in courseForYear)
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
