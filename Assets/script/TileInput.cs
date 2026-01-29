using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 入力の受け皿となるコンポーネント
/// </summary>
public class TileInput : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private Collider2D _collider;
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Vector3 dir = GetEdgeDirection(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
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
