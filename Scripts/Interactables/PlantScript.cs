using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlantScript : Interactable
{
    [FoldoutGroup("Debug")] public int ID;
    [FoldoutGroup("Gameplay")] public int phase;
    [FoldoutGroup("Gameplay")] public int age;

    [FoldoutGroup("Gameplay")] public Item item;
    [FoldoutGroup("Gameplay")] public ItemSO SO;
    [FoldoutGroup("Gameplay")] public int quantity;
    [FoldoutGroup("Gameplay")] public int quality = 100;
    [FoldoutGroup("Gameplay")] public int sunLevel = 100;

    [FoldoutGroup("Gameplay")] public float idealWater;
    [FoldoutGroup("Gameplay")] public bool growingYield;
    [FoldoutGroup("Gameplay")] public bool noWater;
    [FoldoutGroup("Gameplay")] public bool destroyOnHarvest;
    [FoldoutGroup("Gameplay")] public float waterLevel;
    [FoldoutGroup("Gameplay")] public int harvestTime;
    [FoldoutGroup("Gameplay")] public GameObject harvestFX;
    [VerticalGroup("Season")] public bool spring, summer, autumn, winter;
    [FoldoutGroup("Visuals")] public GameObject[] phaseObjects;
    [FoldoutGroup("Visuals")] public GameObject fruit;
    [FoldoutGroup("Debug")] public bool finished;

    [FoldoutGroup("Debug")] public bool watered;
    [FoldoutGroup("Debug")] public bool pickable;
    [FoldoutGroup("Debug")] public bool picked;

    Collider lastCol;
    TimeManager timeManager;
    InventoryScript inventoryScript;

    private void Awake()
    {
        DataManager.Instance.saveItemEvent += SavePlant;
        DataManager.Instance.loadItemEvent += UnloadObject;
        inventoryScript = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<InventoryScript>();
        timeManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<TimeManager>();
        timeManager.hubDayEvent += Grow;

    }

    void Start()
    {
        UpdateGO();
    }

    public void CheckSeason()
    {
        switch (TimeManager.Instance.season)
        {
            case TimeManager.Season.Spring: if (!spring) Destroy(gameObject); break;
            case TimeManager.Season.Summer: if (!summer) Destroy(gameObject); break;
            case TimeManager.Season.Autumn: if (!autumn) Destroy(gameObject); break;
            case TimeManager.Season.Winter: if (!winter) Destroy(gameObject); break;
        }
    }


   public float CheckPlantWater() {
        float tempWater = TerrainScript.Instance.CheckWater(transform.position);
        return tempWater / 4 * 100;
    }

    void SavePlant()
    {
        PlantData data = new PlantData();
        data.phase = phase;
        data.plantID = ID;
        data.quality = quality;
        data.waterLevel = waterLevel;
        data.sunLevel = sunLevel;
        data.position = transform.position;
        data.rotation = transform.rotation;
        DataManager.Instance.currentSaveData.plantData.Add(data);
    }

    void UnloadObject()
    {
        Destroy(gameObject);
    }

    void Update()
    {
    }

    private void OnDisable()
    {
        DataManager.Instance.saveItemEvent -= SavePlant;
        DataManager.Instance.loadItemEvent -= UnloadObject;
        timeManager.hubDayEvent -= Grow;
    }


    public override void Interact()
    {
        base.Interact();
        if (type == Type.None) return;
        Harvest();
    }

    public void Harvest()
    {
        if (picked) return;
        picked = true;
        if (harvestFX != null)
            Instantiate(harvestFX, transform.position, transform.rotation);

        item.SO = SO;
        item.quantity = quantity;
        item.quality = quality;
        inventoryScript.PickupItem(item);

        if (fruit != null)
        {
            fruit.SetActive(false);
        }
        phase = phaseObjects.Length;

        if (destroyOnHarvest) Destroy(gameObject);
    }



    void Grow()
    {
        CheckSeason();
        age++;
        float tempWater = TerrainScript.Instance.CheckWater(transform.position);
        waterLevel += tempWater;
        quality += (int) (5 * ((4-Mathf.Abs(tempWater - idealWater)) / 4));
        if (growingYield) {
            quantity = 1 + (quality / 20);
        }
        if (tempWater >= idealWater || noWater)
        {
            phase++;
            if (phase < phaseObjects.Length)
                UpdateGO();
        }
        watered = false;
    }

    private void OnValidate()
    {
        UpdateVisuals();
    }

    void UpdateGO()
    {
        if (phase >= phaseObjects.Length + harvestTime)
        {
            finished = true;
            picked = false;
            if (fruit != null)
                fruit.SetActive(true);
        }
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        foreach (GameObject GO in phaseObjects)
        {
            GO.SetActive(false);
        }
        if (phase >= phaseObjects.Length)
            phaseObjects[phaseObjects.Length - 1].SetActive(true);
        else
            phaseObjects[phase].SetActive(true);
    }
}
