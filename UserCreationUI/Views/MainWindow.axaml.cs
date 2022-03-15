using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using ReactiveUI;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using UserCreationUI.Utilities;
using UserCreationUI.ViewModels;

namespace UserCreationUI.Views
{
    public class MainWindow : ReactiveWindow<MainWindowViewModel>
    {

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            this.Closing += async (s, e) =>
            {
                if (Application.Current is null || Application.Current.ApplicationLifetime is null)
                    return;
                var appLifetime = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
                var openWindows = appLifetime.Windows.ToList();
                var nonMainWindows = openWindows.Select(w => w.DataContext).Where(x => !(x is MainWindowViewModel));

                if (ViewModel is not null && nonMainWindows.Any())
                {
                    e.Cancel = true;

                    var confirmDialog = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
                        new MessageBoxStandardParams
                        {
                            ButtonDefinitions = ButtonEnum.OkAbort,
                            ContentTitle = "Program Closing Confirmation",
                            ContentMessage = "You currently have other windows open.\n Continuing to close the main app will shutdown the entire program.\n Are you sure you want to continue?",
                            Icon = MessageBox.Avalonia.Enums.Icon.Stop,
                            ShowInCenter = true,
                            Topmost = true
                        });
                    var result = await confirmDialog.ShowDialog(this);

                    if (result == ButtonResult.Ok)
                        UIFunctions.CloseAllWindows();
                }
            };

            this.WhenActivated(disposables =>
            {
                if (ViewModel is not null)
                {
                    disposables(
                        ViewModel
                        .OpenWindow
                        .RegisterHandler(interaction =>
                        {
                            var window = new SettingsWindow
                            {
                                DataContext = interaction.Input
                            };
                            window.Show();
                            interaction.SetOutput(Unit.Default);
                        }));
                }
            });
        }
    }
}
