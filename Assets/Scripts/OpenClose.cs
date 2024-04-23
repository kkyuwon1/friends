using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClose : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(animator != null)
        {
            if(Input.GetKeyDown(KeyCode.O))
            {
                animator.SetTrigger("TrOpen");
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                animator.SetTrigger("TrClose");

                if (!audioSource.isPlaying) 
                {
                    audioSource.Play(); 
                }
            }
        }
    }
}
