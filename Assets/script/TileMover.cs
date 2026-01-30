using DG.Tweening;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TileMover
{
    private Grid _grid;
    private int _offsetX;
    private int _offsetY;
    public TileMover(Grid grid, int offsetX, int offsetY)
    {
        _grid = grid;
        _offsetY = offsetY;
        _offsetX = offsetX;
    }
    /// <summary>
    /// Viewのタイルの移動を行うメソッド
    /// </summary>
    /// <param name="tile">移動するタイル</param>
    /// <param name="to">移動先の位置（配列インデックス）</param>
    /// <param name="action">コールバック</param>
    public void Move(TileController tile, Vector2Int to, System.Action action)
    {
        int gridX = to.x - _offsetX;
        int gridY = to.y - _offsetY;
        Vector3Int swipeCellPos = new Vector3Int(gridX, gridY, 0);
        Vector3 swipePos = _grid.GetCellCenterWorld(swipeCellPos);
        tile.transform.DOMove(swipePos, 0.5f)
            .OnComplete(() =>
            {
                action?.Invoke();
            });
    }
}
