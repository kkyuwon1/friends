using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class DragRecord : MonoBehaviour, IMixedRealityPointerHandler
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isPlaced = false;
    private Renderer pieceRenderer;
    private RecordPlayer recordPlayer;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        pieceRenderer = GetComponent<Renderer>();
        recordPlayer = FindObjectOfType<RecordPlayer>(); // RecordPlayer 스크립트를 찾아 할당합니다.
    }

    void Update()
    {
        if (isPlaced) return;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
        bool nearDropArea = false;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("DropRecord"))
            {
                nearDropArea = true;
                break;
            }
        }
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        // 포인터가 내려갔을 때의 로직
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        // 포인터가 올라갔을 때의 로직
        OnDrop();
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        // 포인터가 클릭되었을 때의 로직
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        // 포인터가 드래그되었을 때의 로직
    }

    public void OnDrop()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, 1f))
        {
            if (hitInfo.collider.CompareTag("DropRecord"))
            {
                Vector3 newPosition = hitInfo.collider.transform.position;
                newPosition.y += pieceRenderer.bounds.size.y / 2;
                transform.position = newPosition;
                transform.rotation = originalRotation; // 원래의 회전 상태를 유지합니다.
                isPlaced = true;

                // RecordPlayer 스크립트에서 애니메이션과 오디오 재생을 시작합니다.
                recordPlayer.StartPlaying();
                return;
            }
        }

        // 드랍이 실패했을 경우 원래 위치와 회전으로 복구합니다.
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        isPlaced = false;
    }
}
