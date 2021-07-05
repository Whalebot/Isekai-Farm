using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // if (other.gameObject.layer == mask)
        {
            Status status = other.gameObject.GetComponentInParent<Status>();
            if (status != null) status.Death();
        }
    }
}
