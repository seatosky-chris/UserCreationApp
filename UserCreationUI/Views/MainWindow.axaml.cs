using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;
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

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
