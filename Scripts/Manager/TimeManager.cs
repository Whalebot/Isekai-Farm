using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    public Vector2 clockTime;
    public float minuteMultiplier;
    public float damping;
    DataManager dataManager;


    public enum Season { Spring, Summer, Autumn, Winter };
    public Season season = new Season();
    public enum WeekDay { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday };
    public WeekDay weekDay = new WeekDay();

    public int date;

    private float time;
    private float nextActionTime = 0.0f;

    public delegate void TimeEvent();
    public TimeEvent dayPassEvent;
    public TimeEvent hubDayEvent;
    public TimeEvent resetEvent;
    public TimeEvent sleepStartEvent;
    public TimeEvent sleepEvent;


    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text dateText;
    [SerializeField] private TMP_Text weekDayText;
    [SerializeField] private Image seasonImage;
    [SerializeField] Sprite springSprite, summerSprite, autumnSprite, winterSprite;

    private void Awake()
    {
        Instance = this;
        dataManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<DataManager>();
        dataManager.saveDataEvent += SaveTime;
        dataManager.loadDataEvent += LoadTime;
    }

    void Start()
    {
        // UpdateUI();

        if (SceneManager.GetActiveScene().name == "Hub")
        {
            while (DataManager.Instance.currentSaveData.savedDays[dataManager.sceneIndex] > 0)
            {
                hubDayEvent?.Invoke();
                resetEvent?.Invoke();
                DataManager.Instance.currentSaveData.savedDays[dataManager.sceneIndex]--;
            }
        }
    }

    void SaveTime()
    {
        dataManager.currentSaveData.profile.date = date;
        dataManager.currentSaveData.profile.time = clockTime;
        dataManager.currentSaveData.profile.season = season;

    }

    void LoadTime()
    {
        date = dataManager.currentSaveData.profile.date;
        clockTime = dataManager.currentSaveData.profile.time;
        season = dataManager.currentSaveData.profile.season;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.numpadPlusKey.wasPressedThisFrame) Sleep();

        //    sun.transform.rotation = Quaternion.Lerp(sun.transform.rotation, ghostSun.transform.rotation, Time.deltaTime * damping);
        if (!GameManager.isPaused)
        {
            time += Time.deltaTime;

            if (time > nextActionTime)
            {
                nextActionTime += 1;
                AddSecond();
            }
        }

        UpdateUI();
    }



    public void Sleep()
    {
        sleepStartEvent?.Invoke();
    }

    public void DayPass()
    {
        clockTime.x = 8;
        clockTime.y = 0;

        date++;
        if (date > 7)
        {
            date = 1;
            SeasonChange();
        }
        UpdateUI();

        //if (SceneManager.GetActiveScene().name != "Hub")
        //{
        //    DataManager.Instance.currentSaveData.savedDays++;
        //}
        for (int i = 0; i < DataManager.Instance.currentSaveData.savedDays.Length; i++)
        {
            DataManager.Instance.currentSaveData.savedDays[i]++;
        }
     
        dayPassEvent?.Invoke();
        resetEvent?.Invoke();
    }

    void UpdateUI()
    {
        dateText.text = "" + date;
        switch (date)
        {
            case 1:
                weekDay = WeekDay.Monday;
                break;
            case 2:
                weekDay = WeekDay.Tuesday;
                break;
            case 3:
                weekDay = WeekDay.Wednesday;
                break;
            case 4:
                weekDay = WeekDay.Thursday;
                break;
            case 5:
                weekDay = WeekDay.Friday;
                break;
            case 6:
                weekDay = WeekDay.Saturday;
                break;
            case 7:
                weekDay = WeekDay.Sunday;
                break;

        }
        switch (weekDay)
        {
            case WeekDay.Monday:
                weekDayText.text = "Mon";
                break;
            case WeekDay.Tuesday:
                weekDayText.text = "Tue";
                break;
            case WeekDay.Wednesday:
                weekDayText.text = "Wed";

                break;
            case WeekDay.Thursday:
                weekDayText.text = "Thu";

                break;
            case WeekDay.Friday:
                weekDayText.text = "Fri";

                break;
            case WeekDay.Saturday:
                weekDayText.text = "Sat";
                break;
            case WeekDay.Sunday:
                weekDayText.text = "Sun";
                break;
        }

        switch (season)
        {
            case Season.Spring:
                seasonImage.sprite = springSprite;
                break;
            case Season.Summer:
                seasonImage.sprite = summerSprite;
                break;
            case Season.Autumn:
                seasonImage.sprite = autumnSprite;

                break;
            case Season.Winter:
                seasonImage.sprite = winterSprite;
                break;
        }

    }

    public void SeasonChange()
    {
        switch (season)
        {
            case Season.Spring:
                season = Season.Summer;
                break;
            case Season.Summer:
                season = Season.Autumn;
                break;
            case Season.Autumn:
                season = Season.Winter;
                break;
            case Season.Winter:
                YearChange();
                season = Season.Spring;
                break;
        }

        UpdateUI();
    }

    public void YearChange() { }


    void AddSecond()
    {

        // ghostSun.transform.Rotate(new Vector3(360 / (60 * 24 / minuteMultiplier), 0, 0));
        clockTime.y += minuteMultiplier;
        if (clockTime.y >= 60)
        {
            clockTime.x++;
            clockTime.y = 0;
            if (clockTime.x >= 24)
            {
                DayPass();
                clockTime.x = 0;
            }
        }

        timeText.text = clockTime.x.ToString("00") + " : " + clockTime.y.ToString("00");
    }
}


