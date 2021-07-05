using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneScript : MonoBehaviour
{
    public string trigger;
    public DialogueSO dialogue;
    public GameObject csCam;
    public Transform[] targets;
    public Transform[] positions;
    public float delay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        DialogueManager.Instance.dialogueEnd += CutsceneEnd;
        SetPositions();
        print(gameObject);
    }

    private void OnDisable()
    {
        DialogueManager.Instance.dialogueEnd -= CutsceneEnd;
    }

    void CutsceneEnd()
    {
        csCam.SetActive(true);
        gameObject.SetActive(false);
        GameManager.cutscene = false;
        DataManager.Instance.SetTrigger(trigger);
    }

    public void SetPositions()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].position = positions[i].position;
            targets[i].rotation = positions[i].rotation;
        }
        csCam.SetActive(true);
        GameManager.cutscene = true;
        StartCoroutine("DelayDialogue");
    }

    IEnumerator DelayDialogue()
    {
        yield return new WaitForSeconds(delay);
        DialogueManager.Instance.StartDialogue(dialogue.dialogues);
    }
}
