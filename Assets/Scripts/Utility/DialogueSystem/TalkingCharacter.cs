using System;
using System.Collections.Generic;
using UnityEngine;


    public class TalkingCharacter : MonoBehaviour
    {
        public Transform bubblePoint;
        public TextAsset inkJSONAsset;

        [SerializeField] private Animator anim;

        private void Start()
        {
            anim = transform.GetComponentInChildren<Animator>();
            
        }

        public void ProcessTag(Dictionary<string, string> dictTag)
        {
            foreach (var kvp in dictTag)
            {
                if (kvp.Key == "anim")
                {
                    ProcessAnimTag(kvp.Value);
                }
            }
        }

        private void ProcessAnimTag(string animTag)
        {
            anim.Play(animTag, -1, 0);
        }
    }