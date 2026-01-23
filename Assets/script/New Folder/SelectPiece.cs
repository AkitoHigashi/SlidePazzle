using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class SelectPiece : MonoBehaviour
    , IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private Collider2D _collider;
    //===設定===
    [SerializeField, Header("選択可能か")] bool _isSeletable = true;
    [SerializeField, Header("スライ可能か検知する際の距離")] float _swipeThreshold = 30f;

    //===状態===
    private bool _isSelected;
    private Vector2 _startPos;
    /// <summary>/// 決めた方向のベクター/// </summary>
    private Vector2 _decidedDirection;

    private bool _directionDecided;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    // ===== Pointer =====

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_isSeletable) return;

        _isSelected = true;
        _directionDecided = false;
        Debug.Log("選択可能中");
        _startPos = eventData.position;

        StartSelectAnimation();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isSelected) return;

        Vector2 dir = GetEdgeDirection(eventData.position);

        if (dir != Vector2.zero)
        {
            Debug.Log($"{dir}にスライド開始");
            this.transform.DOMove(dir*2f,1f);

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
        StopSelectAnimaton();
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
    //===Animation===

    private void StartSelectAnimation()
    {
        Debug.Log("後でアニメーション実装");
    }
    private void StopSelectAnimaton()
    {
        Debug.Log("アニメーションを消す");
    }
}
