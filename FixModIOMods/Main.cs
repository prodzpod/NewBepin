using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoslynCSharp.Compiler;
using System;
using System.Reflection;

namespace FixModIOMods
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class Main : BaseUnityPlugin
    {
        public const string PluginGUID = "000." + PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "prodzpod";
        public const string PluginName = "FixModIOMods";
        public const string PluginVersion = "1.0.0";
        public static ManualLogSource Log;
        public static PluginInfo pluginInfo;
        public static Harmony Harmony;
        public static ConfigFile Config;
        public static Main Instance;

        public void Awake()
        {
            pluginInfo = Info;
            Log = Logger;
            Instance = this;
            Harmony = new(PluginGUID);
            // Config = new ConfigFile(System.IO.Path.Combine(Paths.ConfigPath, PluginGUID + ".cfg"), true);
            Harmony.PatchAll(typeof(MonoIssue));
        }

        public class MonoIssue
        {
            public static string LastLocation = "";
            public static void ILManipulator(ILContext il, MethodBase original, ILLabel retLabel) 
            {
                ILCursor c = new(il);
                while (c.TryGotoNext(MoveType.After, x => x.MatchCallOrCallvirt<Assembly>("get_" + nameof(Assembly.Location))))
                {
                    c.Emit(OpCodes.Ldarg_1);
                    c.EmitDelegate<Func<string, Assembly, string>>((res, self) =>
                    {
                        if (!string.IsNullOrEmpty(res)) LastLocation = res;
                        else
                        {
                            var name = LastLocation[0..LastLocation.LastIndexOf('\\')] + "\\" + self.GetName().Name + ".dll";
                            Log.LogInfo("Fixing " + name);
                            return name;
                        }
                        return res;
                    });
                }
            }
            public static MethodBase TargetMethod()
            {
                return typeof(AssemblyReferenceFromAssemblyObject).GetConstructor([typeof(Assembly)]);
            }
        }
    }
}
