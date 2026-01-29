using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 入力の受け皿となるコンポーネント
/// </summary>
public class TileInput : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private Collider2D _collider;
    private TileController _tileController;

    private bool _isSelected;
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }
    private void Start()
    {
        _tileController = GetComponent<TileController>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _isSelected = true;
        Debug.Log("選択可能中");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isSelected) return;

        Vector3 dir = GetEdgeDirection(eventData.position);
        if (dir != Vector3.zero)
        {
            Debug.Log($"{dir}にスライド開始");
            _tileController.OnSwipe(dir);
        }
        Deselect();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Deselect();
    }
    private void Deselect()
    {
        if (!_isSelected) return;

        _isSelected = false;
    }
    /// <summary>
    /// 4方向の内どの方向かを受け取る
    /// </summary>
    /// <param name="screenPos">マウスの位置</param>
    private Vector2 GetEdgeDirection(Vector2 screenPos)
    {
        Bounds b = _collider.bounds;

        Vector3 min = Camera.main.WorldToScreenPoint(b.min);
        Vector3 max = Camera.main.WorldToScreenPoint(b.max);

        if (screenPos.x > max.x) return Vector2.right;//より右か
        if (screenPos.x < min.x) return Vector2.left;//より左か
        if (screenPos.y > min.y) return Vector2.up;//より上か
        if (screenPos.y < min.y) return Vector2.down;//より下か

        return Vector2.zero;

    }
}
