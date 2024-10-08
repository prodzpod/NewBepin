using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using Mono.Cecil;

public static class Patcher
{
    public const string GUID = "_.prodzpod.RemoveOldCecil";
    public static ManualLogSource Log;
    public static void Initialize()
    {
        Log = Logger.CreateLogSource(GUID);
        var dir = Path.Combine(Paths.GameRootPath, "CoreKeeper_Data\\Managed");
        string[] files = ["Mono.Cecil.dll", "Mono.Cecil.Mdb.dll", "Mono.Cecil.Pdb.dll", "Mono.Cecil.Rocks.dll"];
        files = files.Select(x => Path.Combine(dir, x)).ToArray();
        if (files.Any(File.Exists))
        {
            Log.LogInfo("Old Cecil Detected, Launching Deleter...");
            var process = Process.Start(new ProcessStartInfo()
            {
                FileName = Assembly.GetExecutingAssembly().Location.Replace("RemoveOldCecil.dll", "RemoveOldCecilConsole.exe"),
                Arguments = string.Join(" ", files.Select(x => $"\"{x}\"").ToArray()),
                CreateNoWindow = false
            });
            Log.LogInfo("Deleter Launched.");
        }
    }
    public static IEnumerable<string> TargetDLLs { get; } = [];
    public static void Patch(AssemblyDefinition _) { }
}