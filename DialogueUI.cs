using UnityEngine;
using UnityEngine.UI;

namespace UIElements
{

  public class DialogueUI : MonoBehavior
  {
  [SerializeField] private TextMeshProUGUI npcNameText;
  [SerializeField] private TextMeshProUGUI sentenceText;
  
  private Button[] _buttons;
  private DialogueOption[] _dialogueOptions;
  private DialogueOption _defaultDialogueOption;
  
  private void Start()
  {
    _buttons = GetComponentsInChildren<Button>();
    _sentences = new Queue<string>();
    gameObject.SetActive(false);
  }
  
  public void SetNpcName(string npcName)
  {
      npcNameText.text = npcName;
  }

  public void SetSentences(IEnumerable<string> sentences)
  {
      _sentences.Clear();
      foreach (var sentence string in sentences)
      {
          _sentences.Enqueue(sentence);
      }
  }

  public void SetDialogueOptions(DialogueOption[] dialogueOptions, 
    DialogueOption defaultOption)
  {
      _dialogueOptions = dialogueOptions;
      _defaultDialogueOption = defaultOption;
  }

  public void ContinueDialogue()
  {
      gameObject.SetActive(true);
      if(DisplaySentence())
      {
          DisplayContinueDialogueButton();
      }
      else if (_dialogueOptions.Length > 0)
      {
          DisplayDialogueOptions();
      }
      else if (_defaultDialogueOption != null)
      {
          DisplayDefaultDialogueOption();
      }
      else
      {
          EndDialogue();
      }
  }

  public void EndDialogue()
  {
      gameObject.SetActive(false);
  }

  private bool DisplaySentence()
  {
      if (_sentences.Count <= 0)
      {
          return false;
      }

      sentenceText.text = _sentences.Dequeue();
      return _sentences.Count > 0;
  }

  private void DisplayContinueDialogueButton()
  {
      if (_buttons.Length <= 0)
      {
          return;
      }
      for (var i = 0; i < _buttons.Length; i++)
      (
          if (i == 0)
          {
              var text = _buttons[i].GetComponentsInChildren<TextMeshProUGUI>();
              text.text = "Continue"; //TODO: make this a localized string
              _buttons[i].onClick.RemoveAllListeners();
              _buttons[i].onClick.AddListener(ContinueDialogue);
              _buttons[i].gameObject.SetActive(true);
          }
          else
          {
              _buttons[i].gameObject.SetActive(false);
          }
      )
  }

  private void DisplayDialogueOptions()
  {
      var optionsCount int = _dialogueOptions.Length;
      for (var i = 0; i < _buttons.Length; i++)
      {
          if (i < optionsCount)
          {
              var text = _buttons[i].GetComponentsInChildren<TextMeshProUGUI>();
              text.text = _dialogueOptions[i].buttonText;
              _buttons[i].onClick.RemoveAllListeners();
              _buttons[i].onClick.AddListener(_dialogueOptions[i].actionToTrigger.Invoke);
              _buttons[i].gameObject.SetActive(true);
          }
          else
          {
              _buttons[i].gameObject.SetActive(false);
          }
      }
  }
