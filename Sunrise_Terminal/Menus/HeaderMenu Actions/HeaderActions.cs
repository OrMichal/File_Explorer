using Sunrise_Terminal.Core;
using Sunrise_Terminal.objects;
using Sunrise_Terminal.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise_Terminal.Menus.HeaderMenu_Actions
{
    public class HeaderActions
    {
        public API api;

        public HeaderActions(API api)
        {
            this.api = api;
        }

        enum checkBoxOptions
        {
            File = 0,
            Directory = 1,
            Date = 2,
        }

        public void Filter(List<CheckBox> checkBoxes)
        {
            int counter = 0;
            foreach (var item in checkBoxes)
            {
                if (item.activeChoice == ' ')
                {
                    counter++;
                }
            }

            if(counter == checkBoxes.Count)
            {
                return;
            }

            if (checkBoxes[((int)checkBoxOptions.File)].activeChoice == 'x')
            {
                api.GetActiveListWindow().dataManagement.Filter = Filters.FilterFiles;
                api.RequestFilesRefresh();
            }

            if (checkBoxes[((int)checkBoxOptions.Directory)].activeChoice == 'x')
            {
                var FilteredArray = api.GetActiveListWindow().Rows.Where(x => x.file == false).ToList();
                api.GetActiveListWindow().Rows = FilteredArray;
            }

            if (checkBoxes[((int)checkBoxOptions.Date)].activeChoice == 'x')
            {
                var FilteredArray = api.GetActiveListWindow().Rows.OrderByDescending(x => x.DateOfLastChange).ToList();
                api.GetActiveListWindow().Rows = FilteredArray;
            }
        }
    }
}
