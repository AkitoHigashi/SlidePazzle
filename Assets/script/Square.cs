using DG.Tweening;
using UnityEngine;

public class Square : MonoBehaviour, ISelectable
{
    SpriteRenderer _sp;
    Vector3 _originScale;
    Vector3 _bigScale = new Vector3(1.5f, 1.5f, 1);

    Tween _tween;

    void Awake()
    {
        _sp = GetComponent<SpriteRenderer>();
        _originScale = transform.localScale;
    }

    public void Select()
    {
        if (_tween != null && _tween.IsActive()) return;

        _sp.color = Color.yellow;
        _tween = transform.DOScale(_bigScale, 0.2f);
    }

    public void OnDeselect()
    {
        _tween?.Kill();
        _tween = null;

        _sp.color = Color.red;
        transform.localScale = _originScale;
    }
}
