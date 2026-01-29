using UnityEngine;

public class BoardService
{
    BoardData _boardData;
    TileMover _tileMover;
    /// <summary>/// タイル移動中か/// </summary>
    private bool _isMoving;

    public BoardService(BoardData boardData,TileMover tileMover)
    {
        _boardData = boardData;
        _tileMover = tileMover;
    }
    /// <summary>
    /// 移動可能かタイルデータに問い合わせ、可能なら移動を開始するようにリクエストする。
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="dir"></param>
    public void TryMove(TileController tile, Vector2Int dir)
    {
        if (_isMoving) return;

        Vector2Int from = tile.CellPosition;

        Vector2Int to = from + dir;
        //空きセルか確認
        if (to != _boardData.EmptyCell)
        {
            Debug.LogWarning("移動先が空きセルじゃない");
            return;
        }

        _isMoving = true;

        _boardData.MoveTile(from, to);
        tile.UpdateCell(to);
        _tileMover.Move(tile, to,OnMoveComplete);
    }
    private void OnMoveComplete()
    {
        _isMoving = false;
    }
}
