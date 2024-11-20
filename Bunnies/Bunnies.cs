using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using Bunnies.Windows;
using Microsoft.VisualBasic;

namespace Bunnies;

public sealed class Bunnies : IDalamudPlugin
{
    [PluginService] internal static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
    [PluginService] internal static ITextureProvider TextureProvider { get; private set; } = null!;
    [PluginService] internal static ICommandManager CommandManager { get; private set; } = null!;

    private const string CommandMainName = "/bunnies";
    private const string CommandSettingName = "/bunniesconfig";


    public Configuration Configuration { get; init; }

    public readonly WindowSystem WindowSystem = new("Josh's Plugin");
    private BunniesConfigWindow BunniesConfigWindow { get; init; }
    private BunniesMainWindow BunniesMainWindow { get; init; }


    public Bunnies()
    {
        Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();

        // you might normally want to embed resources and load them from the manifest stream

        BunniesConfigWindow = new BunniesConfigWindow(this);
        BunniesMainWindow = new BunniesMainWindow(this);

        WindowSystem.AddWindow(BunniesConfigWindow);
        WindowSystem.AddWindow(BunniesMainWindow);

        CommandManager.AddHandler(CommandMainName, new CommandInfo(OnCommand)
        {
            HelpMessage = "This is my command, nobody use it!"
        });

        CommandManager.AddHandler(CommandSettingName, new CommandInfo(SettingCommand)
        {
            HelpMessage = "This is my setting command!"
        });

        PluginInterface.UiBuilder.Draw += DrawUI;

        // This adds a button to the plugin installer entry of this plugin which allows
        // to toggle the display status of the configuration ui
        PluginInterface.UiBuilder.OpenConfigUi += ToggleConfigUI;

        // Adds another button that is doing the same but for the main ui of the plugin
        PluginInterface.UiBuilder.OpenMainUi += ToggleMainUI;
    }

    public void Dispose()
    {
        WindowSystem.RemoveAllWindows();

        BunniesConfigWindow.Dispose();
        BunniesMainWindow.Dispose();

        CommandManager.RemoveHandler(CommandMainName);
        CommandManager.RemoveHandler(CommandSettingName);

    }

    private void OnCommand(string command, string args)
    {
        // in response to the slash command, just toggle the display status of our main ui
        ToggleMainUI();
    }

    private void SettingCommand(string command, string args)
    {
        ToggleConfigUI();
    }

    private void DrawUI() => WindowSystem.Draw();
    public void ToggleConfigUI() => BunniesConfigWindow.Toggle();
    public void ToggleMainUI() => BunniesMainWindow.Toggle();
}