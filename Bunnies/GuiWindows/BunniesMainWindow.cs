﻿using System;
using System.Numerics;
using Dalamud.Interface.Internal;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using ImGuiNET;
namespace Bunnies.Windows;
public class BunniesMainWindow : Window, IDisposable
{
    private Bunnies Bunnies;
    // We give this window a hidden ID using ##
    // So that the user will see "My Amazing Window" as window title,
    // but for ImGui the ID is "My Amazing Window##With a hidden ID"
    public BunniesMainWindow(Bunnies plugin)
        : base("My Amazing Bunny!##With a hidden ID", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };
        Bunnies = plugin;
    }
    public void Dispose() { }
    public override void Draw()
    {
        //ImGui.Text($"The random config bool is {Plugin.Configuration.SomePropertyToBeSavedAndWithADefault}");
        if (ImGui.Button("Show Bunny Settings"))
        {
            Bunnies.ToggleConfigUI();
        }
    }
}