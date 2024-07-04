using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class DragDrop : MonoBehaviour, IMixedRealityPointerHandler
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
                Vector3 newPosition = hitInfo.collider.transform.position;
                newPosition.y += pieceRenderer.bounds.size.y / 2;
                transform.position = newPosition;
                transform.rotation = Quaternion.Euler(0, 83, 0);
                isPlaced = true;
                return;
            }
        }
        
        isPlaced = false; // 퍼즐 조각이 드롭되지 않았음을 다시 설정합니다.
    }
}