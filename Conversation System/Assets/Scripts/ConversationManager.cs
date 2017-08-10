using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ConversationManager : Singleton<ConversationManager> {

    protected ConversationManager() { }

    public Queue<Dialogue> dialogues;

    public Dialogue current;

    private bool waitingAnswer = false;

    public GameObject ConversationPanel;

    public Transform optionPanel;

    public Button optionPrefab;

    public TMP_Text dialogueText;

    public TMP_Text nameText;

    private void Start()
    {
        dialogues = new Queue<Dialogue>();
    }

    public void StartConversation(Conversation conversation)
    {
        ConversationPanel.SetActive(true);

        nameText.text = conversation.name;
        
        dialogues.Clear();

        foreach (Dialogue dialogue in conversation.dialogues)
        {
            dialogues.Enqueue(dialogue);
        }
        
        NextDialogue();
    }

    public void NextDialogue()
    {
        if (!waitingAnswer)
        {
            if (dialogues.Count == 0)
            {
                EndConversation();
                return;
            }

            current = dialogues.Dequeue();

            DisplayDialogue();
        }
    }

    public void DisplayDialogue()
    {
        ClearOptionPanel();
        Debug.Log(current.sentence);
        dialogueText.text = current.sentence;

        if (current.options.Length != 0)
        {
            waitingAnswer = true;
            foreach (DialogueOption option in current.options)
            {
                Debug.Log(option.optionNumber + " - " + option.text);

                Button optionButton = GameObject.Instantiate<Button>(optionPrefab, optionPanel);
                optionButton.onClick.AddListener(delegate { SelectOption(option.optionNumber); });
                optionButton.GetComponentInChildren<TMP_Text>().text = option.text;

            }
        }

    }

    public void SelectOption(int selected)
    {
        foreach (DialogueOption option in current.options)
        {
            if (option.optionNumber == selected)
            {
                EndConversation();
                if (option.response)
                {
                    StartConversation(option.response);
                    return;
                }
                else
                {
                    EndConversation();
                }
            }
        }
    }

    public void ClearOptionPanel()
    {
        for (int i = 0; i < optionPanel.childCount; i++)
        {
            Destroy(optionPanel.GetChild(i).gameObject);
        }
    }

    public void EndConversation()
    {
        ConversationPanel.SetActive(false);
        Debug.Log("End Conversation");
        waitingAnswer = false;
        ClearOptionPanel();
        dialogues.Clear();
        current = null;
    }


}
