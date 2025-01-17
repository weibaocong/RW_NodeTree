﻿using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RW_NodeTree.Patch
{

    [HarmonyPatch(typeof(ThingDef))]
    internal static partial class ThingDef_Patcher
    {
        [HarmonyPostfix]
        [HarmonyPatch(
            typeof(ThingDef),
            "SpecialDisplayStats",
            typeof(StatRequest)
            )]
        public static void PostThingDef_SpecialDisplayStats(ThingDef __instance, StatRequest req, ref IEnumerable<StatDrawEntry> __result)
        {
            __result = ((CompChildNodeProccesser)req.Thing)?.PostThingDef_SpecialDisplayStats(__instance, req, __result) ?? __result;
        }
    }
}

namespace RW_NodeTree
{
    /// <summary>
    /// Node function proccesser
    /// </summary>
    public partial class CompChildNodeProccesser : ThingComp, IThingHolder
    {


        /// <summary>
        /// event proccesser after ThingDef.SpecialDisplayStats
        /// (WARRING!!!: Don't invoke any method if thet will invoke ThingDef.SpecialDisplayStats)
        /// </summary>
        /// <param name="def">ThingDef instance</param>
        /// <param name="req">parm 'req' of ThingDef.SpecialDisplayStats()</param>
        /// <param name="result">result of ThingDef.SpecialDisplayStats</param>
        public IEnumerable<StatDrawEntry> PostThingDef_SpecialDisplayStats(ThingDef def, StatRequest req, IEnumerable<StatDrawEntry> result)
        {
            foreach (CompBasicNodeComp comp in AllNodeComp)
            {
                result = comp.PostThingDef_SpecialDisplayStats(def, req, result) ?? result;
            }
            return result;
        }
    }
    public abstract partial class CompBasicNodeComp : ThingComp
    {
        public virtual IEnumerable<StatDrawEntry> PostThingDef_SpecialDisplayStats(ThingDef def, StatRequest req, IEnumerable<StatDrawEntry> result)
        {
            return result;
        }
    }
}