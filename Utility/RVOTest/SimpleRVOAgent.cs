using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FEngine.Utility
{
    //实现RVO算法的Agent,包含一个目标位置,在Update中改变位置,使其朝向目标位置,如果目标位置为null,则不移动,并且在OnDestroy中取消订阅
    //同时有一个tag属性,为友方的tag,进行动态避让
    public class SimpleRVOAgent : SimpleUnit
    {
        [TabGroup("RVO")]
        [ValueDropdown("GetTags")]
        public string targetTag;
        private Transform _target;
        
        [TabGroup("RVO")]
        //友方tag
        [ValueDropdown("GetTags")]
        public string friendTag;
        
        //所有的友方
        private static GameObject[] friends;
        //动态避让的距离
        [TabGroup("RVO"),SerializeField] private float avoidDistance = 1;
        //距离目标多少距离时停止移动
        [TabGroup("RVO"),SerializeField] private float pauseDistance;

        //计算下一帧应该移动的位置,RVO算法
        private Vector3 CalculateNextPosition()
        {
            var avoidVector = Vector3.zero;
            foreach (var friend in friends)
            {
                var distance = Vector3.Distance(transform.position, friend.transform.position);
                if (distance < avoidDistance)
                {
                    var diff = transform.position - friend.transform.position;
                    avoidVector += diff.normalized * avoidDistance;
                }
            }
            // Check if distance to target is less than or equal to desired distance to pause
            if (Vector3.Distance(transform.position, _target.position) <= pauseDistance)
            {
                return transform.position;
            }
            else
            {
                return Vector3.MoveTowards(transform.position, _target.position, speed * Time.deltaTime) + avoidVector;
            }
        }
        //start中开启协程,不断执行CalculateNextPosition,不断获取下一帧的位置,并且移动
        private async void Start()
        {
            _target = GameObject.FindGameObjectWithTag(targetTag).transform;
            friends = GameObject.FindGameObjectsWithTag(friendTag);

            while (true)
            {
                transform.position = CalculateNextPosition();
                await UniTask.Yield();
            }
           
        }
       
        
    }
 
}