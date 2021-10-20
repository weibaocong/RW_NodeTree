﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace RW_NodeTree
{
    public class Comp_ThingsNode : ThingComp
    {
        /// <summary>
        /// get currect node of index
        /// </summary>
        /// <param name="index">node index</param>
        /// <returns>currect node</returns>
        public Comp_ThingsNode this[int index]
        {
            get
            {
                return childNodes[index];
            }
            set
            {
                if(AllowNode(value))
                {
                    if ((value?.parent?.Spawned).GetValueOrDefault()) value.parent.DeSpawn();
                    childNodes[index] = value;
                    UpdateNode();
                }
            }
        }

        /// <summary>
        /// find all comp for node
        /// </summary>
        public IEnumerable<ThingComp_BasicNodeComp> AllNodeComp
        {
            get
            {
                foreach (ThingComp_BasicNodeComp comp in parent.AllComps)
                {
                    if (comp != null)
                    {
                        yield return comp;
                    }
                }
                yield break;
            }
        }

        public Texture CombinedIconTexture(Rot4 rot)
        {
            Graphic graphic = base.parent.Graphic;
            if(graphic as )
        }

        /// <summary>
        /// all 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool AllowNode(Comp_ThingsNode node)
        {
            foreach (ThingComp_BasicNodeComp comp in AllNodeComp)
            {
                if (!comp.AllowNode(node)) return false;
            }
            return true;
        }

        public Vector2 IconTexturePostion(Rot4 rot)
        {
            Vector2 result = Vector2.zero;
            foreach (ThingComp_BasicNodeComp comp in AllNodeComp)
            {
                result += comp.IconTexturePostionOffset(rot);
            }
            return result;
        }

        public Comp_ThingsNode FindNode(Predicate<Comp_ThingsNode> func)
        {
            return childNodes.Find(func);
        }

        public void UpdateNode()
        {
            foreach(ThingComp_BasicNodeComp comp in AllNodeComp)
            {
                comp.UpdateNode();
            }
            foreach(Comp_ThingsNode node in childNodes)
            {
                node.UpdateNode();
            }
        }

        public static implicit operator ThingWithComps(Comp_ThingsNode node)
        {
            return node.parent;
        }

        public static explicit operator Comp_ThingsNode(ThingWithComps thing)
        {
            return thing.GetComp<Comp_ThingsNode>();
        }

        private bool onUpdateNode = false;

        private Comp_ThingsNode parentNode = null;

        private List<Comp_ThingsNode> childNodes = new List<Comp_ThingsNode>();
    }

    public class CompProperties_PartNode : CompProperties
    {

        /// <summary>
        /// graphic data used when draw on parent node
        /// </summary>
        public GraphicData partIconGraphic = null;

        /// <summary>
        /// offset(+-) value for armor penetration
        /// </summary>
        public float armorPenetrationOffset = 0;

        /// <summary>
        /// offset(+-) value for melee weapon cooldown time
        /// </summary>
        public float meleeCooldownTimeOffset = 0;

        /// <summary>
        /// offset(+-) value for melee weapon damage
        /// </summary>
        public float meleeDamageOffset = 0;

        /// <summary>
        /// Multiplier value for ticks between burst shots,faster when value smaller
        /// </summary>
        public float ticksBetweenBurstShotsMultiplier = 1f;

        /// <summary>
        /// Multiplier value for muzzle flash scale,brighter when value biger
        /// </summary>
        public float muzzleFlashScaleMultiplier = 1f;

        /// <summary>
        /// Multiplier value for shooting rang,farther when value biger
        /// </summary>
        public float RangMultiplier = 1f;

        /// <summary>
        /// offset(+-) value for burst shot count
        /// </summary>
        public float burstShotCountOffset = 0;

        /// <summary>
        /// Multiplier value for shooting warmup time(the time need to take before whepon shoting),longer when value biger
        /// </summary>
        public float warmupTimeMultiplier = 1f;

        /// <summary>
        /// if is true,weapon can conly shoot one round when magazine capacity is 0
        /// </summary>
        public bool forceBrushShotWithOutMagCE = false;

        /// <summary>
        /// it set to not be affected by other part of parent node
        /// </summary>
        public bool verbPropertiesAffectByOtherPart = true;

        /// <summary>
        /// projectile replacement
        /// </summary>
        public ThingDef forcedProjectile = null;

        /// <summary>
        /// shooting sound replacement
        /// </summary>
        public SoundDef forceSound = null;

        /// <summary>
        /// Cast tail sound replacement
        /// </summary>
        public SoundDef forceSoundCastTail = null;


        public List<StatModifier> statMultiplier = new List<StatModifier>();


        public List<StatModifier> statOffset = new List<StatModifier>();
    }
}