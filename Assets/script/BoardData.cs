using UnityEngine;
/// <summary>
/// 盤面情報の管理のみをするクラス
/// </summary>
class BoardData
{
    /// <summary>/// 空きセルの位置/// </summary>
    public Vector2Int EmptyCell { get; private set; }

    /// <summary>/// タイルの位置リスト/// </summary>
    private TileController[,] _tiles;

    /// <summary>
    /// 盤面のサイズと空きセルの位置を受け取るコンストラクタ
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="empty"></param>
    public BoardData(int width, int height, Vector2Int empty)
    {
        _tiles = new TileController[width, height];
        EmptyCell = empty;
    }

    /// <summary>
    /// タイルの位置を設定する
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="tileController"></param>
    public void SetTile(Vector2Int pos, TileController tileController)
    {
        if (!IsInside(pos))
        {
            Debug.LogError($"{pos}が範囲外です");
            return;
        }
        _tiles[pos.x, pos.y] = tileController;
    }

    /// <summary>
    /// タイルの位置を取得する
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public TileController GetTile(Vector2Int pos)
    {
        if (!IsInside(pos))
        {
            return null;
        }
        return _tiles[pos.x, pos.y];
    }
    /// <summary>
    /// タイル移動の内部処理
    /// </summary>
    /// <param name="from">移動元</param>
    /// <param name="to">移動先</param>
    public void MoveTile(Vector2Int from, Vector2Int to)
    {
        TileController tile = _tiles[from.x, from.y];
        if (tile == null)
        {
            Debug.LogWarning($"{from}にタイルが存在しません");
            return;
        }
        _tiles[to.x, to.y] = tile;
        //空白セルにする
        _tiles[from.x, from.y] = null;
        //空白セルの位置を更新
        EmptyCell = from;
    }
    /// <summary>
    /// 受け取ったPOSが盤面内かどうかを返す
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private bool IsInside(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < _tiles.GetLength(0) && pos.y >= 0 && pos.y < _tiles.GetLength(1);
    }
}
