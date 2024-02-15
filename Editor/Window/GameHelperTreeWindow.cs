using System;
using System.Linq;
using FEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace FEngineEditor
{
    public class GameHelperTreeWindow : OdinMenuEditorWindow
    {
        private static GameHelperTreeWindow _treeWindow;
        private OdinMenuTree tree;
        [MenuItem("FTool/GameTools")]
        public static void Open()
        {
            _treeWindow ??= GetWindow<GameHelperTreeWindow>();
            _treeWindow.Show();
        }

        [Button("打开GraphWindow")]
        public void OpenGraphWindow()
        {
            MyGraphWindow.Open();
        }

        [Button]
        public void Test()
        {
            Debug.Log($"{ tree.Config}  { tree.Selection }");
        
        }
   
        protected override OdinMenuTree BuildMenuTree()
        {
            tree = new OdinMenuTree()
            {
                { "Home", this, EditorIcons.House }, // Draws the this.someData field in this case.
                // { "Player Settings", Resources.FindObjectsOfTypeAll<PlayerSettings>().FirstOrDefault() }
            };
            tree.Config.DrawSearchToolbar = true;

            tree.DrawSearchToolbar(); 
            tree.AddAllAssetsAtPath("配置/", "Assets/FEngine/Config", typeof(ScriptableObject), true)
                .AddThumbnailIcons();   
            
            tree.Add("TreeConfig",tree.Config);
            tree.Add("TreeSelection",tree.Selection);
            
            //把继承了OdinWindow的添加到树里
            var windows = GetType().Assembly.GetTypes()
                .Where(e=>e.BaseType == typeof(OdinEditorWindow))
                .Where(e => e.GetCustomAttribute<LabelTextAttribute>() != null)
                .Select(e => new {e.GetCustomAttribute<LabelTextAttribute>().Text,e})
                .ToList();
            
            foreach (var window in windows)
            {
                tree.Add(window.Text,CreateInstance(window.e));
            }
            
            // tree.SortMenuItemsByName();

            return tree;
        }
    }
}