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

                CheckedListBox clb = new CheckedListBox();
                clb.Dock = DockStyle.Fill;

                foreach (var course in courseList)
                {
                    clb.Items.Insert(0, course.course + " - " + course.courseAverage);
                }

                tp.Controls.Add(clb);
                i++;
            }
        }

        private void calculateGPAButton_Click(object sender, EventArgs e)
        {
            double weightedGPA = Calculator.CalculateWeightedGPA();
            double unweightedGPA = Calculator.CalculateUnweightedGPA();

            weightedGpaOutputLabel.Text = weightedGPA.ToString();
            unweightedGpaOutputLabel.Text = unweightedGPA.ToString();
        }

        private void selectClassToExclude_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gpaCalculationBar_Click(object sender, EventArgs e)
        {

        }
    }
}
