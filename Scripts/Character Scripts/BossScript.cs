using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    Status status;
    public AI AI;
   // public BossMusic music;
    public bool isMiniBoss = false;
    // Start is called before the first frame update


    private void Start()
    {
        AI = GetComponent<AI>();
        status = GetComponent<Status>();
        status.deathEvent += Death;

    }

    private void Update()
    {

    }

    void Death() {
        Destroy(GetComponent<AI>());
    }

}
