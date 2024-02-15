using Sirenix.OdinInspector;
using UnityEngine;

namespace FEngine
{
    public class DataBaseManager
    {
        //获取DataBase
        public static T GetDataBase<T>() where T : SerializedBehaviour
        {
            var name = typeof(T).Name;
            return FResources.LoadAssetAtPath<T>("Assets/FEngine/DataBase/" + name + ".asset");
        }
    }
}