using System.Windows.Controls;

namespace Flow.Launcher.Plugin.AITranslator;

public partial class Settings : UserControl
{
    private readonly SettingsViewModel _viewModel;
    private readonly SettingsModel _settings;

    public Settings(SettingsViewModel viewModel)
    {
        _viewModel = viewModel;
        _settings = viewModel.Settings;
        DataContext = viewModel;
        InitializeComponent();
    }
}