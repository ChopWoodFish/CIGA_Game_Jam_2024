using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;


    public class InkDialogue
    {
        public InkDialogue(TextAsset inkJSONAsset)
        {
            story = new Story(inkJSONAsset.text);
            parser = new InkParser();
        }

        private Story story;
        private InkParser parser;
        public string crtText;
        public Dictionary<string, string> crtTag => parser.dictTagNameContent;
        public List<Choice> listCrtChoice => story.currentChoices;

        public string GetNextSentence()
        {
            if (story.canContinue)
            {
                crtText = story.Continue();
                parser.ParseCurrentTag(story);
                return crtText;
            }
            else
            {
                return null;
            }
        }

        public void ChooseChoice(int index)
        {
            story.ChooseChoiceIndex(index);
        }

    }