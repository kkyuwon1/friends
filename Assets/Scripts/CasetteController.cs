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
        audioSource = GetComponent<AudioSource>(); // AudioSource ������Ʈ�� ������
    }

    void OnMouseDown()
    {
        if (isOpen)
        {
            animator.SetTrigger("TrClose");
            Invoke("PlayCasette", 3f); // 2�� ��� �� PlayCasette �޼��� ȣ��
        }
        else
        {
            animator.SetTrigger("TrOpen");
            audioSource.Stop(); // ���̽��� ������ �뷡�� �����մϴ�.
        }

        isOpen = !isOpen;
    }

    void PlayCasette()
    {
        if (casettePlayer.IsTapeInserted())
        {
            audioSource.Play(); // �������� ���ԵǾ� ������ �뷡�� ����մϴ�.
        }
        else
        {
            audioSource.Stop(); // �������� ���ԵǾ� ���� ������ �뷡�� �����մϴ�.
        }
    }
}
