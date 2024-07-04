using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] puzzlePieces; // 퍼즐 조각들
    [SerializeField] private GameObject[] dropAreas; // 각 조각이 들어갈 퍼즐판 위치들
    [SerializeField] private VideoPlayer videoPlayer; // 비디오 플레이어

    private Dictionary<GameObject, GameObject> pieceToDropAreaMap = new Dictionary<GameObject, GameObject>(); // 조각과 해당 드롭 영역 매핑
    private bool puzzleCompleted = false;

    void Start()
    {
        // 퍼즐 조각과 해당 드롭 영역을 매핑
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

            // 각 퍼즐 조각에 대해 확인
            foreach (GameObject piece in puzzlePieces)
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
                HandlePuzzleCompletion(); // 퍼즐 완성 처리 함수 호출
            }
        }
    }

    // 조각이 해당 드롭 영역과 충돌하고 있는지를 확인하는 함수
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
    void HandlePuzzleCompletion()
    {
        Debug.Log("Puzzle completed!");

        // 퍼즐 완성 시 비디오 재생
        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }

        // 퍼즐 완성 처리 후 puzzleCompleted 변수를 true로 설정
        puzzleCompleted = true;
    }
}