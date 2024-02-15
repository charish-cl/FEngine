using FEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace FEngineEditor
{
    [LabelText("特殊文件夹")]
    public class FileWindow:OdinEditorWindow
    {
        [Button("打开manifest")]
        public void OpenMain()
        {
            FSystem.OpenFolder(Application.dataPath + "/../Packages/manifest.json");
        }
    }
}