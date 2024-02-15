using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FEngine.Utility
{
    //动态避障VO算法
    public class SimpleVO:SimpleUnit
    {
        [TabGroup("VO")]
        [ValueDropdown("GetTags")]
        public string targetTag;
        private Transform _target;
        
        [TabGroup("VO")]
        //友方tag
        [ValueDropdown("GetTags")]
        public string friendTag;
        
        //所有的友方
        private static GameObject[] friends;
        
        //动态避让的距离
        [TabGroup("VO"),SerializeField] private float avoidDistance = 1;



        private Rigidbody2D _rigidbody2D;
        //start中开启协程,不断执行CalculateNextPosition,不断获取下一帧的位置,并且移动
        private async void Start()
        {
            _target = GameObject.FindGameObjectWithTag(targetTag).transform;
            friends = GameObject.FindGameObjectsWithTag(friendTag);
            //初始化速度,朝向目标
            _rigidbody2D=GetComponent<Rigidbody2D>();
            
            while (true)
            {
                CalculateNextVelocity();
                await UniTask.Yield();
            }
           
        }

        //Velocity Obstacle算法计算下一帧的位置
        private void CalculateNextVelocity()
        {
             //目标到自身的向量
            var targetVector = _target.position - transform.position;
            
            _rigidbody2D.velocity = (_target.position - transform.position).normalized * speed;

            bool canMove = true;
            //获取所有距离小于避障距离的友方
            foreach (var friend in friends)
            {
                var distance = Vector3.Distance(transform.position, friend.transform.position);
                if (distance < avoidDistance)
                {
                    //获取自身的CircleColider半径与友方的CircleColider半径,
                    //以自身为质点,自身半径与友方半径为半径的圆,质点到圆的切线,获取自身速度与友方速度的向量,判断向量是否在两条切线之间
                    //如果在两条切线之间,则计算出自身的速度,并且移动  
                    var selfRadius = GetComponent<CircleCollider2D>().radius;
                    var friendRadius = friend.GetComponent<CircleCollider2D>().radius;
                    var selfSpeed = GetComponent<Rigidbody2D>().velocity.XYZ() + friend.transform.position;
                    var friendSpeed = friend.GetComponent<Rigidbody2D>().velocity.XYZ() + friend.transform.position;
                    var targetCircle = new Circle(friend.transform.position, selfRadius+friendRadius);

                    //与友方的相对速度
                    var relativeSpeed = selfSpeed - friendSpeed;
                    //不断地减小自身的速度,判断是否在两条切线之间,直到不在两条切线之间
                    for (int i = 0; i < 10; i++)
                    {
                        if (IsBetween(relativeSpeed, GetTangent(relativeSpeed, targetCircle).Item2))
                        {
                            relativeSpeed = relativeSpeed.normalized * (relativeSpeed.magnitude - 1f);
                        }
                    }
                   
                    if (relativeSpeed.magnitude<=0)
                    {
                        continue;
                    }
                    //计算出自身的速度
                    var selfVelocity = relativeSpeed + friendSpeed;
                    
                    _rigidbody2D.velocity = (selfVelocity- transform.position);
                }
            }
        }
        //判断点是否在两条切线之间
        private bool IsBetween(Vector3 point, Vector3[] tangent)
        {
            var angle1 = Vector3.Angle(tangent[0], tangent[1]);
            var angle2 = Vector3.Angle(tangent[0], point);
            var angle3 = Vector3.Angle(tangent[1], point);
            return Math.Abs(angle1 - (angle2 + angle3)) < float.Epsilon;
        }
        public  (Vector3[], Vector3[]) GetTangent(Vector3 point, Circle circle) {
            // 计算点到圆心的距离
            float distance = Vector3.Distance(point, circle.center);

            // 判断是否存在切线
            if (distance <= circle.radius) {
                // 不存在切线
                return (null, null);
            }

            // 计算交点
            Vector3 AB = point - circle.center;
            float m = AB.y / AB.x;
            float n = point.y - m * point.x;
            float a = m * m + 1;
            float b = 2 * m * n - 2 * circle.center.x - 2 * m * circle.center.y;
            float c = circle.center.x * circle.center.x + (n - circle.center.y) * (n - circle.center.y) - circle.radius * circle.radius;
            float delta = b * b - 4 * a * c;
            if (delta < 0) {
                // 圆与直线没有交点，不存在切线
                return (null, null);
            }
            float x2 = (-b + Mathf.Sqrt(delta)) / (2 * a);
            float x3 = (-b - Mathf.Sqrt(delta)) / (2 * a);
            Vector3 intersection1 = new Vector3(x2, m * x2 + n, 0);
            Vector3 intersection2 = new Vector3(x3, m * x3 + n, 0);

            // 计算切向量
            Vector3 tangent1 = (intersection1 - point).normalized;
            Vector3 tangent2 = (intersection2 - point).normalized;

            return (new Vector3[] {intersection1, intersection2}, new Vector3[] {tangent1, tangent2});
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, avoidDistance);
            //绘制自身半径
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, GetComponent<CircleCollider2D>().radius);
            
            foreach (var friend in friends)
            {
                var selfRadius = GetComponent<CircleCollider2D>().radius;
                var friendRadius = friend.GetComponent<CircleCollider2D>().radius;
                var selfSpeed = GetComponent<Rigidbody2D>().velocity.XYZ() + friend.transform.position;
                var friendSpeed = friend.GetComponent<Rigidbody2D>().velocity.XYZ() + friend.transform.position;
                var targetCircle = new Circle(friend.transform.position, selfRadius + friendRadius);
                
                //绘制圆
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(targetCircle.center, targetCircle.radius);
                
                //绘制切线
                var tangent = GetTangent(transform.position, targetCircle).Item1;
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, tangent[0]);
                Gizmos.DrawLine(transform.position, tangent[1]);
            }

        }
    }
}