using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasetteController : MonoBehaviour
{
    Collider caseCollider;
    Animator animator;
    public bool isOpen = false;
    private CasettePlayer casettePlayer;
    private AudioSource audioSource;

    void Start()
    {
        caseCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        casettePlayer = FindObjectOfType<CasettePlayer>();
        audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트를 가져옴
    }

    void OnMouseDown()
    {
        if (isOpen)
        {
            animator.SetTrigger("TrClose");
            Invoke("PlayCasette", 3f); // 2초 대기 후 PlayCasette 메서드 호출
        }
        else
        {
            animator.SetTrigger("TrOpen");
            audioSource.Stop(); // 케이스가 열리면 노래를 정지합니다.
        }

        isOpen = !isOpen;
    }

    void PlayCasette()
    {
        if (casettePlayer.IsTapeInserted())
        {
            audioSource.Play(); // 테이프가 삽입되어 있으면 노래를 재생합니다.
        }
        else
        {
            audioSource.Stop(); // 테이프가 삽입되어 있지 않으면 노래를 정지합니다.
        }
    }
}
