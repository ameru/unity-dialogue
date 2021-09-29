using System;
using UnityEngine;

namespace Unity_Dialogue
{
    [Serializable]
    public class DialogueUnit
    {
        /* this is how we keep track of where in the dialogue tree the player 
         * is currently at */
        public string requiredStateKey;

        // skips initial welcome message by initializing at space 2
        [TextArea(2, 5)]
        public string[] sentences;
        public DialogueOption[] options;
    
        
    }
}
