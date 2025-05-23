using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    public class NormalNodeSO : NodeSO
    {
        [SerializeField] protected string reader;
        [TextArea(5,20)]
        [SerializeField] protected string contents;
        public float nodeDelay;

        protected string tagExceptedReader;
        protected string tagExceptedContents;

        [HideInInspector] public List<TagAnimation> readerTagAnimations = new();
        [HideInInspector] public List<TagAnimation> contentTagAnimations = new();
        [HideInInspector] public NodeSO nextNode;

        public void SetNormalNodeByOption(Option option)
        {
            guid = "";
            reader = "Player";
            contents = option.option;
            nextNode = option.nextNode;
            
            startDialogEventSO = option.startDialogEventSO;
            startDialogEvent = option.startDialogEvent;
            
            endDialogEventSO = option.endDialogEventSO;
            endDialogEvent = option.endDialogEvent;
            
            OnEnable();
        }

        public string GetContents() => tagExceptedContents;
        public string GetReaderName() => tagExceptedReader;

        public override List<TagAnimation> GetAllAnimations()
        {
            List<TagAnimation> tagAnimations = new List<TagAnimation>();

            readerTagAnimations.ForEach(anim => tagAnimations.Add(anim));
            contentTagAnimations.ForEach(anim => tagAnimations.Add(anim));

            return tagAnimations;
        }

        private void OnEnable()
        {
            tagExceptedContents = contents;
            contentTagAnimations = TagParser.ParseAnimation(ref tagExceptedContents);
            tagExceptedReader = reader;
            readerTagAnimations = TagParser.ParseAnimation(ref tagExceptedReader);

            contentTagAnimations.ForEach(anim =>
            {
                if (!anim.SetParameter())
                    Debug.LogError(tagExceptedContents);
            });
            readerTagAnimations.ForEach(anim =>
            {
                if (!anim.SetParameter())
                    Debug.LogError(tagExceptedReader);
            });
        }
    }
}

