namespace Flow.Launcher.Plugin.AITranslator;

public class SettingsModel
{
    public string Api { get; set; } = "https://api.openai.com";

    public string ApiKey { get; set; } = "";

    public string Model { get; set; } = "gpt-4.1-nano";

    public string TargetLanguage { get; set; } = "zh-cn";
}