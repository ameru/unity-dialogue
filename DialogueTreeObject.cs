using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity_Dialogue
{
    [CreateAssetMenu(fileName = "DialogueTree", menuName =
        "ScriptableObjects/Dialogue Tree")]
    public class DialogueTreeObject : ScriptableObject
    {
        public string npcName;
        public string defaultState;
        public DialogueOption defaultOption;
        public string[] scriptableCallbackNames;
        public DialogueUnit[] dialogueUnits;

        // Non-editor class members
        public DialogueState dialogueState;
        public Action continueCallback;
        public Action endDialogueCallback;
        public Dictionary<string, DialogueUnit> dialogueUnitsDict;
        public Dictionary<string, Action> scriptableCallbacks =
            new Dictionary<string, Action>();

        public void AddToState(string stateToAdd)
        {
            dialogueState.stateDict[npcName] += stateToAdd;
        }

        public void RemoveState(int length = 1)
        {
            if (dialogueState.stateDict[npcName].Length < length)
            {
                return;
            }
            dialogueState.stateDict[npcName] = dialogueState.stateDict[npcName].
                Remove(dialogueState.stateDict[npcName].Length - length);
        }

        public void ResetState(string newState)
        {
            dialogueState.stateDict[npcName] = newState;
        }

        public void CallScriptableAction(string actionName)
        {
            scriptableCallbacks[actionName]();
        }

        public void Continue()
        {
            continueCallback();
        }

        public void EndDialogue()
        {
            endDialogueCallback();
        }

        public void RegisterScriptableCallback(string callbackName,
            Action action)
        {
            scriptableCallbacks[callbackName] = action;
        }

        public void SetUpDialogueUnitsDict()
        {
            dialogueUnitsDict = new Dictionary<string, DialogueUnit>();
            foreach (var dialogueUnit in dialogueUnits)
            {
                dialogueUnitsDict[dialogueUnit.requiredStateKey] = dialogueUnit;
            }
        }

        public void SetUpDialogueState(DialogueState state)
        {
            dialogueState = state;
            if (!dialogueState.stateDict.ContainsKey(npcName))
            {
                dialogueState.stateDict[npcName] = defaultState;
            }
        }

        public void ResetCallbacks()
        {
            continueCallback = () => { };
            endDialogueCallback = () => { };
            scriptableCallbacks.Clear();
        }

        public DialogueUnit GetNextDialogueUnit()
        {
            return dialogueUnitsDict.TryGetValue(dialogueState.stateDict
                [npcName], out var value DialogueUnit) ? value : null;
        }
    }
}
