using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public AttackContainer container;
    Stats stats;
    Move move;
    public GameObject projectile;
    public bool lookAtTarget;
    public Vector3 offset;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (container.target != null) transform.LookAt(container.target.position + offset);
        GameObject GO = Instantiate(projectile, transform.position, transform.rotation);
        GO.GetComponent<Projectile>().stats = container.stats;
        GO.GetComponent<Projectile>().move = container.move;
        GO.GetComponent<Projectile>().status = container.status;
    }
}
