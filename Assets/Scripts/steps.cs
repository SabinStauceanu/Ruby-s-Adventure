using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steps : MonoBehaviour
     
{

    Rigidbody2D rigidbody2d;
    //private AudioClip[] clips;
    AudioSource audioSrc;
    bool soundEfects = false;
    public AudioClip collectedClips;
    // Start is called before the first frame update

    public void PlaySound(AudioClip clip)
    {
        
       

        if (rigidbody2d.velocity.x != 0 || rigidbody2d.velocity.y != 0)
        {
            soundEfects = true;
        }
        else
        {
            soundEfects = false;
        }

        if (soundEfects == true)
        {
            if (!audioSrc.isPlaying)
                audioSrc.Play();
        }
        else
            audioSrc.Stop();

        PlaySound(collectedClips);


    }

   
   
}
