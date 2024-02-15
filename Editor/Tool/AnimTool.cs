using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace FEngineEditor
{
    public class AnimTool
    {
        public static Dictionary<RuntimeAnimatorController, List<AnimationClip>> GetAnimatorVarible(Object[] gos)
        {
            var dictionary = new Dictionary<RuntimeAnimatorController, List<AnimationClip>>();
            foreach (var gameObject in gos)
            {
                var animator = (RuntimeAnimatorController)gameObject;
                var clips = animator.animationClips;
                dictionary.Add(animator,new List<AnimationClip>());
              
                foreach (var animationClip in clips)
                {
                    dictionary[animator].Add(animationClip);             
                }
            }
            return dictionary;
        }
        
     
    }
}