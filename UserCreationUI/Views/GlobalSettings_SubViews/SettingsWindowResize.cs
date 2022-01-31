using Avalonia;
using Avalonia.Collections;
using Avalonia.Media;
using Avalonia.Rendering;
using Avalonia.VisualTree;
using System;
using UserCreationUI.Views;

namespace UserCreationUI.GlobalSettings.Views
{
    public class SettingsWindowResize
    {
        public static void ResizeWindow(IVisual self, double SWidth, double SHeight, double? SMinWidth = null, double? SMinHeight = null, double? SMaxWidth = null, double? SMaxHeight = null)
        {
            SettingsWindow settingsWindow = self.FindAncestorOfType<SettingsWindow>();
            
            if (SMinHeight.HasValue && SMinWidth.HasValue)
            {
                if (SMaxHeight.HasValue && SMaxWidth.HasValue)
                {
                    settingsWindow.ResizeWindow(SWidth, SHeight, SMinWidth.Value, SMinHeight.Value, SMaxWidth.Value, SMaxHeight.Value);
                }
                else
                {
                    settingsWindow.ResizeWindow(SWidth, SHeight, SMinWidth.Value, SMinHeight.Value);
                }
            } 
            else
            {
                settingsWindow.ResizeWindow(SWidth, SHeight);
            }
        }
    }
}
