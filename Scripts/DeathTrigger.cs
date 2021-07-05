using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    Status status;
    public enum EnemyType {
        Slime, Beetle, Dragon
    }
    public EnemyType type;
    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<Status>();
        status.deathEvent += TriggerEvent;
    }

    private void OnDisable()
    {
        status.deathEvent -= TriggerEvent;
    }

    public void TriggerEvent() {
        switch (type) {
            case EnemyType.Dragon: DataManager.Instance.currentSaveData.triggers.dragonKills++; break;
            case EnemyType.Slime: DataManager.Instance.currentSaveData.triggers.slimeKills++; break;
            case EnemyType.Beetle: DataManager.Instance.currentSaveData.triggers.beetleKills++; break;
        }

    }
}
