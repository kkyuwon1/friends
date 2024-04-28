using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenClose : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    public GameObject Tape;
    public Transform BoxInterior;
    public TextMeshProUGUI instructionText;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 텍스트 UI가 연결되었는지 확인
        if (instructionText == null)
        {
            Debug.LogError("Instruction text not found!"); // 에러 메시지 출력
        }
        else
        {
            instructionText.text = "Press O key to open"; // 초기 메시지 설정
        }
    }

    void Update()
    {
        if (animator != null)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                animator.SetTrigger("TrOpen");
                if (instructionText != null) // null 체크
                {
                    instructionText.text = "Press P key to put tape";
                }
            }

            if (Input.GetKeyDown(KeyCode.P) && Tape != null && BoxInterior != null)
            {
                Tape.transform.SetParent(BoxInterior);
                Tape.transform.localPosition = Vector3.zero; // 위치 조정
                if (instructionText != null)
                {
                    instructionText.text = "Press C key to close";
                }
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                animator.SetTrigger("TrClose");
                if (Tape != null && Tape.transform.IsChildOf(BoxInterior))
                {
                    if (audioSource != null && !audioSource.isPlaying)
                    {
                        audioSource.Play(); // 노래 재생
                    }
                }

                if (instructionText != null)
                {
                    instructionText.gameObject.SetActive(false); // 텍스트 비활성화
                }
            }
        }
    }
}
