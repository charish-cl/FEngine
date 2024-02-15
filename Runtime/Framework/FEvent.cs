using System;
using System.Collections.Generic;
using Object = System.Object;

namespace FEngine
{
    public class BaseAttribute: Attribute
    {
        public Type AttributeType { get; }

        public BaseAttribute()
        {
            this.AttributeType = this.GetType();
        }
    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class EventAttribute:BaseAttribute
    {
        
    }
    [Event]
    public abstract class AEvent<T>
    {
        public abstract void Handle(T arg);
    }
    /// <summary>
    /// simple事件框架
    /// </summary>
    public class FEvent
    {
        public static Dictionary<string, List<Action<Object>>> allEvent=new Dictionary<string, List<Action<object>>>();
        
        // delegate void Handle(object o);
        //
        // public static void InheriteClass(Dictionary<string, List<Action<object>>> allEvent,
        //     Dictionary<string, Type> dictionary)
        // {
        //    
        //     allEvent = new Dictionary<string, List<Action<object>>>();
        //     Assembly assembly = Assembly.Load("Assembly-CSharp");
        //     foreach (var keyValuePair in dictionary)
        //     {
        //         var type = keyValuePair.Value;
        //         var key = keyValuePair.Key;
        //         var types = assembly.Modules
        //             .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(type)))
        //             .ToList();
        //         allEvent.Add(key, new List<Action<object>>());
        //         Action<object> action;
        //         foreach (var t in types)
        //         {
        //             Action<object> d = (Action<object>)Delegate.CreateDelegate(typeof(Handle), t.GetMethods()[0]);
        //             allEvent[key].Add(d);
        //         }
        //     }
        //
        // }
        public static void Register(string key,Action<Object> action)
        {
            if (!allEvent.ContainsKey(key))
            {
                allEvent.Add(key,new List<Action<object>>());
            }
            allEvent[key].Add(action);
        }
        public static void UnRegister(string key,Action<Object> action)
        {
            if (allEvent.ContainsKey(key))
            {
                if(allEvent[key].Contains(action))
                     allEvent[key].Remove(action);
            }
        }
        public static void Fire(string key,Object arg)
        {
            if (allEvent.ContainsKey(key))
            {
                var actions = allEvent[key];
                foreach (var action in actions)
                {
                    action.Invoke(arg);
                }
            }
            else
            {
                allEvent.Add(key,new List<Action<object>>());
                var actions = allEvent[key];
                foreach (var action in actions)
                {
                    action.Invoke(arg);
                }
            }
    
        }
    }
}