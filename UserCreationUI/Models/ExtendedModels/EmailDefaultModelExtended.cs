using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCreationLibrary;

namespace UserCreationUI.Models.ExtendedModels
{
    public class EmailDefaultModelExtended : EmailDefaultModel
    {
        /// <summary>
        ///  A calculated property that contains both WhoToAdd and the Approver.
        /// </summary>
        public string FriendlyName
        {
            get
            {
                string friendlyName = $"{EmailFormat}@{Domain}";

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

                if (EmployeeTypes.Count > 0)
                {
                    if (EmployeeTypes.Count > 1)
                    {
                        friendlyName += " (Employee Types: ";
                    }
                    else
                    {
                        friendlyName += " (Employee Type: ";
                    }

                    IList<string> employeeTypesPrepend = new List<string>();
                    foreach (int type in EmployeeTypes)
                    {
                        employeeTypesPrepend.Add(Program.GlobalConfig.EmployeeTypes[type]);
                    }

                    friendlyName += string.Join(", ", employeeTypesPrepend);
                    friendlyName += ")";
                }

                return friendlyName;
            }
        }
    }
}
