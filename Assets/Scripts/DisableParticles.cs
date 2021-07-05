using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableParticles : MonoBehaviour
{
    public GameObject[] particles;
    public GameObject[] audioSources;
    // Start is called before the first frame update
    void Start()
    {
    }


    public void InterruptAttack()
    {
    }

    public void ActivateParticle(int i)
    {

        DeactivateParticles();
        if (particles.Length > 0)
            if (i < particles.Length)
            {
                ParticleSystem[] ps = particles[i].GetComponentsInChildren<ParticleSystem>();

                foreach (ParticleSystem thisPS in ps)
                {
                    thisPS.Play();
                }

            }
            else
            {
                particles[0].SetActive(true);

                ParticleSystem[] ps = particles[0].GetComponentsInChildren<ParticleSystem>();

                foreach (ParticleSystem thisPS in ps)
                {
                    thisPS.Play();
                }
            }
    }


    public void DeactivateParticles()
    {
        foreach (GameObject particle in particles)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                ParticleSystem[] ps = particles[i].GetComponentsInChildren<ParticleSystem>();

                foreach (ParticleSystem thisPS in ps)
                {
                    thisPS.Stop();
                }
            }
        }
    }

    public void DeactivateSFX()
    {

        
        foreach (GameObject AS in audioSources)
        {
            for (int i = 0; i < audioSources.Length; i++)
            {
                AudioSource[] temp = audioSources[i].GetComponentsInChildren<AudioSource>();

                foreach (AudioSource thisPS in temp)
                {
                    thisPS.Stop();
                }
            }
        }
    }

    // Update is called once per frame
    void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        DeactivateParticles();
        DeactivateSFX();
    }
}
