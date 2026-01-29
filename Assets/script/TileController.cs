using JetBrains.Annotations;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public Vector2Int CellPosition { get; private set; }

    private BoardService _boardService;

    public void Initialize(BoardService boardService)
    {
        _boardService = boardService;
    }

    /// <summary>
    /// 現在いるセルの位置を更新する
    /// </summary>
    /// <param name="pos"></param>
    public void UpdateCell(Vector2Int pos)
    {
        CellPosition = pos;
    }

    public void OnSwipe(Vector2 direction)
    {
        if (_boardService == null)
        {
            Debug.LogError("BoardServiceが設定されていません");
            return;
        }

        Vector2Int dir = new Vector2Int((int)direction.x, (int)direction.y);

        _boardService.TryMove(this, dir);
    }
}