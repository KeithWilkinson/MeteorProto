using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioSource _audioSrc;
    private static AudioClip clickSound;
    private static AudioClip planetDamageSound;
    private static AudioClip meteorCollideSound;

    // Start is called before the first frame update
    void Start()
    {
        _audioSrc = GetComponent<AudioSource>();
        clickSound = Resources.Load<AudioClip>("ButtonPress");
        planetDamageSound = Resources.Load<AudioClip>("DamagePlanet");
        meteorCollideSound = Resources.Load<AudioClip>("MeteorCrash");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // Button press sound
    public static void PlayButtonPressSound()
    {
        _audioSrc.PlayOneShot(clickSound);
    }

    // Meteor collide with planet sound
    public static void PlayPlanetDamageSound()
    {
        _audioSrc.PlayOneShot(planetDamageSound);
    }

    // Meteor collide with meteor sound
    public static void PlayMeteorCollideSound()
    {
        _audioSrc.PlayOneShot(meteorCollideSound);
    }
}
