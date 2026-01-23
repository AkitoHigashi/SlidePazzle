using UnityEngine;

public class Click : MonoBehaviour
{
    ISelectable _current;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        ISelectable next = null;

        if (hit.collider != null)
        {
                next = hit.collider.GetComponent<ISelectable>();
        }

        // ëIëÇ™ïœÇÌÇ¡ÇΩèuä‘
        if (_current != next)
        {
            _current?.OnDeselect();
            _current = next;
            _current?.Select();
        }
    }
}
