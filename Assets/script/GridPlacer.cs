using UnityEngine;
/// <summary>
/// GameManegerとしてグリッド上にタイルを配置するコンポーネント
/// </summary>
public class GridPlacer : MonoBehaviour
{
    [Header("タイルの詳細")]
    [SerializeField] Sprite[] _sprites;
    [SerializeField] GameObject _prefab;
    [Header("タイルの生成")]
    [SerializeField] int _col;//列(x)
    [SerializeField] int _row;//行(y)
    private int _offsetX;
    private int _offsetY;
    private int _spriteIndex = 0;

    private Grid _grid;
    private BoardData _boardData;
    private BoardService _boardService;
    private TileMover _tileMover;
    private void Awake()
    {
        _offsetX = _col / 2;
        _offsetY = _row / 2;
        _grid = GetComponent<Grid>();
        InitializeBoard();
    }
    private void InitializeBoard()
    {
        //ボードデータの初期時に空セルの位置を指定するグリッドではない配列のインデックスを渡す
        _boardData = new BoardData(_col, _row, new Vector2Int(_col - 1,0));
        _tileMover = new TileMover(_grid,_offsetX,_offsetY);
        _boardService = new BoardService(_boardData, _tileMover);

    }
    private void Start()
    {
        GenerateTiles();
    }
    private void GenerateTiles()
    {
        for (int x = 0; x < _col; x++)
        {
            for (int y = 0; y < _row; y++)
            {
                if (x == _col - 1 && y == 0)
                    continue;
                int gridX = x - _offsetX;
                int gridY = y - _offsetY;

                Place(gridX, gridY, x, y, _spriteIndex);

                _spriteIndex++;
            }
        }
    }

    private void Place(int gridX, int gridY, int arrayX, int arrayY, int spriteIndex)
    {
        if (spriteIndex >= _sprites.Length)
        {
            Debug.LogWarning("スプライトの数が足りません");
            return;
        }
        //タイルの生成
        GameObject obj = Instantiate(_prefab, transform);
        //グリッド位置に配置
        Vector3Int cell = new Vector3Int(gridX, gridY, 0);
        obj.transform.position = _grid.GetCellCenterWorld(cell);

        SpriteRenderer sp = obj.GetComponent<SpriteRenderer>();
        sp.sprite = _sprites[spriteIndex];

        TileController tileController = obj.GetComponent<TileController>();
        if (tileController != null)
        {
            //初期化
            tileController.Initialize(_boardService);
            tileController.UpdateCell(new Vector2Int(arrayX, arrayY));

            _boardData.SetTile(new Vector2Int(arrayX, arrayY), tileController);
        }
        else
        {
            Debug.LogError("TileControllerコンポーネントがアタッチされていません");
            return;
        }
    }

}
