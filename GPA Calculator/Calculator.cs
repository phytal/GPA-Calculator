using System.Collections.Generic;
using System.Windows.Forms;

namespace GPA_Calculator
{

    public class Calculator
    {
        internal static List<List<Course>> checkedCourses = new List<List<Course>>();
        public static double CalculateWeightedGPA(List<CheckedListBox> checkedListBoxesList)
        {
            var allCourseLists = Variables.AllCourseLists;
            double totalGPA = 0;
            //gets all of the boxes that are checked to be excluded from
            var checkedBoxes = Helpers.CheckForCheckedBoxes(checkedListBoxesList);
            //if there are checked boxes
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

                        //if there are any overlapping classes
                        foreach (var element in checkedBoxes)
                        {
                            if (!(element.Classes.Contains(course.courseID) && element.Year.Equals(yearNumber)))
                            {
                                continue;
                            }
                            subList.Add(course);
                        }


                    }

                    checkedCourses.Add(subList);
                }
            }

            var totalCourses = 0;
            //gets total number of courses
            foreach (var year in allCourseLists) totalCourses += year.Count;
            totalCourses -= checkedBoxes.Count;
            //foreach (even though its a for loop) year you took (-1 because of indexes)
            for (var year = 0; year <= allCourseLists.Count - 1; year++)
            {
                List<Course> courseForYear = allCourseLists[year];
                //foreach course in that year
                foreach (var course in courseForYear)
                {
                    //check if a checked box is being used
                    if (checkedCourses[year].Contains(course))
                        continue;
                    var scale = CalculateScale(Variables.ClassScale[course.courseID]) / 100;
                    var courseGPA = course.courseAverage * scale;
                    totalGPA += courseGPA;
                }
            }
            var avgGPA = totalGPA / totalCourses;
            return avgGPA;
        }

        public static double CalculateUnweightedGPA(List<CheckedListBox> checkedListBoxesList)
        {
            var allCourseLists = Variables.AllCourseLists;
            //this won't need all the code from before because the results from getting the weighted gpa can be used here as well
            double totalGPA = 0;

            var totalCourses = 0;
            //gets total number of courses
            foreach (var year in allCourseLists) totalCourses += year.Count;
            var total = 0;
            foreach (var year in checkedCourses)
                if (year.Count != 0) total++;
            totalCourses -= total;
            //foreach (even though its a for loop) year you took (-1 because of indexes)
            for (var year = 0; year <= allCourseLists.Count - 1; year++)
            {
                List<Course> courseForYear = allCourseLists[year];
                //foreach course in that year
                foreach (var course in courseForYear)
                {
                    if (checkedCourses[year].Contains(course))
                        continue;
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
