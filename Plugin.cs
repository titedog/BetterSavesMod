using BepInEx;
using HarmonyLib;
using Gloomwood.UI;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Linq;
using Gloomwood.Saving;

namespace BetterSavesMod
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static readonly int MaxSaveSlots = 100;

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            Harmony.CreateAndPatchAll(typeof(Plugin));
        }

        [HarmonyPatch(typeof(LoadGameMenu), "UpdateSaveSlots")]
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> LoadGameMenu_UpdateSaveSlots_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            for (var i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldc_I4_S)
                {
                    codes[i].operand = Plugin.MaxSaveSlots;
                }
            }

            return codes.AsEnumerable();
        }

        [HarmonyPatch(typeof(SaveGameMenu), "UpdateSaveSlots")]
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> SaveGameMenu_UpdateSaveSlots_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            for (var i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldc_I4_S)
                {
                    codes[i].operand = Plugin.MaxSaveSlots;
                }
            }

            return codes.AsEnumerable();
        }

        [HarmonyPatch(typeof(SaveLoadManager), "FindSaveFiles")]
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> SaveLoadManager_FindSaveFiles_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            for (var i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldc_I4_S)
                {
                    codes[i].operand = Plugin.MaxSaveSlots + 1;
                }
            }

            return codes.AsEnumerable();
        }
    }
}
