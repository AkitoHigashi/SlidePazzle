using UnityEngine;

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
    private void Awake()
    {
        _grid = GetComponent<Grid>();
        _offsetX = _col / 2;
        _offsetY = _row / 2;
    }
    private void Start()
    {
        for (int x = 0; x < _col; x++)
        {
            for (int y = 0; y < _row; y++)
            {
                if (x == _col - 1 && y == 0)
                    continue;
                int cellX = x - _offsetX;
                int cellY = y - _offsetY;

                Place(cellX, cellY, _spriteIndex);

                _spriteIndex++;
            }
        }
    }
    private void Place(int x, int y, int spriteIndex)
    {
        if (spriteIndex >= _sprites.Length)
        {
            Debug.LogWarning("スプライトの数が足りません");
            return;
        }
        GameObject obj = Instantiate(_prefab, transform);
        obj.AddComponent<SelectPiece>();
        Vector3Int cell = new Vector3Int(x, y, 0);
        obj.transform.position = _grid.GetCellCenterWorld(cell);
        SpriteRenderer sp = obj.GetComponent<SpriteRenderer>();
        sp.sprite = _sprites[spriteIndex];
    }

}
