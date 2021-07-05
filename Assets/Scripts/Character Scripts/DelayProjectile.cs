using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayProjectile : Projectile
{
    public GameObject projectile;
    public Transform target;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = target.position;
        StartCoroutine("DelaySpawn");
    }

    IEnumerator DelaySpawn() {
        yield return new WaitForSeconds(delay);
        GameObject GO = Instantiate(projectile, transform.position, transform.rotation);
        GO.GetComponent<Projectile>().stats = stats;
        GO.GetComponent<Projectile>().move = move;
        GO.GetComponent<Projectile>().status = status;

    }
}
