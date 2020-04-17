using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    public Dialogue dialogue;
    private void Start()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
