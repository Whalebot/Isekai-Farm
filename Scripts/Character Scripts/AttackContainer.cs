using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackContainer : MonoBehaviour
{
    public GameObject[] hitboxes;
    [HideInInspector] public Status status;
    [HideInInspector] public Stats stats;
    [HideInInspector] public Move move;
 public Transform target;
    public GameObject[] visuals;
    int attackNumber = 0;
    bool interrupted;

    private void Start()
    {
   }



    public void ActivateHitbox()
    {
        if (hitboxes.Length > attackNumber && !interrupted)
        {

            ActivateGameobjects(attackNumber);
            attackNumber++;
        }
    }

    void ActivateGameobjects(int number)
    {
        foreach (GameObject hitbox in hitboxes)
        {
            hitbox.SetActive(false);
        }


        foreach (GameObject visual in visuals)
        {
            visual.SetActive(false);
        }


        hitboxes[number].SetActive(true);
        visuals[number].SetActive(true);
    }

    IEnumerator ShadowHit(int number)
    {
        yield return new WaitForSeconds(0.2F);
        {
            ActivateGameobjects(number);
        }
        yield return new WaitForSeconds(0.1F);
        DeactivateHitbox();
    }

    public void InterruptAttack()
    {
        attackNumber = hitboxes.Length;
        interrupted = true;
        StopAllCoroutines();
        DeactivateHitbox();
    }

    public void DeactivateHitbox()
    {
        foreach (GameObject hitbox in hitboxes)
        {

            hitbox.SetActive(false);
        }
    }
}
