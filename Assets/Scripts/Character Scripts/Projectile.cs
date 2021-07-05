using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Projectile : MonoBehaviour
{
    public bool instantExplode;
    public bool destroyOnHit;
    public bool destroyOnCollission;
    public LayerMask collissionMask;
    public bool setVelocityOnStart;
    public float velocity;
    protected Rigidbody rb;
    public float baseDamage;
    public float baseKnockback;

    [EnumToggleButtons]
    public Element element;
    public bool instantElement;
    public int elementalSize = 5;
    public int sizeY;

    public int damage;
    public int totalDamage;
    public bool willExplode;
    public float explosionRadius;


    [FoldoutGroup("Debug")] public Move move;
    [FoldoutGroup("Debug")] public Stats stats;
    [FoldoutGroup("Debug")] public Status status;
    public GameObject hitFX;
    public GameObject explosionFX;

    bool healOnce;
    Vector3 collissionPoint;
    Vector3 knockbackDirection;
    Vector3 aVector;
    List<Status> enemyList;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (setVelocityOnStart) rb.velocity = transform.forward * velocity;
        if (instantElement) ElementalEffect();
        if (instantExplode) Explode();
    }

    private void Awake()
    {
        enemyList = new List<Status>();
    }

    private void FixedUpdate()
    {
        if (!setVelocityOnStart)
            rb.velocity = transform.forward * velocity;
    }


    public void ElementalEffect() {
        if (TerrainScript.Instance != null)
        {
            if (element == Element.Water)
                TerrainScript.Instance.WaterTerrain(transform.position, elementalSize);
            else if (element == Element.Earth)
            {
                TerrainScript.Instance.TillTerrain(transform.position, elementalSize);
                //TerrainScript.Instance.TillTerrain(collissionPoint, elementalSize, sizeY);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Status enemyStatus = other.GetComponentInParent<Status>();
        collissionPoint = other.ClosestPoint(transform.position);
        if (!enemyList.Contains(enemyStatus))
        {

            if (enemyStatus != null)
            {
                enemyList.Add(enemyStatus);
                if (status.alignment != enemyStatus.alignment)
                {
                    //if (status)
                    switch (enemyStatus.alignment)
                    {
                        case Alignment.Player:
                            CameraManager.Instance.ShakeCamera(move.shakeMagnitude, move.shakeDuration);
                            break;
                        case Alignment.Enemy:
                            if (status.alignment == Alignment.Player) CameraManager.Instance.ShakeCamera(move.shakeMagnitude, move.shakeDuration);
                            break;
                        default: break;
                    }

                    if (willExplode)
                    {
                        //  GameObject GO = Instantiate(hitFX, other.transform.position + Vector3.up * 2, transform.rotation);
                        Explode();
                    }
                    else
                        DoDamage(enemyStatus);


                    if (destroyOnHit)
                        DestroyEvents();
                }

            }

        }
        if (destroyOnCollission)
        {
            if (status.alignment == Alignment.Player)
            {
                if (other.CompareTag("Player")) return;
            }

            if (other.gameObject.layer != collissionMask) DestroyEvents();

        }

    }

    void DestroyEvents()
    {
        if (willExplode)
        {
            if (explosionFX != null)
                Instantiate(explosionFX, transform.position, transform.rotation);
            Explode();
        }
        if (TerrainScript.Instance != null)
        {
            if (element == Element.Water)
                TerrainScript.Instance.WaterTerrain(collissionPoint, elementalSize);
            else if (element == Element.Earth)
            {
                TerrainScript.Instance.TillTerrain(collissionPoint, elementalSize);
                //TerrainScript.Instance.TillTerrain(collissionPoint, elementalSize, sizeY);

            }
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var item in hitColliders)
        {
            if (element == Element.Fire)
            {
                PlantScript plant = item.GetComponentInParent<PlantScript>();
                if (plant != null) Destroy(plant.gameObject);
            }


            Status tempStatus = item.GetComponentInParent<Status>();

            if (tempStatus != null)
            {
                if (!enemyList.Contains(tempStatus))
                {
                    if (status.alignment != tempStatus.alignment)
                        DoDamage(tempStatus);
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


    public virtual void DoDamage(Status other)
    {
        enemyList.Add(other);

     
        totalDamage = (int)((baseDamage * move.damage * stats.damageModifierPercentage) + (stats.damageModifierFlat));

        int damageDealt = (int)((totalDamage - other.rawStats.resistance) * (1F - other.rawStats.defense));
        if (damageDealt < 0) damageDealt = 0;

        GameManager.Instance.DamageNumbers(other.transform, damageDealt);

        knockbackDirection = (new Vector3(other.transform.position.x, 0, other.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z)).normalized;
        aVector = baseKnockback * knockbackDirection * move.knockback * stats.knockbackModifier;

        other.TakeHit(damageDealt, aVector, (int)(move.hitStun * stats.hitStunMultiplier), (int)(move.poiseBreak * stats.guardBreakModifier), knockbackDirection, move.slowMotionDuration);
    }
}
