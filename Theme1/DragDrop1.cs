using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class DragDrop1 : MonoBehaviour, IMixedRealityPointerHandler
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isPlaced = false;
    private Renderer pieceRenderer;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        pieceRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (isPlaced) return;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
        bool nearDropArea = false;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("DropArea1"))
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
            if (hitInfo.collider.CompareTag("DropArea1"))
            {
                transform.position = hitInfo.collider.transform.position;
                transform.rotation = hitInfo.collider.transform.rotation;
                isPlaced = true;
                return;
            }
        }

        // 드롭 영역을 찾지 못한 경우, 퍼즐 조각을 원래 위치와 회전으로 되돌립니다.
        
        isPlaced = false; // 퍼즐 조각이 드롭되지 않았음을 다시 설정합니다.
    }
}