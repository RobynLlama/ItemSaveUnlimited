using System.IO;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using ItemSaveUnlimited.Patches;
using YAPYAP;

namespace ItemSaveUnlimited;

[BepInAutoPlugin]
public partial class ItemSavePatchPlugin : BaseUnityPlugin
{
  internal static ManualLogSource Log { get; private set; }
  internal static ConfigEntry<int> MaxItemsConfig;

  private void Awake()
  {
    Log = Logger;

    var cfg = new ConfigFile(Path.Combine(Paths.ConfigPath, Id + ".cfg"), true, Info.Metadata);
    MaxItemsConfig = cfg.Bind("ItemSavePatch", "ItemSaveMax", 50, "Sets the maximum number of save props in the save file, the higher this is the bigger your save will be, may also add some lag when players join and items are sync'd to clients");

    Harmony patcher = new(Id);
    patcher.PatchAll(typeof(HubPropPersistPatches));

    Log.LogInfo($"Plugin {Name} patched {patcher.GetPatchedMethods().Count()} methods");
  }
}
