using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GPA_Calculator
{
    public class Helpers
    {
        public static List<CheckedBoxesInfo> CheckForCheckedBoxes(List<CheckedListBox> clb)
        {
            //a list of 2 values-the year and the list of classes that are checked
            List<CheckedBoxesInfo> checkedList = new List<CheckedBoxesInfo>();
            //foreach year in the list
            foreach (var checkedListBox in clb)
            {
                
                List<string> checkedSubList = new List<string>();
                //cycles through the list of checked boxes and adds them to our list of checked boxes
                for (byte i = 0; i <= checkedListBox.CheckedItems.Count - 1; i++)
                {
                    string className = checkedListBox.CheckedItems[i].ToString();

                    int numericValue;
                    //gets rid of extra letters/numbers
                    do
                    {
                        if (int.TryParse(className.Substring(className.Length - 1), out numericValue))
                        {
                            className = className.Trim(className[className.Length - 1]);
                            numericValue = 10; //sets to 10 because it cant go beyond 1 digit
                        }
                        else numericValue = 1;

                        className = className.Trim(className[className.Length - 1]);

                    } while (className.LastOrDefault() == '-' || className.LastOrDefault() == ' ' ||
                             numericValue == 10);

                    checkedSubList.Add(className);
                    var year = clb.IndexOf(checkedListBox) + 1;
                    checkedList.Add(new CheckedBoxesInfo
                    {
                        Year = year,
                        Classes = checkedSubList
                    });

                }
            }
            return checkedList;
        }
    }

    public class CheckedBoxesInfo
    {
        public int Year { get; set; }
        public List<string> Classes { get; set; }
    }
}

