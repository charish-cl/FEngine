using System;

namespace FEngine
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SceneRightClickMenuItemAttribute : Attribute
    {
        public string MenuItemName { get; }

        public SceneRightClickMenuItemAttribute(string menuItemName)
        {
            MenuItemName = menuItemName;
        }
    }
}