using DG.Tweening;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class TileMover
{
    Grid _grid;
    public TileMover(Grid grid)
    {
        _grid = grid;
    }
    /// <summary>
    /// Viewのタイルの移動を行うメソッド
    /// </summary>
    /// <param name="dir"></param>
    public void Move(TileController tile, Vector2Int to, System.Action action)
    {
        Vector3Int swipeCellPos = new Vector3Int(to.x,to.y,0);
        Vector3 swipePos = _grid.GetCellCenterWorld(swipeCellPos);
        tile.transform.DOMove(swipePos, 1f)
            .OnComplete(() =>
            {
                action?.Invoke();
            });
    }
}
