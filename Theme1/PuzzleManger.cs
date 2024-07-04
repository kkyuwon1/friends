using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] puzzlePieces; // ���� ������
    [SerializeField] private GameObject[] dropAreas; // �� ������ �� ������ ��ġ��
    [SerializeField] private VideoPlayer videoPlayer; // ���� �÷��̾�

    private Dictionary<GameObject, GameObject> pieceToDropAreaMap = new Dictionary<GameObject, GameObject>(); // ������ �ش� ��� ���� ����
    private bool puzzleCompleted = false;

    void Start()
    {
        // ���� ������ �ش� ��� ������ ����
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            pieceToDropAreaMap[puzzlePieces[i]] = dropAreas[i];
        }

        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }
    }

    void Update()
    {
        if (!puzzleCompleted)
        {
            bool allPiecesInPlace = true;

            // �� ���� ������ ���� Ȯ��
            foreach (GameObject piece in puzzlePieces)
            {
                // ���� ������ �ش� ��� ������ �����Ͽ� �ش� ��� ������ ������
                GameObject dropArea = pieceToDropAreaMap[piece];

                // ������ �ش� ��� ������ �浹�ϰ� �ִ����� Ȯ���Ͽ� �ڸ��� �´��� �Ǵ�
                if (!IsPieceInDropArea(piece, dropArea))
                {
                    allPiecesInPlace = false;
                    break;
                }
            }

            // ��� ������ �ڸ��� �¾��� �� �̺�Ʈ�� Ʈ����
            if (allPiecesInPlace)
            {
                HandlePuzzleCompletion(); // ���� �ϼ� ó�� �Լ� ȣ��
            }
        }
    }

    // ������ �ش� ��� ������ �浹�ϰ� �ִ����� Ȯ���ϴ� �Լ�
    bool IsPieceInDropArea(GameObject piece, GameObject dropArea)
    {
        Collider pieceCollider = piece.GetComponent<Collider>();
        Collider dropAreaCollider = dropArea.GetComponent<Collider>();

        // ���� ������ ��� ������ Collider�� ��� �����ϴ��� Ȯ��
        if (pieceCollider != null && dropAreaCollider != null)
        {
            // ������ Collider�� ��� ������ Collider�� ���� ��ġ���� Ȯ��
            bool isColliding = pieceCollider.bounds.Intersects(dropAreaCollider.bounds);
            if (isColliding)
            {
                Debug.Log("Piece is colliding with drop area: " + piece.name + " and " + dropArea.name);
            }
            return isColliding;
        }

        return false;
    }

    // ���� �ϼ� �� ó���� �Լ�
    void HandlePuzzleCompletion()
    {
        Debug.Log("Puzzle completed!");

        // ���� �ϼ� �� ���� ���
        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }

        // ���� �ϼ� ó�� �� puzzleCompleted ������ true�� ����
        puzzleCompleted = true;
    }
}