using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class FollowText:MonoBehaviour
    {
        private Transform target;
        public  TextMeshProUGUI text;
   
        
        public void SetText(string s)
        {
            
            text.text = s;
            text.text = "";
        }       
        public void SetTarget(Transform t)
        {
            target = t;
        }
        
        public void SetDisplay(bool isDisplay)
        {
            text.gameObject.SetActive(isDisplay);
        }
        private void LateUpdate()
        {
            if (target)
            {
                var targetPosition = target.position;
                transform.position =Camera.main.WorldToScreenPoint(targetPosition);
            }
        }
    }
}