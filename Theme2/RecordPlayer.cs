using UnityEngine;

public class RecordPlayer : MonoBehaviour
{
    public GameObject record; // ȸ���� ���ڵ� ������Ʈ
    public AudioSource audioSource; // ����� ����� �ҽ�
    public float rotationSpeed = 100f; // ���ڵ� ȸ�� �ӵ�

    private bool isPlaying = false;

    void Update()
    {
        if (isPlaying)
        {
            record.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    public void StartPlaying()
    {
        isPlaying = true;
        audioSource.Play();
    }

    public void StopPlaying()
    {
        isPlaying = false;
        audioSource.Stop();
    }
}
