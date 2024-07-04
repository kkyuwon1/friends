using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasettePlayer : MonoBehaviour
{
    public Vector3 tapeInsertPosition;
    private Collider playerCollider;
    private bool tapeInserted = false; // �������� ���ԵǾ����� ���θ� ����

    void Start()
    {
        playerCollider = GetComponent<Collider>();
        // ���� ���� �� �������� ���� ���� �ʱ�ȭ
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