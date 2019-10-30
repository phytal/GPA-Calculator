using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRISD_HAC_Access
{
    public static class AssignmentStore
    {
        private static Dictionary<Student, List<Assignment>> students = new Dictionary<Student, List<Assignment>>();

        public static List<UpdatedAssignment> updateAssignments(Student student, List<Assignment> assignments)
        {
            List<Assignment> stored = students[student];
            List<UpdatedAssignment> ret = new List<UpdatedAssignment>();
            foreach (Assignment assignment in stored)
            {
                foreach (Assignment check in assignments)
                {
                    if (assignment.classwork.Equals(check.classwork))
                    {
                        if (!assignment.Equals(check))
                        {
                            ret.Add(new UpdatedAssignment
                            {
                                oldAssignment = assignment,
                                newAssignment = check
                            });
                        }
                    }
                }
            }
            students[student] = assignments;
            return ret;
        }
        public static void addStudent(Student student)
        {
            students[student] = new List<Assignment>();
        }
    }
}
