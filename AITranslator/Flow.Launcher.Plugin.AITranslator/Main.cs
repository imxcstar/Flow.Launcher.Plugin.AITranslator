using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Flow.Launcher.Plugin;
using JetBrains.Annotations;
using OpenAI;
using OpenAI.Chat;

namespace Flow.Launcher.Plugin.AITranslator
{
    public class AITranslator : IAsyncPlugin, IPluginI18n, ISettingProvider
    {
        private PluginInitContext _context;
        private static SettingsModel _settings;
        private static SettingsViewModel _viewModel;
        private static ChatClient _client;

        public Task InitAsync(PluginInitContext context)
        {
            _context = context;
            _settings = context.API.LoadSettingJsonStorage<SettingsModel>();
            _viewModel = new SettingsViewModel(_settings);
            _client = new ChatClient(model: _settings.Model, new ApiKeyCredential(_settings.ApiKey),
                new OpenAIClientOptions()
                {
                    Endpoint = new Uri(_settings.Api)
                });
            return Task.CompletedTask;
        }

        public async Task<List<Result>> QueryAsync(Query query, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(query.Search.Replace(" ", "")))
                return new List<Result>();
            await Task.Delay(300, token);
            var completion =
                await _client.CompleteChatAsync(
                    new List<ChatMessage>
                    {
                        $"翻译此内容到{_settings.TargetLanguage}语言，不要返回其它内容，只返回翻译结果。内容为：{query.Search}"
                    }, new ChatCompletionOptions(), token);

            var ret = completion.Value.Content[0].Text;
            return new List<Result>
            {
                new Result()
                {
                    IcoPath = "icon.png",
                    Title = ret,
                    CopyText = ret,
                    Action = c =>
                    {
                        _context.API.CopyToClipboard(ret);
                        return true;
                    }
                }
            };
        }

        public string GetTranslatedPluginTitle()
        {
            return _context.API.GetTranslation("title");
        }

        public string GetTranslatedPluginDescription()
        {
            return _context.API.GetTranslation("description");
        }

        public Control CreateSettingPanel()
        {
            return new Settings(_viewModel);
        }
    }
}