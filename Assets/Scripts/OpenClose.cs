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

        // �ؽ�Ʈ UI�� ����Ǿ����� Ȯ��
        if (instructionText == null)
        {
            Debug.LogError("Instruction text not found!"); // ���� �޽��� ���
        }
        else
        {
            instructionText.text = "Press O key to open"; // �ʱ� �޽��� ����
        }
    }

    void Update()
    {
        if (animator != null)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                animator.SetTrigger("TrOpen");
                if (instructionText != null) // null üũ
                {
                    instructionText.text = "Press P key to put tape";
                }
            }

            if (Input.GetKeyDown(KeyCode.P) && Tape != null && BoxInterior != null)
            {
                Tape.transform.SetParent(BoxInterior);
                Tape.transform.localPosition = Vector3.zero; // ��ġ ����
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
                        audioSource.Play(); // �뷡 ���
                    }
                }

                if (instructionText != null)
                {
                    instructionText.gameObject.SetActive(false); // �ؽ�Ʈ ��Ȱ��ȭ
                }
            }
        }
    }
}
