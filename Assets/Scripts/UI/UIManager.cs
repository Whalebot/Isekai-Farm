using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Febucci.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public EventSystem eventSystem;
    public Transform descriptionWindowPanel;
    public GameObject descriptionWindow;
    public GameObject inventoryFull;
    public TextAnimatorPlayer moneyPlayer;

    public GameObject statusUI;
    public GameObject timeUI;

    public DescriptionWindow usedItemText;
    public DescriptionWindow foundItemWindow;
    public static bool prioritizeUI;
    bool openWindow;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.southInput += CloseWindow;
        InputManager.Instance.westInput += CloseWindow;
        UpdateMoney();
    }

    private void Update()
    {
        if (GameManager.shopOpen || GameManager.menuOpen)
        {
            statusUI.SetActive(false);
          //  timeUI.SetActive(false);
        }
        else {
            statusUI.SetActive(true);
         //   timeUI.SetActive(true);
        }
    }

    public void UpdateMoney() {
        
        moneyPlayer.ShowText("" + DataManager.Instance.currentSaveData.profile.gold);
    }

    public void CloseWindow() {
        if (!openWindow) return;
      //  GameManager.Instance.ResumeGame();
        prioritizeUI = false;
        foundItemWindow.gameObject.SetActive(false);
        openWindow = false;
    }

    public void FoundItem(Item item) {
        prioritizeUI = true;
      //  GameManager.Instance.PauseGame();
        openWindow = true;
        foundItemWindow.gameObject.SetActive(true);
        foundItemWindow.DisplayUI(item);
    }

    public void DisplayPickedItem(ItemSO item) {
        GameObject GO = Instantiate(descriptionWindow, descriptionWindowPanel);
        GO.GetComponent<DescriptionWindow>().DisplayUI(item);
    }

    public void DisplayPickedItem(Item item)
    {
 
        GameObject GO = Instantiate(descriptionWindow, descriptionWindowPanel);
        GO.GetComponent<DescriptionWindow>().DisplayUI(item);
    }

    public void InventoryFull() {
        GameObject GO = Instantiate(inventoryFull, descriptionWindowPanel);
    }
    public void DisplayUsedItemEffect(Item item) {
        StopAllCoroutines();
        StartCoroutine("ActiveUsedItemWindow");
        usedItemText.DisplayUI(item);
    }

    IEnumerator ActiveUsedItemWindow() {
        usedItemText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2F);
        usedItemText.gameObject.SetActive(false);
    }

    public void SetActive(GameObject GO) {
        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(GO);

    }
}
