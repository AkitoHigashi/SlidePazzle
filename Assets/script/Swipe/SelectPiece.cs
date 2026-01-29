using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Threading.Tasks;
public class SelectPiece : MonoBehaviour
    , IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private Collider2D _collider;
    //===設定===
    [SerializeField, Header("グリッド")] Grid _grid;
    [SerializeField, Header("選択可能か")] bool _isSeletable = true;
    [SerializeField, Header("スライド可能か検知する際の距離")] float _swipeThreshold = 30f;
    [SerializeField, Header("スライドする距離")] float _swipeDuration = 2f;

    //===状態===
    private bool _isSelected;
    private bool _isMoving = false;
    private Vector2 _startPos;
    /// <summary>/// 決めた方向のベクター/// </summary>
    private Vector2 _decidedDirection;

    private bool _directionDecided;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _grid = GetComponentInParent<Grid>();
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

        Vector3 dir = GetEdgeDirection(eventData.position);

        if (dir != Vector3.zero)
        {
            Debug.Log($"{dir}にスライド開始");
            if (_isMoving) return;
            MoveTile(dir);
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
        StopSelectAnimation();
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
    private void StopSelectAnimation()
    {
        Debug.Log("アニメーションを消す");
    }
    //===Grid===
    private async Task MoveTile(Vector3 dir)
    {
        _isMoving = true;
        Vector3Int cellPos = _grid.WorldToCell(transform.position);
        Vector3Int swipeCellPos = new Vector3Int((int)dir.x, (int)dir.y, 0) + cellPos;//キャスト不可なんで
        Vector3 swipePos = _grid.GetCellCenterWorld(swipeCellPos);
        await transform.DOMove(swipePos, 1f).AsyncWaitForCompletion();
        _isMoving = false;
    }
}
