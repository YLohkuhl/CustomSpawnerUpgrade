global using Il2Cpp;
global using UnityEngine;
global using static Utility;
using Il2CppInterop.Runtime.Injection;
using MelonLoader;
using CustomSpawnerUpgrade;
using CustomSpawnerUpgrade.Data.Upgrades;
using Il2CppInterop.Runtime;
using HarmonyLib;
using CustomSpawnerUpgrade.Enums;

[assembly: MelonInfo(typeof(SpawnerEntry), "Custom Spawner Upgrade", "1.0.0", "YLohkuhl")]
[assembly: MelonGame("MonomiPark", "SlimeRancher2")]
[assembly: MelonColor(0, 238, 171, 68)]
namespace CustomSpawnerUpgrade
{
    internal class SpawnerEntry : MelonMod
    {
        public static SlimeDefinition[] SpawnableSlimeDefinitions = [];

        public override void OnInitializeMelon()
        {
            // PREFERENCES
            SpawnerPreferences.Initialize();

            // ENUMS
            LandPlotUpgrade.Initialize();
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            SpawnerUpgrade.Load(sceneName);

            switch (sceneName)
            {
                case "zoneCore":
                    {
                        foreach (SlimeDefinition slimeDefinition in Resources.FindObjectsOfTypeAll<SlimeDefinition>())
                            if (SpawnerPreferences.SpawnableSlimes.Value.Contains(slimeDefinition.name))
                                if (!SpawnableSlimeDefinitions.Any(x => x == slimeDefinition))
                                    SpawnableSlimeDefinitions = SpawnableSlimeDefinitions.AddToArray(slimeDefinition);
                        break;
                    }
            }
        }
    }
}
