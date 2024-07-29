using Il2CppMonomiPark.SlimeRancher.Pedia;
using Il2CppMonomiPark.SlimeRancher.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Localization;
using Il2CppMonomiPark.SlimeRancher;
using CustomSpawnerUpgrade.Harmony;

namespace CustomSpawnerUpgrade.Assist
{
    public static class GeneralizedHelper
    {
        public static LocalizedString CreateTranslation(string table, string key, string localized) => LocalizationDirectorLoadTablesPatch.AddTranslation(table, key, localized);
    }
}
