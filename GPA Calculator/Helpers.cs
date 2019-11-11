using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GPA_Calculator
{
    public class Helpers
    {
        public static List<Tuple<int, List<string>>> CheckForCheckedBoxes(List<CheckedListBox> clb)
        {
            List<Tuple<int, List<string>>> checkedList = new List<Tuple<int, List<string>>>();
            //a list of 2 values-the year and the list of classes that are checked
            foreach (var checkedListBox in clb)
            {
                List<string> checkedSubList = new List<string>();
                checkedSubList.Add("none");
                for (byte i = 0; i <= checkedListBox.Items.Count - 1; i++)
                {
                    if (checkedListBox.GetItemChecked(i))
                    {
                        if (checkedSubList.Contains("none")) checkedSubList.Remove("none");
                        checkedSubList.Add(i + " = " + checkedListBox.Items[i]); //index - class
                    }
                }

                var year = clb.IndexOf(checkedListBox);
                checkedList.Add(new Tuple<int, List<string>>(year,checkedSubList));
            }

            return checkedList;
        }
    }
}
