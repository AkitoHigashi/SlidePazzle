using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class InputBuffer : MonoBehaviour
{
    private string _moveAction = "Move";
    private string _deltaAction = "Look";

    public InputAction PlayerMove => _playerMove;
    public InputAction MouseDelta => _mouseDelta;

    private InputAction _playerMove;
    private InputAction _mouseDelta;

    private void Awake()
    {
        if (TryGetComponent<PlayerInput>(out var playerInput))
        {
            _playerMove = playerInput.actions[_moveAction];
            _mouseDelta = playerInput.actions[_deltaAction];
        }
    }
}
