using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Sirenix.OdinInspector;

public class Status : MonoBehaviour
{
    public Character character;

    [HideInInspector] public int hitstunValue;
    [HideInInspector] public int blockstunValue;
    [HideInInspector] public bool inBlockStun;
    [HideInInspector] public bool inHitStun;

    public bool hasArmor;
    [HideInInspector] public bool animationArmor;

    [Header("Auto destroy on death")]
    public bool autoDeath;
    public bool staminaDeath;
    public bool destroyParent;
    [ShowIf("autoDeath")] public float autoDeathTime = 1.5F;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Vector2 knockbackDirection;
    [HideInInspector] public bool isDead;
    [HideInInspector] public bool blocking;
    [HideInInspector] public bool parrying;
    [HideInInspector] public int parryStun;

    public delegate void StatusEvent();
    public StatusEvent healthEvent;
    public StatusEvent hurtEvent;
    public StatusEvent deathEvent;
    public StatusEvent blockEvent;
    public StatusEvent parryEvent;

    public delegate void TransitionEvent();
    public TransitionEvent neutralEvent;
    public TransitionEvent animationEvent;
    public TransitionEvent blockstunEvent;
    public TransitionEvent hitstunEvent;

    CharacterSFX characterSFX;

    [HideInInspector] public bool godMode;

    public Alignment alignment = Alignment.Enemy;
    public enum State { Neutral, Hitstun, Blockstun, InAnimation, TopAnimation }
    [SerializeField] public State currentState;
    [HideInInspector] public bool regenStamina;

    public int staminaRegenTimer = 1;
    int staminaRegenCounter;
    [TabGroup("Current Stats")]
    public Stats rawStats;
    [TabGroup("Base Stats")]
    public Stats baseStats;


    void Awake()
    {

        rb = GetComponent<Rigidbody>();
        characterSFX = GetComponentInChildren<CharacterSFX>();
        currentState = State.Neutral;

        ApplyCharacter();
    }

    private void Start()
    {
        TimeManager.Instance.sleepStartEvent += RestoreStats;
    }

    void FixedUpdate()
    {
        ResolveHitstun();
        StateMachine();
    }



    public void RestoreStats()
    {
        ReplaceStats(rawStats, baseStats);
    }

    public void ReplaceStats(Stats stat1, Stats stat2)
    {
        //Get stat definition and replace 1 with 2
        Stats def1 = stat1;
        Stats def2 = stat2;

        FieldInfo[] defInfo1 = def1.GetType().GetFields();
        FieldInfo[] defInfo2 = def2.GetType().GetFields();

        for (int i = 0; i < defInfo1.Length; i++)
        {
            object obj = def1;
            object obj2 = def2;
            // Debug.Log("fi name " + defInfo1[i].Name + " val " + defInfo1[i].GetValue(obj));
            defInfo1[i].SetValue(obj, defInfo2[i].GetValue(obj2));
        }
    }

    void StateMachine()
    {
        switch (currentState)
        {
            case State.Neutral:
                StaminaRegen();
                break;
            case State.InAnimation: break;
            case State.Hitstun: break;
            case State.Blockstun: break;
            default: break;
        }
    }

    void StaminaRegen()
    {
        if (rawStats.currentStamina < rawStats.maxStamina)
        {
            if (regenStamina)
            {
                staminaRegenCounter--;
                if (staminaRegenCounter <= 0 && blocking)
                {
                    staminaRegenCounter = staminaRegenTimer;
                    rawStats.currentStamina += 2;
                }
                else rawStats.currentStamina += 2;
            }

            rawStats.currentStamina = Mathf.Clamp(rawStats.currentStamina, -100, rawStats.maxStamina);
        }
    }

    public void GoToState(State transitionState)
    {
        switch (transitionState)
        {
            case State.Neutral:
                if (rawStats.maxStamina <= 0 && staminaDeath) Death();
                currentState = State.Neutral;
                neutralEvent?.Invoke(); break;
            case State.InAnimation:
                currentState = State.InAnimation;
                animationEvent?.Invoke();
                break;
            case State.Hitstun:
                currentState = State.Hitstun;
                hitstunEvent?.Invoke();
                break;
            case State.Blockstun:
                currentState = State.Blockstun;
                blockstunEvent?.Invoke(); break;
            case State.TopAnimation:
                currentState = State.TopAnimation;
                break;
            default: break;
        }
    }

    public void ApplyCharacter()
    {
        if (character == null) return;
        ReplaceStats(rawStats, character.stats);
        ReplaceStats(baseStats, character.stats);
    }

