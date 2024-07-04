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
        // 포인터가 물체를 처음으로 잡았을 때 초기 위치를 설정합니다.
        initialGrabPosition = eventData.Pointer.Result.Details.Point;
        initialObjectPosition = transform.position;

        isDragging = true;
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        if (isDragging)
        {
            // 포인터의 현재 위치와 이전 위치의 차이를 계산하여 물체를 이동합니다.
            Vector3 delta = eventData.Pointer.Result.Details.Point - initialGrabPosition;
            transform.position = initialObjectPosition + delta;
        }
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        if (isDragging)
        {
            isDragging = false;

            // 이후 로직은 그대로 유지됩니다.
            Collider tapeCollider = GetComponent<Collider>();
            tapeCollider.enabled = true;

            if (casetteController.isOpen && casettePlayer.IsTapeInside(tapeCollider))
            {
                transform.position = casettePlayer.tapeInsertPosition;//테이프위치를 입력받은 위치로 넣는다.

                transform.rotation = Quaternion.Euler(90, -180, 0);

                casettePlayer.SetTapeInserted(true);
            }
            else
            {
                transform.position = initialObjectPosition;
                casettePlayer.SetTapeInserted(false);
            }

            // 디버그 메시지 출력
            if (casettePlayer.IsTapeInside(tapeCollider))
            {
                Debug.Log("테이프가 카세트 안에 있습니다.");
            }
            else
            {
                Debug.Log("테이프가 카세트 안에 없습니다.");
            }
        }
    }

    // 나머지 이벤트는 구현하지 않음
    public void OnPointerClicked(MixedRealityPointerEventData eventData) { }
}