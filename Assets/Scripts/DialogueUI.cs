using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;

    private TypewriterEffect typewriterEffect;

    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return typewriterEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        // End of tutorial
        var m = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        m.StartedGame = true;
        m.Spawner.GetComponent<Spawner>().StartSpawning(); //GameObject.FindGameObjectWithTag("Spawner").SetActive(true); // NOTE: you can't find disabled objects
        GameObject.FindGameObjectWithTag("LymphNode").SetActive(false);
        GameObject.FindGameObjectWithTag("DialogueBox").SetActive(false);
    }
}
