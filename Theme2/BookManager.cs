using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] bookPieces; // 책들
    [SerializeField] private GameObject[] dropAreas; // 각 책이 들어갈 판 위치들
    [SerializeField] private GameObject drawer; // 서랍
    
 
    private Dictionary<GameObject, GameObject> pieceToDropAreaMap = new Dictionary<GameObject, GameObject>(); // 조각과 해당 드롭 영역 매핑
    private bool bookCompleted = false;
    private Animator animator;


    void Start()
    {
        for (int i = 0; i < bookPieces.Length; i++)
        {
            pieceToDropAreaMap[bookPieces[i]] = dropAreas[i];
        }

        // 서랍 찾기
        drawer = GameObject.Find("Draw 3");
        if (drawer == null)
        {
            Debug.LogError("Drawer with name 'Draw 3' not found!");
        }

        // Animator 컴포넌트 가져오기
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

            // 각 퍼즐 조각에 대해 확인
            foreach (GameObject piece in bookPieces)
            {
                // 퍼즐 조각과 해당 드롭 영역을 매핑하여 해당 드롭 영역을 가져옴
                GameObject dropArea = pieceToDropAreaMap[piece];

                // 조각이 해당 드롭 영역과 충돌하고 있는지를 확인하여 자리에 맞는지 판단
                if (!IsPieceInDropArea(piece, dropArea))
                {
                    allPiecesInPlace = false;
                    break;
                }
            }

            // 모든 조각이 자리에 맞았을 때 이벤트를 트리거
            if (allPiecesInPlace)
            {
                HandleBookCompletion(); // 퍼즐 완성 처리 함수 호출
            }
        }
    }

    bool IsPieceInDropArea(GameObject piece, GameObject dropArea)
    {
        Collider pieceCollider = piece.GetComponent<Collider>();
        Collider dropAreaCollider = dropArea.GetComponent<Collider>();

        // 퍼즐 조각과 드롭 영역의 Collider가 모두 존재하는지 확인
        if (pieceCollider != null && dropAreaCollider != null)
        {
            // 조각의 Collider와 드롭 영역의 Collider가 서로 겹치는지 확인
            bool isColliding = pieceCollider.bounds.Intersects(dropAreaCollider.bounds);
            if (isColliding)
            {
                Debug.Log("Piece is colliding with drop area: " + piece.name + " and " + dropArea.name);
            }
            return isColliding;
        }

        return false;
    }

    // 퍼즐 완성 시 처리할 함수
    void HandleBookCompletion()
    {
        Debug.Log("Book completed!");

        // 퍼즐 완성 시 애니메이션 -> If문 사용
        if (drawer != null)
        {
            animator.SetTrigger("Open");
        }

            // 퍼즐 완성 처리 후 puzzleCompleted 변수를 true로 설정
            bookCompleted = true;
    }
}
