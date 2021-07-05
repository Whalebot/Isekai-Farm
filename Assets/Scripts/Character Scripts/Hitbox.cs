using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public float baseDamage = 1;
    public float baseKnockback = 1;
    public int totalDamage;
    public AttackContainer container;
    Move move;
    Stats stats;
    Status status;
    public GameObject projectile;

    public GameObject hitFX;
    Vector3 knockbackDirection;
    Vector3 aVector;
    public Transform body;
    [SerializeField] List<Status> enemyList;
    public bool isPlayer;
    MeshRenderer mr;
    Transform colPos;
    private void Start()
    {

    }

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        move = container.move;
        status = container.status;

        stats = container.stats;


        if (body == null) body = transform.parent;

        if (GameManager.Instance.showHitboxes)
        {
            mr.enabled = true;
        }
        else
        {
            mr.enabled = false;
        }


        enemyList = new List<Status>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.IsChildOf(body))
        {
            Status enemyStatus = other.GetComponentInParent<Status>();
            if (enemyStatus != null)
                if (!enemyList.Contains(enemyStatus))
                {
                    colPos = other.gameObject.transform;
                    if (enemyStatus != null)
                    {
                        move = container.move;
                        status = container.status;
                        stats = container.stats;

                        if (status.alignment != enemyStatus.alignment)
                        {
                            //switch (enemyStatus.alignment)
                            //{
                            //    case Alignment.Player:
                            //        CameraManager.Instance.ShakeCamera(move.shakeMagnitude, move.shakeDuration);
                            //        break;
                            //    case Alignment.Enemy:
                            //        if (status.alignment == Alignment.Player) CameraManager.Instance.ShakeCamera(move.shakeMagnitude, move.shakeDuration);
                            //        break;
                            //    default: break;
                            //}
                            enemyList.Add(enemyStatus);
                            Hurtbox hurtbox = other.GetComponent<Hurtbox>();
                            if (hurtbox != null)
                            {

                                DoDamage(enemyStatus, hurtbox.damageMultiplier, hurtbox.poiseMultiplier);
                            }
                            else
                                DoDamage(enemyStatus, 1, 1);
                        }
                    }
                }
        }
    }



    void OnDisable()
    {
        enemyList.Clear();
    }

    void OnEnable()
    {
        enemyList.Clear();

    }


    void DoDamage(Status other, float dmgMod, float poiseMod)
    {

        GameObject GO = Instantiate(hitFX, colPos.position, colPos.rotation);


        totalDamage = (int)(dmgMod * (stats.attack + baseDamage * container.move.damage * stats.damageModifierPercentage) + (stats.damageModifierFlat));

        //bool crit = false;
        ////Crit damage
        //if (stats.critChance > 0)
        //{
        //    if (Random.Range(0, 100) < stats.critChance * 100)
        //    {
        //        crit = true;
        //        totalDamage = (int)(totalDamage * stats.critMultiplier);
        //        Instantiate(PowerupManager.Instance.critSound, other.transform.position + Vector3.up * 2, transform.rotation);
        //    }
        //}

        float sum = move.slashValue + move.bluntValue + move.chopValue + move.thrustValue;
        float slash = move.slashValue / sum;
        float blunt = move.bluntValue / sum;
        float thrust = move.thrustValue / sum;
        float chop = move.chopValue / sum;


        int damageDealt = (int)(

            ((totalDamage * move.slashValue / 100) * (1F - other.rawStats.slashDefense / 100) +
            (totalDamage * move.bluntValue / 100) * (1F - other.rawStats.bluntDefense / 100) +
            (totalDamage * move.thrustValue / 100) * (1F - other.rawStats.thrustDefense / 100) +
             (totalDamage * move.chopValue / 100) * (1F - other.rawStats.chopDefense / 100))
            - other.rawStats.resistance);

        if (damageDealt > 0) CameraManager.Instance.ShakeCamera(move.shakeMagnitude, move.shakeDuration);
        if (damageDealt < 0) damageDealt = 0;


        //  int damageDealt = (int)((totalDamage - other.rawStats.resistance) * (1F - other.rawStats.defense));
        GameManager.Instance.DamageNumbers(other.transform, damageDealt);

        ////fx.damage = damageDealt;
        ////fx.ChangeAbility();

        knockbackDirection = (new Vector3(other.transform.position.x, 0, other.transform.position.z) - new Vector3(body.position.x, 0, body.position.z)).normalized;
        aVector = baseKnockback * knockbackDirection * move.knockback * stats.knockbackModifier;

        other.TakeHit(damageDealt, aVector, (int)(move.hitStun * stats.hitStunMultiplier), (int)(poiseMod * move.poiseBreak * stats.guardBreakModifier), knockbackDirection, move.slowMotionDuration);
    }
}
