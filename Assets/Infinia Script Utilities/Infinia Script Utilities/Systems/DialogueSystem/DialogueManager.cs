using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;

    public Dialogue secondDialogue;
    public int lastDialogue = 2;
    public float animationTime = 1f;
    public Animator animator;

    public Queue<string> sentences;

    int dialogueCount = 0;
    int count = 0;

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("NumberOfRuns", 0);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueCount++;
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        count = 0;
        DisplayNextSentence();       
    }

    public void DisplayNextSentence()
    {
        count++;
        if (sentences.Count == 0 && count % 2 != 0)
        {
            EndDialogue();
            return;
        }

        if (count % 2 != 0)
        {
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeWrite(sentence));
        }
    }

    IEnumerator TypeWrite (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            if (count % 2 != 0)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.032f);
            }
            else
            {
                dialogueText.text += letter;
                yield return new WaitForFixedUpdate();
            }
        }
        count = 0;
    }
    public void Skip()
    {
        EndDialogue();
    }
    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);

        StartCoroutine(LoadNextDialogue());
    }
    IEnumerator LoadNextDialogue()
    {
        yield return new WaitForSeconds(1f);

        if (dialogueCount == lastDialogue)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        dialogueText.text = "...";
        StartDialogue(secondDialogue);     
    }
}

