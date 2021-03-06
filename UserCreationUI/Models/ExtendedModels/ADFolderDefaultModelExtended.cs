using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAppSharedLibrary;

namespace UserCreationUI.Models.ExtendedModels
{
    public class ADFolderDefaultModelExtended : ADFolderDefaultModel
    {
        /// <summary>
        ///  A calculated property that provides a friendly name for the UI.
        /// </summary>
        public string FriendlyName
        {
            get
            {
                string friendlyName = FolderName;

                if (Locations.Count > 0)
                {
                    if (Locations.Count > 1)
                    {
                        friendlyName += " (Locations: ";
                    }
                    else
                    {
                        friendlyName += " (Location: ";
                    }

                    IList<string> locationsPrepend = new List<string>();
                    foreach (int location in Locations)
                    {
                        locationsPrepend.Add(Program.GlobalConfig.Locations[location]);
                    }

                    friendlyName += string.Join(", ", locationsPrepend);
                    friendlyName += ")";
                }

                return friendlyName;
            }
        }
    }
}
