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
        recordPlayer = FindObjectOfType<RecordPlayer>(); // RecordPlayer ��ũ��Ʈ�� ã�� �Ҵ��մϴ�.
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
            if (hitInfo.collider.CompareTag("DropRecord"))
            {
                Vector3 newPosition = hitInfo.collider.transform.position;
                newPosition.y += pieceRenderer.bounds.size.y / 2;
                transform.position = newPosition;
                transform.rotation = originalRotation; // ������ ȸ�� ���¸� �����մϴ�.
                isPlaced = true;

                // RecordPlayer ��ũ��Ʈ���� �ִϸ��̼ǰ� ����� ����� �����մϴ�.
                recordPlayer.StartPlaying();
                return;
            }
        }

        // ����� �������� ��� ���� ��ġ�� ȸ������ �����մϴ�.
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        isPlaced = false;
    }
}
