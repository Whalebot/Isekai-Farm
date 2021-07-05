using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    private Status status;
    public WeaponParticles weaponParticles;
    public HitboxContainer containerScript;
    public Moveset moveset;

    private PlayerInputHandler input;

    Movement movement;
    Dodge dodge;

    Move activeMove;
    CharacterSFX sfx;
    SkillHandler skillHandler;
    public Transform hitboxContainer;

    public enum AttackState { None, Startup, Active, Recovery }
    public AttackState attackState = AttackState.None;

    public delegate void AttackEvent();
    public AttackEvent startupEvent;
    public AttackEvent activeEvent;
    public AttackEvent recoveryEvent;
    public AttackEvent parryEvent;
    public AttackEvent blockEvent;

    [HeaderAttribute("Attack attributes")]
    public int attackID;
    [HideInInspector] public bool canAttack;
    [HideInInspector] public bool attacking;
    public bool attackString;
    public bool holdAttack;
    [HideInInspector] public bool landCancel;
    [HideInInspector] public bool autoAim;
    bool newAttack;
    [HideInInspector] public int combo;
    public GameObject mainCollider;
    public GameObject hurtbox;
    [HideInInspector] public bool fullCancel;
    [HideInInspector] public bool iFrames;
    public bool startupRotation;
    int lastAttackID;
    int momentumCount;
    public bool block;
    //public bool
    public int parryWindow = 10;
    int parryCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        containerScript = GetComponentInChildren<HitboxContainer>();
        status = GetComponent<Status>();
        movement = GetComponent<Movement>();
        dodge = GetComponent<Dodge>();
        sfx = GetComponentInChildren<CharacterSFX>();
        input = GetComponent<PlayerInputHandler>();
        skillHandler = GetComponent<SkillHandler>();

        movement.jumpEvent += Idle;

        status.neutralEvent += ResetCombo;
        status.hurtEvent += HitstunEvent;
        status.deathEvent += HitstunEvent;
    }

    private void FixedUpdate()
    {
        if (startupRotation)
        {
            movement.AttackRotation();
        }

        if (parryCounter > 0)
        {
            status.parrying = true;
            parryCounter--;
        }
        else status.parrying = false;
    }

    public void ParryStart()
    {
        parryCounter = parryWindow;

    }

    public void AttackProperties(Move move)
    {
        momentumCount = 0;
        activeMove = move;
        attackID = move.animationID;

        //Send hitbox & container the information it needs
        GameObject tempGO;
        if (!move.verticalRotation)
        {
            tempGO = Instantiate(activeMove.attackPrefab, transform.position, transform.rotation, hitboxContainer);
        }
        else { tempGO = Instantiate(activeMove.attackPrefab, transform.position, hitboxContainer.rotation, hitboxContainer); }

        AttackContainer attackContainer = tempGO.GetComponentInChildren<AttackContainer>();

        if (movement.strafeTarget != null) attackContainer.target = movement.strafeTarget;

        attackContainer.status = status;
        attackContainer.stats = status.rawStats;
        attackContainer.move = move;


        switch (move.damageType)
        {
            case DamageType.Fire:
                status.rawStats.currentStamina -= move.staminaCost * status.rawStats.fireCost;
                status.rawStats.maxStamina -= move.fatigue * status.rawStats.fireCost;
                break;
            case DamageType.Wind:
                status.rawStats.currentStamina -= move.staminaCost * status.rawStats.windCost;
                status.rawStats.maxStamina -= move.fatigue * status.rawStats.windCost;
                break;
            case DamageType.Water:
                status.rawStats.currentStamina -= move.staminaCost * status.rawStats.waterCost;
                status.rawStats.maxStamina -= move.fatigue * status.rawStats.waterCost;
                break;
            case DamageType.Earth:
                status.rawStats.currentStamina -= move.staminaCost * status.rawStats.earthCost;
                status.rawStats.maxStamina -= move.fatigue * status.rawStats.earthCost;
                break;
            default:
                status.rawStats.currentStamina -= move.staminaCost;
                status.rawStats.maxStamina -= move.fatigue;
                break;
        }



        Startup();
        status.GoToState(Status.State.InAnimation);
        autoAim = move.autoAim;

        ////Rotate in direction of player's left stick when pressing attack buttaon
        //if (input != null)
        //{
        //    if (input.input.controlScheme == ControlScheme.MouseAndKeyboard)
        //        transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.forward, input.MouseDirection(), Vector3.up), 0);
        //    else if (movement.direction != Vector3.zero) transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.forward, new Vector3(movement.direction.x, 0, movement.direction.z), Vector3.up), 0);
        //}
        //else
        //if (movement.direction != Vector3.zero)
        //{
        //    transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.forward, new Vector3(movement.direction.x, 0, movement.direction.z), Vector3.up), 0);
        //}

        AttackMomentum();

        status.animationArmor = activeMove.armor;
        fullCancel = activeMove.fullCancelable;
        holdAttack = activeMove.holdAttack;

        iFrames = activeMove.iFrames;

        {
            if (mainCollider != null)
                mainCollider.layer = LayerMask.NameToLayer("Player");
            hurtbox.layer = LayerMask.NameToLayer("Hurtbox");
        }

        ResetFrames();


        attackString = false;

        startupEvent?.Invoke();
        attacking = true;
        newAttack = true;
        movement.isMoving = false;

        //Skill experience gain
        if (status.alignment == Alignment.Player)
            foreach (SkillExp item in activeMove.trainedSkills)
            {
                skillHandler.SkillEXP(item.skill, item.experience);
            }

    }

    public void LightAttack()
    {
        if (status.rawStats.currentStamina <= 0) return;
        if (moveset.dashAttack != null)
            if (dodge != null)
            {
                if (dodge.recovering)
                {
                    dodge.Interrupt();
                    AttackProperties(moveset.dashAttack);
                    return;
                }
            }
        if (moveset.runningLight != null)
            if (movement.sprinting)
            {
                AttackProperties(moveset.runningLight);
                return;
            }

        if (status.rawStats.currentStamina <= 0) return;
        if (moveset.lightCombo.moves[0] == null) return;

        if (moveset.lightCombo.moves.Length > combo)

            AttackProperties(moveset.lightCombo.moves[combo]);
        else
        {

            AttackProperties(moveset.lightCombo.moves[0]);
            combo = 0;
        }

        if (moveset.lightCombo.moves.Length - 1 > combo)
            combo++;
        else combo = 0;

    }


    public void HeavyAttack()
    {
        if (status.rawStats.currentStamina <= 0) return;
        if (moveset.runningHeavy != null)
            if (movement.sprinting)
            {
                AttackProperties(moveset.runningHeavy);
                return;
            }
        if (moveset.heavyCombo.moves[0] == null) return;

        if (moveset.heavyCombo.moves.Length > combo)

            AttackProperties(moveset.heavyCombo.moves[combo]);
        else
        {

            AttackProperties(moveset.heavyCombo.moves[0]);
            combo = 0;
        }

        if (moveset.heavyCombo.moves.Length - 1 > combo)
            combo++;
        else combo = 0;
    }

    public void ExtraAttack()
    {
        if (status.rawStats.currentStamina <= 0) return;
        if (moveset.extra.moves[0] == null) return;

        if (moveset.extra.moves.Length > combo)

            AttackProperties(moveset.extra.moves[combo]);
        else
        {

            AttackProperties(moveset.extra.moves[0]);
            combo = 0;
        }

        if (moveset.extra.moves.Length - 1 > combo)
            combo++;
        else combo = 0;
    }

    public void Combo(int num)
    {
        if (moveset.combos.Length <= num) return;
        if (status.rawStats.currentStamina <= 0) return;
        if (moveset.combos[num].moves[0] == null) return;

        if (moveset.combos[num].moves.Length > combo)

            AttackProperties(moveset.combos[num].moves[combo]);
        else
        {

            AttackProperties(moveset.combos[num].moves[0]);
            combo = 0;
        }

        if (moveset.combos[num].moves.Length - 1 > combo)
            combo++;
        else combo = 0;
    }


    public void AttackMomentum()
    {
        if (activeMove.overrideVelocity)
            status.rb.velocity = Vector3.zero;
        status.rb.AddForce(transform.right * activeMove.Momentum.x + transform.up * activeMove.Momentum.y + transform.forward * activeMove.Momentum.z, ForceMode.VelocityChange);
    }

    public void AnimMomemtun()
    {
        if (activeMove.noClip)
        {
            hurtbox.layer = LayerMask.NameToLayer("NoClip");
            if (mainCollider != null)
                mainCollider.layer = LayerMask.NameToLayer("NoClip");

        }
        else if (iFrames)
        {
            if (mainCollider != null)
                mainCollider.layer = LayerMask.NameToLayer("Player");
            hurtbox.layer = LayerMask.NameToLayer("Invulnerable");
        }


        if (lastAttackID == attackID) { }

        if (activeMove.overrideVelocity)
            status.rb.velocity = Vector3.zero;
        if (activeMove.momentumArray.Length > momentumCount)
        {
            status.rb.AddForce(transform.right * activeMove.momentumArray[momentumCount].x + transform.up * activeMove.momentumArray[momentumCount].y + transform.forward * activeMove.momentumArray[momentumCount].z, ForceMode.VelocityChange);
        }
        momentumCount++;

    }

    void Startup()
    {
        attackState = AttackState.Startup;
        startupRotation = true;
    }

    public void StartRotation()
    {
        startupRotation = true;
    }


    public void StopRotation()
    {
        startupRotation = false;
    }

    public void Active()
    {
        startupRotation = false;
        autoAim = false;


        //Invoke event for powerups
        activeEvent?.Invoke();

        attackState = AttackState.Active;

        if (sfx != null)
            sfx.AttackSFX(attackID);
        containerScript.ActivateHitbox(0);
    }

    public void ParticleStart()
    {
        if (weaponParticles != null)
            weaponParticles.ActivateParticle(activeMove.particleID);

        containerScript.ActivateParticle(activeMove.particleID);
    }

    public void ParticleEnd()
    {
        if (weaponParticles != null) weaponParticles.DeactivateParticles();
        containerScript.DeactivateParticles();
    }

    public void Recovery()
    {
        attackState = AttackState.Recovery;
        status.animationArmor = false;

        if (activeMove != null)
            if (activeMove.resetVelocityDuringRecovery)
                status.rb.velocity = Vector3.zero;

        containerScript.DeactivateHitboxes();
        if (mainCollider != null)
            mainCollider.layer = LayerMask.NameToLayer("Player");
        if (hurtbox != null)
            hurtbox.layer = LayerMask.NameToLayer("Hurtbox");
        newAttack = false;
    }

    public void AttackLink()
    {
        attackString = true;
    }

    public void ResetFrames()
    {
        containerScript.DeactivateHitboxes();
        recoveryEvent?.Invoke();
        attacking = false;
        attackString = false;
    }

    void ResetCombo()
    {
        combo = 0;
    }

    void HitstunEvent()
    {
        autoAim = false;
        status.animationArmor = false;
        fullCancel = false;
        holdAttack = false;

        if (mainCollider != null)
            mainCollider.layer = LayerMask.NameToLayer("Player");

        hurtbox.layer = LayerMask.NameToLayer("Hurtbox");
        newAttack = false;

        combo = 0;
        attackState = AttackState.None;

        containerScript.InterruptAttack();
        containerScript.DeactivateParticles();
    }

    public void Idle()
    {
        if (!newAttack)
        {
            attackState = AttackState.None;
            autoAim = false;
            attackString = false;
            fullCancel = false;

            combo = 0;
            status.GoToState(Status.State.Neutral);
            containerScript.DeactivateParticles();
            attacking = false;
            recoveryEvent?.Invoke();

        }
    }
}
