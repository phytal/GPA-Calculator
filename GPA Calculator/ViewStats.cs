using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPA_Calculator
{
    public partial class ViewStats : Form
    {
        public ViewStats()
        {
            InitializeComponent();
            byte i = 1;
            foreach (var courseList in Variables.AllCourseLists)
            {
                TabPage tp = new TabPage($"Year {i}");
                chooseTime.TabPages.Add(tp);

                CheckedListBox clb = new CheckedListBox
                {
                    AccessibleName = $"Year{i}CLB", 
                    Dock = DockStyle.Fill
                };

                foreach (var course in courseList)
                {
                    clb.Items.Insert(0, course.course + " - " + course.courseAverage);
                }
                Variables.AllCheckedListBoxes.Add(clb);
                tp.Controls.Add(clb);
                i++;
            }

            Variables.NumberOfYears = i;
        }

        private void calculateGPAButton_Click(object sender, EventArgs e)
        {
            List<CheckedListBox> checkedListBoxesList = new List<CheckedListBox>();
            for (byte i = 1; i <= Variables.NumberOfYears; i++)
            {

                //goes to tab control and finds the tab
                foreach (TabPage tp in chooseTime.TabPages)
                {
                    foreach (CheckedListBox clb in tp.Controls)
                    {
                        if (!checkedListBoxesList.Contains(clb))
                            checkedListBoxesList.Add(clb);
                    }
                }
            }
            double weightedGPA = Calculator.CalculateWeightedGPA(checkedListBoxesList);
            double unweightedGPA = Calculator.CalculateUnweightedGPA(checkedListBoxesList);

            weightedGpaOutputLabel.Text = weightedGPA.ToString();
            unweightedGpaOutputLabel.Text = unweightedGPA.ToString();
        }

        private void gpaCalculationBar_Click(object sender, EventArgs e)
        {

        }
    }
}