    public int Poise
    {
        get
        {
            return rawStats.poise;
        }
        set
        {
            if (rawStats.poise <= 0)
            {
                rawStats.poise = baseStats.poise;
            }
            else
            {
                rawStats.poise = Mathf.Clamp(value, 0, baseStats.poise);
            }
        }
    }

    public float MaxStamina
    {
        get
        {
            return baseStats.maxStamina;
        }
        set
        {
            float difference = value - baseStats.maxStamina;
            baseStats.maxStamina = value;

            rawStats.maxStamina = Mathf.Clamp(rawStats.maxStamina + difference, 0, baseStats.maxStamina);
            rawStats.currentStamina = Mathf.Clamp(rawStats.currentStamina + difference, 0, rawStats.maxStamina);
        }
    }


    public float Fatigue
    {
        get
        {
            return rawStats.maxStamina;
        }
        set
        {
            float difference = value - rawStats.maxStamina;

            rawStats.maxStamina = Mathf.Clamp(value, 0, baseStats.maxStamina);
            rawStats.currentStamina = Mathf.Clamp(rawStats.currentStamina + difference, 0, rawStats.maxStamina);
  
        }
    }


    public int Health
    {
        get
        {
            return rawStats.currentHealth;
        }
        set
        {
            if (isDead)
                if (rawStats.currentHealth == value) return;
            if (godMode) return;

            //if (value < rawStats.currentHealth)
            //    hurtEvent?.Invoke();
            //if (value - rawStats.currentHealth > 10) Instantiate(PowerupManager.Instance.healParticle, transform.position, Quaternion.identity, transform);

            rawStats.currentHealth = Mathf.Clamp(value, 0, rawStats.maxHealth);

            healthEvent?.Invoke();
            if (rawStats.currentHealth <= 0 && !isDead)
            {
                Death();
            }
        }
    }

    public int HitStun
    {
        get { return hitstunValue; }
        set
        {
            if (!hasArmor && !animationArmor)
            {
                if (value <= 0) return;

                hitstunValue = value;
                GoToState(State.Hitstun);
            }
        }
    }

    public int BlockStun
    {
        get { return blockstunValue; }
        set
        {
            blockstunValue = value;
            GoToState(State.Blockstun);
        }
    }

    public void TakeHit(int damage, Vector3 kb, int stunVal, int poiseBreak, Vector3 dir, float slowDur)
    {
        float angle = Mathf.Abs(Vector3.SignedAngle(transform.forward, dir, Vector3.up));
        Poise -= poiseBreak;

        if (angle > 90)
        {
            if (parrying)
            {
                parryEvent?.Invoke();
                BlockStun = parryStun;
                return;
            }
            else if (blocking)
            {
                blockEvent?.Invoke();
                rawStats.currentStamina -= damage * 2;
                BlockStun = stunVal;
                if (Poise <= 0)
                {
                    TakePushback(kb);
                }
                return;
            }
        }


        if (Poise <= 0)
        {
            if (baseStats.poise < 5)
                TakePushback(kb);

            HitStun = stunVal;
            hurtEvent?.Invoke();
            GameManager.Instance.Slowmotion(slowDur);
        }

        Health -= damage;

    }
    public void TakePushback(Vector3 direction)
    {
        float temp = Vector3.SignedAngle(new Vector3(direction.x, 0, direction.z), transform.forward, Vector3.up);
        Vector3 tempVector = (Quaternion.Euler(0, temp, 0) * new Vector3(direction.x, 0, direction.z)).normalized;
        knockbackDirection = new Vector2(tempVector.x, tempVector.z);

        if (!hasArmor && !animationArmor)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(direction, ForceMode.VelocityChange);
        }
    }

    public void Death()
    {
        isDead = true;
        deathEvent?.Invoke();
        if (autoDeath) StartCoroutine("DelayDeath");
    }

    IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(autoDeathTime);
        if (!destroyParent)
            Destroy(gameObject);
        else Destroy(transform.parent.gameObject);
    }

    void ResolveHitstun()
    {
        if (blockstunValue > 0)
        {
            inBlockStun = true;
            blockstunValue--;
        }
        else if (blockstunValue <= 0 && inBlockStun)
        {
            GoToState(State.Neutral);
            blockstunValue = 0;
            inBlockStun = false;
        }


        if (hitstunValue > 0 && !hasArmor)
        {
            hitstunValue--;
            inHitStun = true;
        }
        else if (hitstunValue <= 0 && inHitStun)
        {
            GoToState(State.Neutral);
            hitstunValue = 0;
            inHitStun = false;
        }
    }
}

public enum Alignment
{
    Player,
    Enemy
}
public enum StatusEffect { Burning, Frozen };
