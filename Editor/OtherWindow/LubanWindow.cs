using FEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace FEngineEditor
{
    [LabelText("LubanHelper")]
    public class LubanWindow:OdinEditorWindow
    {
        [InfoBox("Luban默认安装路径在Asset同级目录下")]
        [Button("通过Luban生成数据类")]
        public static void GenerateData()
        {
            FSystem.RunProcess(Application.dataPath + "/../Luban/gen_code_json.bat","");
        }
        [Button("打开Excel")]
        public static void Open()
        {
            FSystem.OpenFolder(Application.dataPath + "/../Luban/Config/Datas");
        }
        [Button("打开文件模板")]
        public static void OpenConfig()
        {
            FSystem.OpenFolder(Application.dataPath +@"/../Luban\Luban.ClientServer\Templates\config\cs_unity_json");
        }
    }
}