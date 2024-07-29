using Il2CppInterop.Runtime;
using Il2CppMonomiPark.SlimeRancher.Pedia;
using Il2CppMonomiPark.SlimeRancher.UI;
using Il2CppMonomiPark.SlimeRancher.UI.Plot;
using CustomSpawnerUpgrade.Assist;
using CustomSpawnerUpgrade.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CustomSpawnerUpgrade.Enums.LandPlotUpgrade;

namespace CustomSpawnerUpgrade.Data.Upgrades
{
    internal static class SpawnerUpgrade
    {
        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        PurchaseCost purchaseCost = PurchaseCost.CreateEmpty();
                        purchaseCost.newbuckCost = 10000;

                        PlotUpgradePurchaseItemModel spawnerUpgradeShopEntry = LandPlotUpgradeHelper.CreateUpgradeShopEntry(SPAWNER_UPGRADE, Get<Sprite>("iconSlimeYolky"), "Spawner Upgrade", purchaseCost,
                            GeneralizedHelper.CreateTranslation("Pedia", "m.upgrade.name.corral.spawner", "Custom Spawner"),
                            GeneralizedHelper.CreateTranslation("Pedia", "m.upgrade.desc.corral.spawner", "Places a customizable slime spawner within your Corral.\nCustomize via the <b>SpawnerPreferences.cfg</b> file in the <b>UserData</b> folder.")
                        );

                        spawnerUpgradeShopEntry._pediaEntry = Get<PediaEntry>("Corral");
                        spawnerUpgradeShopEntry.RegisterUpgradeShopEntry(Get<LandPlotUIRoot>("CorralUI"));

                        GameContext.Instance.LookupDirector.GetPlotPrefab(LandPlot.Id.CORRAL).AddComponent<SpawnerUpgrader>();
                        break;
                    }
            }
        }
    }
}
