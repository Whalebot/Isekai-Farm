using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    Status status;
    Movement movement;
    AttackScript attackScript;
    public int dashCost;
    public float dashVelocity;
    public bool dashing;
    public bool recovering;
    public int extraDashes;
    int extraDashCounter = 0;
    int dashCounter;
    [SerializeField]
    public int dashDuration = 30;
    public int dashRecovery;
    float recoveryCounter;
    CharacterSFX sfx;

    public bool quickstep;

    public bool teleport;
    public GameObject graphics;

    public delegate void DashEvent();
    public DashEvent dashStartEvent;

    // Start is called before the first frame update
    public GameObject hurtbox;
    public GameObject mainCollider;
    void Start()
    {
        status = GetComponent<Status>();
        movement = GetComponent<Movement>();
        attackScript = GetComponent<AttackScript>();
        sfx = GetComponentInChildren<CharacterSFX>();

        status.hitstunEvent += Interrupt;
        extraDashCounter = extraDashes;
    }


    private void FixedUpdate()
    {
        if (recovering)
        {
            recoveryCounter--;
            if (recoveryCounter <= 0)
            {
                dashing = false;
                extraDashCounter = extraDashes;
                recovering = false;
                //status.GoToState(Status.State.Neutral);
                attackScript.Idle();
            }
        }

        else if (dashing)
        {
            dashCounter--;
            if (dashCounter <= 0)
            {
                ResetDash();
            }
        }


    }

    public void Interrupt()
    {
        dashing = false;
        recovering = false;
    }

    public void Dash()
    {
        if (status.rawStats.currentStamina <= 0) return;
        if (!dashing || recovering && extraDashCounter > -1)
        {
            status.rawStats.currentStamina -= dashCost;
            extraDashCounter--;
            dashing = true;
            recovering = false;
            dashStartEvent?.Invoke();

            sfx.Roll();
            attackScript.attackString = false;
            if (teleport)
            {
                if (mainCollider != null)
                    mainCollider.layer = LayerMask.NameToLayer("NoClip");
                hurtbox.layer = LayerMask.NameToLayer("NoClip");
            }
            else
                hurtbox.layer = LayerMask.NameToLayer("Invulnerable");

            if (movement.direction != Vector3.zero) transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.forward, new Vector3(movement.direction.x, 0, movement.direction.z), Vector3.up), 0);

            dashCounter = dashDuration;
            movement.rb.velocity = transform.forward * dashVelocity;
            status.GoToState(Status.State.InAnimation);
        }
    }


    public void ResetDash()
    {
        if (teleport)
        {
            //    graphics.SetActive(true);
        }

        if (quickstep) movement.rb.velocity = Vector3.zero;

        if (mainCollider != null)
            mainCollider.layer = LayerMask.NameToLayer("Player");
        hurtbox.layer = LayerMask.NameToLayer("Hurtbox");
        recovering = true;
        recoveryCounter = dashRecovery;
    }


}
