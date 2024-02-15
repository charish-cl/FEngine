using System.Collections.Generic;
using System.IO;
using System.Linq;
using FEngine;
using UnityEditor;
using UnityEngine;

namespace FEngineEditor
{
    public class SceneUtility
    {
        /// <summary>
        /// 打开场景
        /// </summary>
        /// <param name="scenePath"></param>
        public static void OpenScene(string scenePath)
        {
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene(scenePath);
        }
        
        //控制Camera Align View with scene view
        [SceneRightClickMenuItem("同步相机视图")]
        public static void LockViewToFrame()
        {
            Camera.main.transform.position = SceneView.lastActiveSceneView.camera.transform.position;
            Camera.main.transform.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
        }
        
    }
}