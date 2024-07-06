using System;
using System.Collections.Generic;
using System.Text;
using Ink.Runtime;
using UnityEngine;

    /// <summary>
    /// 解析ink中的meta元素
    /// </summary>
    public class InkParser
    {
        public Dictionary<string, string> dictTagNameContent = new Dictionary<string, string>();
        
        // todo currentTag中可能混合有global、knot和row tag，可能需要作出区分
        // global、knot tag只有在continue到第一句是能够获取到，很神秘
        public void ParseCurrentTag(Story story)
        {
            dictTagNameContent.Clear();
            foreach (var tag in story.currentTags)
            {
                var arrSplit = tag.Split(':');
                if (arrSplit.Length != 2)
                {
                    Debug.LogError($"try parse tag [{tag}] failed!");
                    continue;
                }
                
                string tagName = arrSplit[0].Trim(' ');
                string tagContent = arrSplit[1].Trim(' ');
                dictTagNameContent[tagName] = tagContent;
            }
            
            // PrintAllTags();
        }

        private void PrintAllTags()
        {
            StringBuilder sb = new StringBuilder(String.Empty);
            foreach (var kvp in dictTagNameContent)
            {
                sb.AppendLine($"{kvp.Key}: {kvp.Value}");
            }
            Debug.Log(sb);
        }
    }