using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public GameObject[] buttons;
    GameObject[] children;
    public GameObject skillWindow;
    public GameObject skillNotification;
    public EventSystem ES;
    Status status;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (status.levelUp && status.level < 6) {
        //    skillNotification.SetActive(true);
        //}
        //else skillNotification.SetActive(false);

        //if (status.levelUp && Input.GetButtonDown("Start")) 
        //{
        //    status.levelUp = false;
        //    OpenSkillWindow();
        //}
    }

    IEnumerator ButtonShit(GameObject bs) {
        yield return null;
        ES.SetSelectedGameObject(null);
        ES.SetSelectedGameObject(bs);
    }

    public void OpenSkillWindow()
    {

        skillWindow.SetActive(true);
        GameManager.isPaused = true;
        for (int j = 0; j < buttons.Length; j++)
        {
            if (buttons[j].activeSelf)
            {
                button = buttons[j].GetComponentInChildren<Button>();
                button.Select();

                break;
            }
        }

    }

    public void CloseSkillWindow()
    {
        skillWindow.SetActive(false);
        GameManager.isPaused = false;
    }

    public void SkillActive(int i)
    {
        //GameManager.skills[i] = true;
        buttons[i].SetActive(false);
        ES.SetSelectedGameObject(null);
   
        CloseSkillWindow();
    }
}
