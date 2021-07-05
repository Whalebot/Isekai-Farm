using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterAnimator : MonoBehaviour
{
    private Status status;
    private Animator anim;
    private Movement movement;
    private Dodge dodge;
    private AttackScript attack;
    private float runSpeed;
    private Character character;
    float x, y;
    float zeroFloat = 0f;
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    private float deaccelerateSpeed;
    float tempDirection = 0F;

    AI ai;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponentInParent<Status>();
        anim = GetComponent<Animator>();
        movement = GetComponentInParent<Movement>();
        dodge = GetComponentInParent<Dodge>();
        attack = GetComponentInParent<AttackScript>();
        ai = GetComponentInParent<AI>();

        character = status.character;

        status.hitstunEvent += HitStun;

        if (movement != null)
        {
            movement.jumpEvent += Jump;
        }

        if (dodge != null)
            dodge.dashStartEvent += DodgeAnimation;
        if (attack != null)
        {
            attack.startupEvent += StartAttack;
            attack.recoveryEvent += AttackRecovery;
            status.parryEvent += ParryAnimation;
            status.blockEvent += Block;
        }

        if (ai != null)
        {
            ai.detectEvent += DetectAnimation;
        }
    }

    void DetectAnimation()
    {
        anim.SetBool("Detect", true);
    }

    // Update is called once per frame
    void Update()
    {
        StatusAnimation();
        MovementAnimation();
        BlockAnimation();

       // if (Keyboard.current.eKey.wasReleasedThisFrame) EatAnimation();
    }

    void BlockAnimation()
    {
        anim.SetBool("Blocking", attack.block);
    }

    void Block()
    {
        anim.SetTrigger("Block");
    }

    public void ParryAnimation()
    {
        anim.SetTrigger("Parry");
    }

    public void RefillCan()
    {
        anim.SetInteger("AttackID", 32);
        anim.SetTrigger("Attack");
        anim.SetBool("Attacking", true);
    }


    public void EatAnimation()
    {
        anim.SetInteger("AttackID", 100);
        anim.SetTrigger("Top");
        anim.SetBool("Attacking", true);
    }

    public void EquipAnimation()
    {
        anim.SetInteger("AttackID", 102);
        anim.SetTrigger("Top");
        anim.SetBool("Attacking", true);
    }

    public void PlantAnimation()
    {

        anim.SetInteger("AttackID", 103);
        anim.SetTrigger("Attack");
        // anim.SetTrigger("Top");
        anim.SetBool("Attacking", true);
    }


    public void UnequipAnimation()
    {
        anim.SetInteger("AttackID", 101);
        anim.SetTrigger("Top");
        anim.SetBool("Attacking", true);
    }

    void StatusAnimation()
    {
        anim.SetBool("Dead", status.isDead);
        anim.SetBool("Hitstun", status.inHitStun);
        anim.SetBool("InAnimation", status.currentState == Status.State.InAnimation || status.currentState == Status.State.Blockstun);

        anim.SetFloat("AttackSpeed", status.rawStats.attackSpeed);
        anim.SetFloat("MovementSpeed", status.rawStats.movementSpeedModifier);
    }

    void HitStun()
    {
        anim.SetFloat("HitX", status.knockbackDirection.x);
        anim.SetFloat("HitY", status.knockbackDirection.y);
        anim.SetTrigger("Hit");

    }

    void MovementAnimation()
    {
        if (movement == null) return;
        RunSpeed();
        tempDirection = Mathf.Sign(movement.deltaAngle);
        // anim.SetFloat("Direction", tempDirection);

        anim.SetBool("Walking", movement.isMoving);

        anim.SetBool("Strafe", movement.strafe && !movement.sprinting);
        x = Mathf.Lerp(x, movement.RelativeToForward().normalized.x, maxSpeed);
        y = Mathf.Lerp(y, movement.RelativeToForward().normalized.z, maxSpeed);

        anim.SetBool("Ground", movement.ground);

        anim.SetFloat("Horizontal", x);
        anim.SetFloat("Vertical", y);

        if (movement.rb.velocity.y < -0.5F)
            anim.SetInteger("Falling", -1);
        else anim.SetInteger("Falling", 1);
    }

    void DodgeAnimation()
    {
        if (dodge == null) return;
        anim.SetTrigger("Dodge");
    }

    void Release() { anim.SetTrigger("Release"); }

    void StartAttack()
    {

        anim.SetTrigger("Attack");
        anim.SetBool("Attacking", true);
        anim.SetInteger("AttackID", attack.attackID);

    }



    void AttackRecovery()
    {
        anim.SetBool("Attacking", false);
    }



    private void RunSpeed()
    {
        if (!movement.isMoving) runSpeed = Mathf.Lerp(runSpeed, 0, deaccelerateSpeed);
        else if (movement.sprinting) runSpeed = Mathf.Lerp(runSpeed, 1, deaccelerateSpeed);
        else if (movement.run) runSpeed = Mathf.Lerp(runSpeed, 0.6F, deaccelerateSpeed);
        else if (movement.isMoving) runSpeed = Mathf.Lerp(runSpeed, 0.25F, deaccelerateSpeed);


        anim.SetFloat("RunSpeed", Mathf.Abs(runSpeed));
    }

    void Jump()
    {
        anim.SetTrigger("Jump");
    }

    void FootL() { }
    void FootR() { }

    void Land()
    {
    }
    void Hit()
    {

    }
}
