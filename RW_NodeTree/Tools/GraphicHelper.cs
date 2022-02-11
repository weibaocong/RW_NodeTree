﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RW_NodeTree.Tools
{
    public static class GraphicHelper
    {

        public static Graphic subGraphic(this Graphic parent)
        {
            FieldInfo fieldInfo = parent.GetType().GetField("subGraphic", AccessTools.all);
            if(fieldInfo != null)
            {
                return fieldInfo.GetValue(parent) as Graphic;
            }
            return null;
        }

        public static Graphic_ChildNode GetGraphic_ChildNode(this Graphic parent)
        {
            Graphic graphic = parent;
            while (parent != null && !(parent is Graphic_ChildNode))
            {
                parent = parent.subGraphic();
            }
            return graphic as Graphic_ChildNode;
        }
    }
}