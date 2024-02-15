using Sirenix.OdinInspector;
using UnityEngine;

namespace FEngine
{
    [CreateAssetMenu]
    public class FConfig:ScriptableObject
    {
        [LabelText("SVN Path")]
        [SerializeField]
        [FolderPath]
        public string SVNProjectPath;
        
        public static FConfig GetConfig()
        {
            return FResources.LoadAssetAtPath<FEngine.FConfig>("Assets/FEngine/Config/FConfig.asset") as FConfig;
        }
    }
}