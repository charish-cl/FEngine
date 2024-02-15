using UnityEngine;

namespace FEngine
{
    public static class F2DPhysic
    {
  
        /// <summary>
        /// 检查碰撞体挤压
        /// </summary>
        /// <param name="collider2D"></param>
        /// <param name="other"></param>
        /// <param name="thresheld"></param>
        /// <returns></returns>
        public static bool  CheckExtrusion(this Collider2D collider2D,Collision2D other,float thresheld)
        {
            var bounds = collider2D.bounds;
            var center=bounds.center;
            var width = bounds.size.x;
            var height = bounds.size.y;
            var target = other.collider.bounds.center;
            if ((Mathf.Abs(target.x - center.x) < (width / 2 - thresheld))&&Mathf.Abs(target.y-center.y)< height / 2 - thresheld)
            {
                return true;
            }
            return false;
        }
    }
}