using HarmonyLib;
using Il2CppMonomiPark.SlimeRancher.Regions;
using Il2CppSystem;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CustomSpawnerUpgrade.Enums.LandPlotUpgrade;
using static CustomSpawnerUpgrade.SpawnerEntry;

namespace CustomSpawnerUpgrade.Components
{
    [RegisterTypeInIl2Cpp]
    internal class SpawnerUpgrader : MonoBehaviour
    {
        private CellDirector CellDirector { get; set; }

        public static SlimeSet.Member[] SlimeSetMembers = [];
        public GameObject nodeSlimeUpgrade;
        public bool isSpawnerSetup;

        void Start()
        {
            CellDirector = GetComponentInParent<CellDirector>();
            if (CellDirector)
                if (CellDirector.TargetSlimeCount != SpawnerPreferences.GlobalTargetCount.Value)
                    CellDirector.TargetSlimeCount = SpawnerPreferences.GlobalTargetCount.Value;

            foreach (var slimeDefinition in SpawnableSlimeDefinitions)
            {
                if (SlimeSetMembers.Any(x => x.IdentType == slimeDefinition))
                    continue;

                SlimeSetMembers = SlimeSetMembers.AddToArray(new()
                {
                    Weight = 1,
                    _prefab = slimeDefinition.prefab,
                    IdentType = slimeDefinition,
                });
            }
        }

        void OnDestroy()
        {
            CellDirector?._spawners?.Remove(nodeSlimeUpgrade?.GetComponent<DirectedSlimeSpawner>());
            isSpawnerSetup = false;
        }

        public void Apply(LandPlot.Upgrade upgrade)
        {
            if (upgrade == SPAWNER_UPGRADE)
                SetupSpawner();
        }

        public void SetupSpawner()
        {
            if (isSpawnerSetup)
                return;

            DirectedActorSpawner.SpawnConstraint[] spawnConstraints = [
                new DirectedActorSpawner.SpawnConstraint()
                {
                    Window = new()
                    {
                        TimeMode = DirectedActorSpawner.TimeMode.ANY
                    },
                    Slimeset = new()
                    {
                        Members = SlimeSetMembers.ToArray()
                    },
                    Weight = 1
                }
            ];

            GameObject baseNodeSlime = GameObject.Find("zoneFields_Area1/cellRainbowExpanse/Sector/Slimes/nodeSlime (5)");
            nodeSlimeUpgrade = Instantiate(baseNodeSlime);
            nodeSlimeUpgrade.SetActive(false);

            nodeSlimeUpgrade.name = "nodeSlimeUpgrade";
            nodeSlimeUpgrade.transform.parent = transform;
            nodeSlimeUpgrade.transform.position = transform.position;
            nodeSlimeUpgrade.transform.rotation = Quaternion.identity;
            Destroy(nodeSlimeUpgrade.GetComponent<DirectedSlimeSpawner>());

            DirectedSlimeSpawner directedSlimeSpawner = nodeSlimeUpgrade.AddComponent<DirectedSlimeSpawner>();

            directedSlimeSpawner.Radius = 2.5f;
            directedSlimeSpawner._region = GetComponentInParent<Region>();
            directedSlimeSpawner._spawnFX = baseNodeSlime.GetComponent<DirectedSlimeSpawner>()._spawnFX;
            directedSlimeSpawner.SlimeSpawnFX = baseNodeSlime.GetComponent<DirectedSlimeSpawner>().SlimeSpawnFX;

            directedSlimeSpawner.Constraints = spawnConstraints;
            directedSlimeSpawner.EnableToteming = true;
            directedSlimeSpawner.AllowDirectedSpawns = true;

            nodeSlimeUpgrade.SetActive(true);
            isSpawnerSetup = true;
        }
    }
}
