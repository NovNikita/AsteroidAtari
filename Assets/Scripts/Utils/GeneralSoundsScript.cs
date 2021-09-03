using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script with static method to play explosion sound
public class GeneralSoundsScript : MonoBehaviour
{
    private static AudioSource explosionSoundAS;
    void Start()
    {
        explosionSoundAS = transform.Find("ExplosionSound").GetComponent<AudioSource>();
    }

    public static void PlayExplosionSoundEffect()
    {
        explosionSoundAS.Play();
    }
}
