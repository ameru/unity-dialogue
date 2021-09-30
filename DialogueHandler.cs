namespace Dialogue
{

  public class DialogueHandler : MonoBehavior
  {
    [SerializeField] private DialogueTreeObject dialogueTree;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private UnityEvent onDialogueEnd;
    [SerializeField] private ScriptableEvent[] scriptableEvents;
    // DialogueUI provider
    
    private void Start ()
    {
      dialogueTree.ResetCallbacks();
      foreach (var scriptableEvent in scriptableEvents)
      {
        dialogueTree.RegisterScriptableCallback(
          scriptableEvent.eventName,
          action: (() => scriptableEvent.unityEvent.Invoke())
          );
      }
    }
    dialogueTree.SetUpDialogueUnitsDict();
    dialogueTree.continueCallback += dialogueUI.ContinueDialogue;
    dialogueTree.continueCallback += ContinueDialogue;
    dialogueTree.endDialogueCallback += dialogueUI.EndDialogue;
    dialogueTree.endDialogueCallback += EndDialogue;
  }
  
  public void OnInteract(Interactor interactor)
  {
    var dialogueState = interactor.GetComponent<DialogueState>();
    if(dialogueState == null) return;
    
    dialogueTree.SetUpDialogueState(dialogueState);
    ContinueDialogue();
  }
  
  public void ContinueDialogue()
  {
    HandleDialogue(dialogueTree.GetNextDialogueUnit());
  }
  
  public void EndDialogue()
  {
    onDialogueEnd.Invoke();
  }
  
  private void HandleDialogue(DialogueUnit dialogueUnit)
  {
    // get the UI from the UI provider
    // populate the dialogue UI
    dialogueUI.SetNpcName(dialogueTree.npcName);
    dialogueUI.SetSentences(dialogueUnit.sentences);
    dialogueUI.SetDialogueOptions(dialogueUnit.options, dialogueTree.defaultOption);
    dialogueUI.ContinueDialogue();
  }
