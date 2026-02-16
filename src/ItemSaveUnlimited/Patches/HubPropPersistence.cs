using BepInEx;
using HarmonyLib;
using Unity.Entities;
using YAPYAP;

namespace ItemSaveUnlimited.Patches;

internal static class HubPropPersistPatches
{

  [HarmonyPatch(typeof(HubPropsPersistence), nameof(HubPropsPersistence.OnStartServer)), HarmonyPostfix]
  internal static void SetValuesOnStart(HubPropsPersistence __instance)
  {
    __instance.maxPersistedProps = ItemSavePatchPlugin.MaxItemsConfig.Value;
    ItemSavePatchPlugin.Log.LogInfo($"Set max saved props to {ItemSavePatchPlugin.MaxItemsConfig.Value}");
  }
}
