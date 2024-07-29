using HarmonyLib;
using CustomSpawnerUpgrade.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSpawnerUpgrade.Harmony
{
    [HarmonyPatch(typeof(LandPlot), nameof(LandPlot.ApplyUpgrades))]
    internal static class LandPlotApplyUpgradesPatch
    {
        public static void Postfix(LandPlot __instance, Il2CppSystem.Collections.Generic.IEnumerable<LandPlot.Upgrade> upgrades)
        {
            Il2CppSystem.Collections.Generic.List<LandPlot.Upgrade> plotUpgrades = new(upgrades);
            SpawnerUpgrader spawnerPlotUpgrader = __instance.GetComponent<SpawnerUpgrader>();

            if (spawnerPlotUpgrader)
                foreach (var upgrade in plotUpgrades)
                    spawnerPlotUpgrader.Apply(upgrade);
        }
    }
}
