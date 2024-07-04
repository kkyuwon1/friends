using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasettePlayer : MonoBehaviour
{
    public Vector3 tapeInsertPosition;
    private Collider playerCollider;
    private bool tapeInserted = false; // 테이프가 삽입되었는지 여부를 추적

    void Start()
    {
        playerCollider = GetComponent<Collider>();
        // 게임 시작 시 테이프의 삽입 여부 초기화
        tapeInserted = false;
    }

    public bool IsTapeInside(Collider tapeCollider)
    {
        return playerCollider.bounds.Intersects(tapeCollider.bounds);
    }

    public void SetTapeInserted(bool inserted)
    {
        tapeInserted = inserted;
    }

    public bool IsTapeInserted()
    {
        return tapeInserted;
    }
}