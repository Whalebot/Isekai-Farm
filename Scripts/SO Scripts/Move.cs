using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Move", menuName = "Move")]
public class Move : ScriptableObject
{
    public GameObject attackPrefab;
    public int animationID;
    public Sprite icon;
    public float staminaCost;
    public float fatigue;


    [Header("Hit properties")]
    public int damage;
    public float hitStun;
    public float knockback;
    public float poiseBreak;
    [EnumToggleButtons] public DamageType damageType;
    [Range(0, 100)] public int slashValue;
    [Range(0, 100)] public int thrustValue;
    [Range(0, 100)] public int bluntValue;
    [Range(0, 100)] public int chopValue;

    public List<SkillExp> trainedSkills;

    [Header("Screen shake")]
    [FoldoutGroup("Feedback")] public float shakeMagnitude;
    [FoldoutGroup("Feedback")] public float shakeDuration;
    [Header("Hit Stop")]
    [FoldoutGroup("Feedback")] public float slowMotionDuration = 0.01F;
    [FoldoutGroup("Feedback")] public bool startupParticle;
    [Header("Move properties")]
    [FoldoutGroup("Move properties")] public bool verticalRotation = true;
    [FoldoutGroup("Move properties")] public int particleID;
    [FoldoutGroup("Move properties")] public bool holdAttack;
    [FoldoutGroup("Move properties")] public bool autoAim;
    [FoldoutGroup("Move properties")] public bool tracking;
    [FoldoutGroup("Move properties")] public bool armor;
    [FoldoutGroup("Move properties")] public bool homing;
    [FoldoutGroup("Move properties")] public bool fullCancelable;
    [FoldoutGroup("Move properties")] public bool noClip;
    [FoldoutGroup("Move properties")] public bool iFrames;

    [Header("Momentum")]
    [FoldoutGroup("Momentum")] public bool overrideVelocity = true;
    [FoldoutGroup("Momentum")] public bool resetVelocityDuringRecovery = true;
    [FoldoutGroup("Momentum")] public Vector3 Momentum;
    [FoldoutGroup("Momentum")] public Vector3[] momentumArray;

}

public enum DamageType { Slash, Blunt, Thrust, Chop, Fire, Water, Earth, Wind }