using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasetteController : MonoBehaviour
{
    Collider caseCollider;
    Animator animator;
    public bool isOpen = false;

    void Start()
    {
        caseCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        if (isOpen)
        {
            animator.SetTrigger("TrClose");
        }
        else
        {
            animator.SetTrigger("TrOpen");
        }

        isOpen = !isOpen;
    }
}
