using System;
using System.Collections.Generic;
using UnityEngine;


    public class TalkingCharacter : MonoBehaviour
    {
        public Transform bubblePoint;
        public TextAsset inkJSONAsset;
        public List<string> listLihuiName = new List<string>();
        public List<Sprite> listLihui = new List<Sprite>();
        public TextAsset tunaDialogue;

        [SerializeField] private Animator anim;

        private bool hasTuna;

        public TextAsset GetDialogueAsset()
        {
            if (hasTuna && tunaDialogue != null)
            {
                return tunaDialogue;
            }

            return inkJSONAsset;
        }
        
        private void Start()
        {
            anim = transform.GetComponentInChildren<Animator>();
            IntEventSystem.Register(GameEventEnum.GetTuna, (param) => hasTuna = true);
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
            anim?.Play(animTag, -1, 0);
        }
    }