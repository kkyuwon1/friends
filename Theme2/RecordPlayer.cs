using UnityEngine;

public class RecordPlayer : MonoBehaviour
{
    public GameObject record; // 회전할 레코드 오브젝트
    public AudioSource audioSource; // 재생할 오디오 소스
    public float rotationSpeed = 100f; // 레코드 회전 속도

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
