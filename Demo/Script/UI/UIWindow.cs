using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public partial class UIWindow : MonoBehaviour
    {
        public bool IsPointerOnUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}