using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public Slider masterSlider;
    public Slider camXSlider;
    public Slider camYSlider;


    public TextMeshProUGUI masterText;
    public TextMeshProUGUI camXText;
    public TextMeshProUGUI camYText;

    // Start is called before the first frame update
    void Awake()
    {
      
    }

    

    void LoadSettings()
    {
        camXSlider.value = DataManager.Instance.currentSaveData.settings.cameraX;
        camYSlider.value = DataManager.Instance.currentSaveData.settings.cameraY;
        masterSlider.value = DataManager.Instance.currentSaveData.settings.masterVolume;
    }

    void SaveSettings()
    {

        DataManager.Instance.currentSaveData.settings.cameraX = camXSlider.value;
        DataManager.Instance.currentSaveData.settings.cameraY = camYSlider.value;
        DataManager.Instance.currentSaveData.settings.masterVolume = masterSlider.value;
    }

    private void OnEnable()
    {
      //  DataManager.Instance.saveDataEvent += SaveSettings;
      //  DataManager.Instance.loadDataEvent += LoadSettings;
        LoadSettings();
        UpdateValue();
    
    }

    private void OnDisable()
    {
     //   DataManager.Instance.saveDataEvent -= SaveSettings;
     //   DataManager.Instance.loadDataEvent -= LoadSettings;
    }

    public void UpdateValue()
    {
        masterText.text = "" + masterSlider.value;
        camXText.text = "" + camXSlider.value;
        camYText.text = "" + camYSlider.value;

        SaveSettings();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeleteData()
    {
        DataManager.Instance.ClearData();
  
        SceneManager.LoadScene(0);
    }
    public void TitleScreen()
    {
        SceneManager.LoadScene(4);
    }
    public void CloseGame()
    {
        Application.Quit();
    }

}
