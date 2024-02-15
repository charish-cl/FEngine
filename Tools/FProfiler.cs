using System;
using System.Diagnostics;

namespace FEngine
{
    public class FProfiler
    {
        /// <summary>
        /// 计算方法耗时
        /// </summary>
        /// <param name="action"></param>
        public void CalculateMethod(Action action)
        {
            // Profiler.BeginSample("Destroy耗时");
            // TestMethod1();
            // Profiler.EndSample();
            //
            // Profiler.BeginSample("SetActive耗时");
            // TestMethod2();
            // Profiler.EndSample();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            TestMethod(action,100);
            sw.Stop();
            UnityEngine.Debug.Log(string.Format("100 total: {0} ms",sw.ElapsedMilliseconds));
            sw.Reset();
            
            sw.Start();
            TestMethod(action,1000);
            sw.Stop();
            UnityEngine.Debug.Log(string.Format("1000 total: {0} ms",sw.ElapsedMilliseconds));
            sw.Reset();
            
            
            sw.Start();
            TestMethod(action,10000);
            sw.Stop();
            UnityEngine.Debug.Log(string.Format("10000 total: {0} ms",sw.ElapsedMilliseconds));
            sw.Reset();
        }

        private void TestMethod(Action action,int times)
        {
            for ( int i = 0 ; i < times ; i ++ )
            {
                action.Invoke();
            }
        }
    }
}