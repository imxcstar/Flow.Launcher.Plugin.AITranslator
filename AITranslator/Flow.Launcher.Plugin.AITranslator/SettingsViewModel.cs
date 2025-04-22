using System.ComponentModel;

namespace Flow.Launcher.Plugin.AITranslator;

public class SettingsViewModel : BaseModel
{
    public SettingsModel Settings { get; set; }
    
    public SettingsViewModel(SettingsModel settingsModel)
    {
        Settings = settingsModel;
    }
}