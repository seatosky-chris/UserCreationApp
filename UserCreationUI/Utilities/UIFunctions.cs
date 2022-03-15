using Avalonia.Interactivity;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UserCreationUI.Utilities
{
    public static class UIFunctions
    {
        public static void UI_OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        // This functions can be used to stop an event from bubbling upwards
        public static void EventHandled(object? sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        // Used by the DoFilter function for filtering datagrids
        public static bool FilterDataGrid(object filterBy, ReactiveObject gridFilters)
        {
            foreach (var filterProp in gridFilters.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly))
            {
                var filterVal = filterProp.GetValue(gridFilters, null);
                if (filterVal is null || string.IsNullOrWhiteSpace(filterVal.ToString()))
                    continue;

                string? filterValString = filterVal.ToString();
                if (filterValString is null)
                    continue;

                var curValueProp = filterBy.GetType().GetProperty(filterProp.Name);
                if (curValueProp == null)
                    return false;

                var curValue = curValueProp.GetValue(filterBy, null);
                if (curValue is null)
                    return false;

                string? curValueString = curValue.ToString();
                if (curValueString is null || string.IsNullOrWhiteSpace(curValueString) || !curValueString.ToLower().Contains(filterValString.ToLower()))
                    return false;
            }

            return true;
        }
    }
}
