using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public HPBar bar;
    public AudioSource source;
    public GameObject boss;
    public float delayStart = 1;
    public float delayFinish = 1;

    public GameObject winFX;
    public IKScript iKScript;
    AttackScript attack;
    public Moveset phase2Moveset;

    public int phase = 1;
    AI ai;
    Status status;
    // Start is called before the first frame update
    void Start()
    {
        ai = boss.GetComponent<AI>();
        attack = boss.GetComponent<AttackScript>();
        status = boss.GetComponent<Status>();

        ai.detectEvent += TriggerBoss;
        status.deathEvent += BossEnd;
    }

    private void Update()
    {
        if (status.rawStats.currentHealth <= 1000)
        {
            if (phase == 1) {
                attack.moveset = phase2Moveset;
                phase = 2;
            }
                
         
        }
    }

    public void TriggerBoss()
    {
        AudioManager.Instance.GoToState(AudioManager.AudioState.Boss);
        AudioManager.Instance.FadeOutVolume();

        StartCoroutine("DelayStart");
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(delayStart);
        source.gameObject.SetActive(true);
        if (iKScript != null)
        {
            iKScript.enabled = true;
        }
    }

    public void BossEnd()
    {
        AudioManager.Instance.FadeOutVolume(source);
        AudioManager.Instance.GoToState(AudioManager.AudioState.Default);
        if (DataManager.Instance.currentSaveData.triggers.dragonKill == 0) {
            DataManager.Instance.currentSaveData.triggers.dragonKill = 1;

        }
        StartCoroutine("DelayFinish");
    }

    IEnumerator DelayFinish()
    {

        yield return new WaitForSeconds(delayFinish);
        Instantiate(winFX);
        source.gameObject.SetActive(true);
        yield return new WaitForSeconds(delayFinish);
        AudioManager.Instance.FadeInVolume();
    }
}
