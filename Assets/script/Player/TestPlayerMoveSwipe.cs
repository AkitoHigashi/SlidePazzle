using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TestPlayerMoveSwipe : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float _moveSpeed = 10.0f;
    //マウス移動のしきい値
    [SerializeField] float _minSwipeThreshold = 1f;
    //オブジェクトの移動距離
    [SerializeField] float __swipeDistance = 5f;
    Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void Move(Vector2 input)
    {
        //_rb.linearVelocity = input.normalized * _moveSpeed;
    }
    public void Swipe(Vector2 inputDelta)
    {
        if (inputDelta.magnitude > _minSwipeThreshold)
        {
            //transform.position = (Vector2)inputDelta * __swipeDistance;
        }
    }
}
