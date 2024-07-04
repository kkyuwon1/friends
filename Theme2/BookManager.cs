using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] bookPieces; // å��
    [SerializeField] private GameObject[] dropAreas; // �� å�� �� �� ��ġ��
    [SerializeField] private GameObject drawer; // ����
    
 
    private Dictionary<GameObject, GameObject> pieceToDropAreaMap = new Dictionary<GameObject, GameObject>(); // ������ �ش� ��� ���� ����
    private bool bookCompleted = false;
    private Animator animator;


    void Start()
    {
        for (int i = 0; i < bookPieces.Length; i++)
        {
            pieceToDropAreaMap[bookPieces[i]] = dropAreas[i];
        }

        // ���� ã��
        drawer = GameObject.Find("Draw 3");
        if (drawer == null)
        {
            Debug.LogError("Drawer with name 'Draw 3' not found!");
        }

        // Animator ������Ʈ ��������
        animator = drawer.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on drawer!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!bookCompleted)
        {
            bool allPiecesInPlace = true;

            // �� ���� ������ ���� Ȯ��
            foreach (GameObject piece in bookPieces)
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
                HandleBookCompletion(); // ���� �ϼ� ó�� �Լ� ȣ��
            }
        }
    }

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
    void HandleBookCompletion()
    {
        Debug.Log("Book completed!");

        // ���� �ϼ� �� �ִϸ��̼� -> If�� ���
        if (drawer != null)
        {
            animator.SetTrigger("Open");
        }

            // ���� �ϼ� ó�� �� puzzleCompleted ������ true�� ����
            bookCompleted = true;
    }
}
