using System.Collections.Generic;
using UnityEngine;

namespace Unity_Dialogue
{
    public class DialogueState : MonoBehavior
    {
        // Dict(npcName, dialogueTreeState)
        public Dictionary<string, string> stateDict;

        // TO DO: Add save/load methods (serialization)
        /* It saves on start now, but in the future we should have it load
        from last saved checkpoint */

        private void Start()
        {
            stateDict = new Dictionary<string, string>();
        }
    }
}
