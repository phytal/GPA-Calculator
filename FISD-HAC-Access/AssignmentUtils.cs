using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RRISD_HAC_Access
{
    public class AssignmentUtils
    {
        public static Dictionary<String, List<Assignment>> organizeAssignments(List<Assignment> assignments)
        {
            Dictionary<String, List<Assignment>> ret = new Dictionary<String, List<Assignment>>();
            foreach (Assignment assignment in assignments)
            {
                if (ret.ContainsKey(assignment.course))
                    ret[assignment.course].Add(assignment);
                else
                {
                    ret[assignment.course] = new List<Assignment>();
                    ret[assignment.course].Add(assignment);
                }
            }
            return ret;
        }

        //doesn't take weighted average into consideration! :(
        public static double getListAverage(List<Assignment> assignments)
        {
            double a = 0;
            int b = 0;
            foreach (Assignment assignment in assignments)
            {
                if (assignment.points != -1)
                {
                    a += assignment.points;
                    b++;
                }
            }
            return (a / (double)b);
        }
        public static List<Assignment> getUpcomingAssignments(List<Assignment> assignments, TimeSpan timeSpan, bool includeAlreadyGradedAssignments)
        {
            DateTime now = DateTime.Now;
            List<Assignment> ret = new List<Assignment>();
            foreach (Assignment assignment in assignments)
            {
                TimeSpan x = assignment.dueDate.Subtract(now);
                if ((x < timeSpan) && (includeAlreadyGradedAssignments || assignment.points != -1))
                {
                    ret.Add(assignment);
                }
            }
            return ret;
        }
        public static List<Assignment> getAssignmentsAboveValue(List<Assignment> assignments, double value)
        {
            List<Assignment> ret = new List<Assignment>();
            foreach (Assignment assignment in assignments)
            {
                if ((assignment.points > value) && (assignment.points != -1)) //second part kind of irrelevant
                {
                    ret.Add(assignment);
                }
            }
            return ret;
        }
        public static List<Assignment> getAssignmentsBelowValue(List<Assignment> assignments, double value)
        {
            List<Assignment> ret = new List<Assignment>();
            foreach (Assignment assignment in assignments)
            {
                if ((assignment.points < value) && (assignment.points != -1))
                {
                    ret.Add(assignment);
                }
            }
            return ret;
        }
        public static List<Assignment> getExtraCreditOpportunities(List<Assignment> assignments, TimeSpan timeSpan, bool includeAlreadyGradedAssignments)
        {
            DateTime now = DateTime.Now;
            List<Assignment> ret = new List<Assignment>();
            foreach (Assignment assignment in assignments)
            {
                TimeSpan x = assignment.dueDate.Subtract(now);
                if ((x < timeSpan) && (includeAlreadyGradedAssignments || assignment.points != -1) && assignment.extraCredit)
                {
                    ret.Add(assignment);
                }
            }
            return ret;
        }
        public static List<Assignment> getDroppableAssignments(List<Assignment> assignments, bool includeAlreadyGradedAssignments)
        {
            List<Assignment> ret = new List<Assignment>();
            foreach (Assignment assignment in assignments)
            {
                if ((includeAlreadyGradedAssignments || assignment.points != -1) && assignment.canBeDropped)
                {
                    ret.Add(assignment);
                }
            }
            return ret;
        }
        public static List<Assignment> getAssignmentsUnderValueAndDroppable(List<Assignment> assignments, double value)
        {
            return getDroppableAssignments(getAssignmentsBelowValue(assignments, value), true);
        }
        public static List<Assignment> getAssignmentsWithStatus(List<Assignment> assignments, AssignmentStatus status)
        {
            List<Assignment> ret = new List<Assignment>();
            foreach (Assignment assignment in assignments)
            {
                if (assignment.status == status)
                {
                    ret.Add(assignment);
                }
            }
            return ret;
        }
        public static UpdatedAssignment getReasonForAssignmentUpdate(UpdatedAssignment assignment)
        {
            Assignment oldAssignment = assignment.oldAssignment;
            Assignment newAssignment = assignment.newAssignment;
            PropertyInfo[] properties = oldAssignment.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Type type = property.GetType();
                object oldValue = property.GetValue(oldAssignment);
                object newValue = property.GetValue(newAssignment);
                if (!oldValue.Equals(newValue))
                {
                    switch (property.Name)
                    {
                        case "points":
                            if ((oldAssignment.points < newAssignment.points) && (newAssignment.status == AssignmentStatus.Upcoming))
                            {
                                assignment.reason = " received a grade of " + newAssignment.points + "% on this assignment.";
                                if (newAssignment.points < 70)
                                {
                                    assignment.positive = false;
                                }
                                else
                                {
                                    assignment.positive = true;
                                }
                            }
                            else if (oldAssignment.points > newAssignment.points)
                            {
                                assignment.reason = " had a grade drop from a " + oldAssignment.points + "% to a " + newAssignment.points + "%. They now have a " + newAssignment.courseAverage + "% in " + newAssignment.course + ".";
                                assignment.positive = false;
                            }
                            else if (newAssignment.points > oldAssignment.points)
                            {
                                assignment.reason = " has risen a grade from a " + oldAssignment.points + "% to a " + newAssignment.points + "%. They now have a " + newAssignment.courseAverage + " % in " + newAssignment.course + ".";
                                assignment.positive = true;
                            }
                            break;
                        case "canBeDropped":
                            if ((newAssignment.canBeDropped) && (!oldAssignment.canBeDropped))
                            {
                                assignment.reason = " now has the opportunity to drop this assignment (Current grade: " + newAssignment.points + "%).";
                                assignment.positive = true;
                            }
                            else if ((oldAssignment.canBeDropped) && (!newAssignment.canBeDropped))
                            {
                                assignment.reason = " can no longer drop this assigment!";
                                assignment.positive = false;
                            }
                            break;
                        case "status":
                            if ((newAssignment.status == AssignmentStatus.Missing) && (oldAssignment.status != AssignmentStatus.Missing))
                            {
                                assignment.reason = " has not turned in this assignment, and is currently marked as missing!";
                                assignment.positive = false;
                            }
                            else if ((newAssignment.status == AssignmentStatus.Incomplete) && (oldAssignment.status != AssignmentStatus.Incomplete))
                            {
                                assignment.reason = " has an incomplete assignment!";
                                assignment.positive = false;
                            }
                            else if ((newAssignment.status == AssignmentStatus.Complete) && (oldAssignment.status != AssignmentStatus.Complete))
                            {
                                assignment.reason = " has turned in this assignment!";
                                assignment.positive = true;
                            }
                            break;
                        case "dueDate":
                            if (newAssignment.dueDate > oldAssignment.dueDate)
                            {
                                assignment.reason = " has had this assignment's due date pushed back! (From " + oldAssignment.dueDate.ToShortDateString() + " to " + newAssignment.dueDate.ToShortDateString() + ")";
                            }
                            else if (newAssignment.dueDate < oldAssignment.dueDate)
                            {
                                assignment.reason = " has had this assignment's due date pushed forward! (From " + oldAssignment.dueDate.ToShortDateString() + " to " + newAssignment.dueDate.ToShortDateString() + ")";
                            }
                            break;
                    }
                }
            }
            return assignment;
        }
    }
}
