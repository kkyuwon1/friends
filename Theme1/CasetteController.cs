using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;

public class CasetteController : MonoBehaviour, IMixedRealityPointerHandler
{
    private Collider caseCollider;
    private Animator animator;
    public bool isOpen = false;
    private CasettePlayer casettePlayer;
    private AudioSource audioSource;

    void Start()
    {
        caseCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        casettePlayer = transform.parent.GetComponentInChildren<CasettePlayer>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        // 핸드 트래킹으로 클릭 이벤트를 처리
        HandleInteraction();
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        // 필요에 따라 구현
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        // 필요에 따라 구현
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        // 필요에 따라 구현
    }

    private void HandleInteraction()
    {
        if (isOpen)
        {
            animator.SetTrigger("TrClose");
            Invoke("PlayCasette", 3f);
        }
        else
        {
            animator.SetTrigger("TrOpen");
            audioSource.Stop();
        }

        isOpen = !isOpen;
    }

    private void PlayCasette()
    {
        if (casettePlayer.IsTapeInserted())
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}