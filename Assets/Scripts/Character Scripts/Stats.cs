
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Stats
{
    [Header("Ressources")]
    public int maxHealth = 0;
    public int currentHealth = 0;
    public float maxStamina = 0;
    public float currentStamina = 0;
    public int experience = 0;
    public int level = 1;

    public int attack;
    public int magic;
    

    [FoldoutGroup("Defense")] public int poise = 0;
    [FoldoutGroup("Defense")] public float defense = 0;
    [FoldoutGroup("Defense")] public float bluntDefense = 0;
    [FoldoutGroup("Defense")] public float slashDefense = 0;
    [FoldoutGroup("Defense")] public float thrustDefense = 0;
    [FoldoutGroup("Defense")] public float chopDefense = 0;
    [FoldoutGroup("Defense")] public int resistance = 0;

    [Header("Modifiers")]
    [FoldoutGroup("Modifiers")] public float guardBreakModifier = 0;
    [FoldoutGroup("Modifiers")] public float movementSpeedModifier = 0;

    [FoldoutGroup("Modifiers")] public float attackSpeed = 0;
    [FoldoutGroup("Modifiers")] public float baseDamage = 0;
    [FoldoutGroup("Modifiers")] public float sizeModifier = 0;
    [FoldoutGroup("Modifiers")] public float damageModifierPercentage = 0;
    [FoldoutGroup("Modifiers")] public float damageModifierFlat = 0;
    [FoldoutGroup("Modifiers")] public float hitStunMultiplier = 0;
    [FoldoutGroup("Modifiers")] public float knockbackModifier = 0;


    [Header("External Passives")]
    [FoldoutGroup("Elemental stuff")] public float critChance = 0;
    [FoldoutGroup("Elemental stuff")] public float critMultiplier = 0;
    public float fireCost = 0;
    public float waterCost = 0;
    public float earthCost = 0;
    public float windCost = 0;
}
