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
        // �����Ͱ� �������� ���� ����
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        // �����Ͱ� �ö��� ���� ����
        OnDrop();
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        // �����Ͱ� Ŭ���Ǿ��� ���� ����
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        // �����Ͱ� �巡�׵Ǿ��� ���� ����
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

        // ��� ������ ã�� ���� ���, ���� ������ ���� ��ġ�� ȸ������ �ǵ����ϴ�.
        
        isPlaced = false; // ���� ������ ��ӵ��� �ʾ����� �ٽ� �����մϴ�.
    }
}