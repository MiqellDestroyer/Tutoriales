using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewConversation", menuName = "ConversationSystem/Conversation", order = 1)]
[System.Serializable]
public class Conversation : ScriptableObject {

    public string name;

    public Dialogue[] dialogues;

}
[System.Serializable]
public class Dialogue
{
    [TextArea(3,10)]
    public string sentence;
    public DialogueOption[] options; //deberia tener un limite;
}
[System.Serializable]
public class DialogueOption
{
    [TextArea(3, 10)]
    public string text;

    public int optionNumber;

    public Conversation response;
}
