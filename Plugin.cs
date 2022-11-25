using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace FishFix
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class FishFixPlugin : BaseUnityPlugin
    {
        internal const string ModName = "FishFix";
        internal const string ModVersion = "1.0.0";
        internal const string Author = "Azumatt";
        private const string ModGUID = Author + "." + ModName;
        private readonly Harmony _harmony = new(ModGUID);

        public static readonly ManualLogSource FishFixLogger =
            BepInEx.Logging.Logger.CreateLogSource(ModName);

        public void Awake()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            _harmony.PatchAll(assembly);
        }
    }

    [HarmonyPatch(typeof(Fish), nameof(Fish.Awake))]
    static class Fish_Awake_Patch
    {
        static void Postfix(Fish __instance)
        {
            if (__instance.m_pickupItem != null) return;
            __instance.m_pickupItem = __instance.gameObject;
        }
    }
}