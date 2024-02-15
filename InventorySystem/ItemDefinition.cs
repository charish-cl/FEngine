using Sirenix.OdinInspector;
using UnityEngine;

namespace FEngine.InventorySystem
{
    public class ItemDefinition : BaseData
    {
        [LabelText("UIIcon")]
        public Sprite UISprite;

        [LabelText("是否可堆叠")]
        public bool IsStackable;
    }
}