using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class TapeController : MonoBehaviour, IMixedRealityPointerHandler
{
    private bool isDragging = false;
    private Vector3 initialGrabPosition;
    private Vector3 initialObjectPosition;
    private CasetteController casetteController;
    private CasettePlayer casettePlayer;

    void Start()
    {
        initialObjectPosition = transform.position;
        casetteController = FindObjectOfType<CasetteController>();
        casettePlayer = FindObjectOfType<CasettePlayer>();
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        // �����Ͱ� ��ü�� ó������ ����� �� �ʱ� ��ġ�� �����մϴ�.
        initialGrabPosition = eventData.Pointer.Result.Details.Point;
        initialObjectPosition = transform.position;

        isDragging = true;
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        if (isDragging)
        {
            // �������� ���� ��ġ�� ���� ��ġ�� ���̸� ����Ͽ� ��ü�� �̵��մϴ�.
            Vector3 delta = eventData.Pointer.Result.Details.Point - initialGrabPosition;
            transform.position = initialObjectPosition + delta;
        }
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        if (isDragging)
        {
            isDragging = false;

            // ���� ������ �״�� �����˴ϴ�.
            Collider tapeCollider = GetComponent<Collider>();
            tapeCollider.enabled = true;

            if (casetteController.isOpen && casettePlayer.IsTapeInside(tapeCollider))
            {
                transform.position = casettePlayer.tapeInsertPosition;//��������ġ�� �Է¹��� ��ġ�� �ִ´�.

                transform.rotation = Quaternion.Euler(90, -180, 0);

                casettePlayer.SetTapeInserted(true);
            }
            else
            {
                transform.position = initialObjectPosition;
                casettePlayer.SetTapeInserted(false);
            }

            // ����� �޽��� ���
            if (casettePlayer.IsTapeInside(tapeCollider))
            {
                Debug.Log("�������� ī��Ʈ �ȿ� �ֽ��ϴ�.");
            }
            else
            {
                Debug.Log("�������� ī��Ʈ �ȿ� �����ϴ�.");
            }
        }
    }

    // ������ �̺�Ʈ�� �������� ����
    public void OnPointerClicked(MixedRealityPointerEventData eventData) { }
}