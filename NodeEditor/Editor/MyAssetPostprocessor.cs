﻿using UnityEditor;
using UnityEngine;

namespace FEngine.NodeEditor
{
    public class MyAssetPostprocessor:AssetPostprocessor
    {
        
        void OnMouseDown()
        {

            if (Selection.activeObject == null)
            {
                return;
            }
            // if (Selection.activeObject is NodeDataBase)
            // {
            //     Debug.Log("点开");
            //     return;
            // }
        }
    }
}