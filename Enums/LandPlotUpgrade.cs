﻿using CustomSpawnerUpgrade.Assist;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS0649
namespace CustomSpawnerUpgrade.Enums
{
    internal class LandPlotUpgrade
    {
        public static LandPlot.Upgrade SPAWNER_UPGRADE;

        public static void Initialize()
        {
            foreach (FieldInfo fieldInfo in typeof(LandPlotUpgrade).GetFields())
            {
                object val = EnumPatchHelper.AddEnumValue(fieldInfo.FieldType, fieldInfo.Name);
                fieldInfo.SetValue(null, val);
                MelonLogger.Msg($"[{typeof(LandPlotUpgrade).Name}] Initialized Modded Enum -> " + fieldInfo.Name);
            }
        }
    }
}