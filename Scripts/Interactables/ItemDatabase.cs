﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

using UnityEditor;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; private set; }
    [SerializeField]
    public List<ItemSO> itemSO;
    [SerializeField]
    public List<int> itemID;
    public List<GameObject> plants;
    public List<int> plantID;
    public List<Skill> skills;

    public GameObject storage;
    void Awake()
    {
        Instance = this;
        //itemSO


    }

    [Button]
    void LoadItemSO()
    {
        string[] assetNames = AssetDatabase.FindAssets("t:ItemSO", new[] { "Assets/Scriptable Objects" });
        itemSO.Clear();
        itemID.Clear();
        plantID.Clear();

        foreach (var item in plants)
        {
            plantID.Add(item.GetComponent<PlantScript>().ID);
        }

        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var item = AssetDatabase.LoadAssetAtPath<ItemSO>(SOpath);
            itemSO.Add(item);
            itemID.Add(item.ID);
        }

        string[] skillsNames = AssetDatabase.FindAssets("t:Skill", new[] { "Assets/Scriptable Objects" });
        skills.Clear();
        foreach (string SOName in skillsNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var item = AssetDatabase.LoadAssetAtPath<Skill>(SOpath);
            skills.Add(item);
        }
    }

    public GameObject GetPlant(int ID) {
        int index = plantID.IndexOf(ID);

        return plants[index];
    }

    public int GetSkillID(Skill skill)
    {
        return skills.IndexOf(skill);
    }

    public Skill GetSkill(int ID)
    {
        if (ID == -1) return null;
        return skills[ID];
    }

    public ItemSO GetItemData(int ID)
    {
        if (ID == 0) return null;
        return itemSO[itemID.IndexOf(ID)];
    }

    private void OnApplicationQuit()
    {
        if (!DataManager.Instance.HasSaveData()) ResetAll();
    }

    [Button]
    public void ResetAll() {
        foreach (ItemSO item in itemSO) {
            item.found = false;
            item.used = false;
        }

        foreach (Skill item in skills)
        {
            item.level = 0;
            item.experience = 0;
        }
    }
}
