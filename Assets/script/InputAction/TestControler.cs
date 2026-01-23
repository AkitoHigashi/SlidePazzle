using UnityEngine;
using UnityEngine.InputSystem;

public class TestControler : MonoBehaviour
{
    private InputBuffer _inputBuffer;
    private TestPlayerMoveSwipe _playerMove;

    private void Awake()
    {
        _inputBuffer = GetComponent<InputBuffer>();
        _playerMove = GetComponent<TestPlayerMoveSwipe>();
    }
    private void Start()
    {
        _inputBuffer.PlayerMove.performed += OnInputMove;
        _inputBuffer.MouseDelta.performed += OnInputDelta;
    }
    /// <summary>
    /// ƒ}ƒEƒX‚Ì‚ÌˆÚ“®—Ê‚ðŽó‚¯Žæ‚é
    /// </summary>
    /// <param name="context">ˆÚ“®—Ê</param>
    private void OnInputDelta(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();
            _playerMove.Swipe(input);
        }
    }

    private void OnInputMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();
            _playerMove?.Move(input);
        }
    }
}
