using MelonLoader;
using MelonLoader.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSpawnerUpgrade
{
    internal class SpawnerPreferences
    {
        public static MelonPreferences_Category Preferences { get; protected set; }

        public static MelonPreferences_Entry<string[]> SpawnableSlimes { get; protected set; }
        public static MelonPreferences_Entry<int> GlobalTargetCount { get; protected set; }

        public static void Initialize()
        {
            Preferences = MelonPreferences.CreateCategory("CustomSpawnerUpgrade");

            SpawnableSlimes = Preferences.CreateEntry<string[]>("SpawnableSlimes", ["Pink", "Cotton", "Tabby", "Rock"], "Spawnable Slimes",
                "Edit in the names of slimes into this array of strings in order to add them to spawn list for the upgrade.\n\n" +
                "Simply follow the similar format and specify the name of the slime, do not put 'Slime' afterwards. For example 'Pink' would do.\n\n" +
                "In any case where it doesn't, it must be a modded slime which does not follow the original naming that is used by the game and would have to be figured out separately.\n\n" +
                "Consider reloading the game after each change to the configuration!"
            );

            GlobalTargetCount = Preferences.CreateEntry("GlobalTargetCount", 12, "Global Target Slime Count",
                "The game has a limit on how many slimes can be in one area at once. If this upgrade is ever applied, it will modify this and change it to your preferred amount.\n\n" +
                "This may need to be changed if slimes are already within the area or if you generally just want more slimes to spawn than usual.\n\n" +
                "Try changing it! Though be careful not to go overboard!"
            );

            Preferences.SetFilePath(Path.Combine(MelonEnvironment.UserDataDirectory, "SpawnerPreferences.cfg"));
            Preferences.SaveToFile();
        }
    }
}
