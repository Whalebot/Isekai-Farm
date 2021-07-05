using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameTriggerHandler : MonoBehaviour
{
    public GameTrigger[] triggers;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameTrigger t in triggers)
        {
            if (t.days.Length > 0)
            {
                if (t.days[TimeManager.Instance.date - 1] )
                {
                    if (TimeManager.Instance.clockTime.x > t.minTime && TimeManager.Instance.clockTime.x < t.maxTime)

                        PerformTrigger(t);
                }
            }
            else { PerformTrigger(t); }

        }
    }

    void PerformTrigger(GameTrigger t)
    {
        t.trigger = DataManager.Instance.CheckTrigger(t.triggerName);
        //print(DataManager.Instance.CheckTrigger(t.triggerName) + t.triggerName);
        if (t.trigger)
        {
            if (t.activateObject != null)
            {
                t.activateObject.SetActive(true);
            }
            if (t.deactivateObject != null)
            {
                t.deactivateObject.SetActive(false);
            }
        }
        else
        {
            if (t.activateObject != null)
            {
                t.activateObject.SetActive(false);
            }
            if (t.deactivateObject != null)
            {
                t.deactivateObject.SetActive(true);
            }
        }

    }
}
[System.Serializable]
public class GameTrigger
{
    public bool[] days = new bool[7];
    public int minTime, maxTime;
    public string triggerName;
    public bool trigger;
    public GameObject activateObject;
    public GameObject deactivateObject;
}
