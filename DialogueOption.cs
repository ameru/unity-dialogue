using System;
using UnityEngine.Events;

namespace Unity_Dialogue
{
    [Serializable]

    // can decide which text to show and which options are triggered
    public class DialogueOption
    {
        // Unity events triggered by clicking on buttons in UI Canvas
        public string buttonText;
        public UnityEvent actionToTrigger;

    
    }
}
