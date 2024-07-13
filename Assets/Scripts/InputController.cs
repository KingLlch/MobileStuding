using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [HideInInspector] public UnityEvent<Vector2> MoveEvent;
    [HideInInspector] public UnityEvent<Vector2> RotateEvent;

    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private RectTransform _touchZone;
    [SerializeField] private RectTransform _touchHandle;

    private float handleRange = 50f;

    private Vector2 input;
    private Vector2 inputPosition;
    private Vector2 joystickPosition;
    private Vector2 direction;

    private bool isTouchInZone;

    private void Start()
    {
        _playerInput.actions["JoyStickPosition"].performed += Position;
        _playerInput.actions["JoyStick"].performed += OnMove;
        _playerInput.actions["JoyStickTouch"].canceled += OnStop;

    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (isTouchInZone)
        {
            input += context.ReadValue<Vector2>();

            joystickPosition = Vector2.ClampMagnitude(input / 10, 1.0f) * handleRange;

            _touchHandle.anchoredPosition = joystickPosition;

            direction = joystickPosition.normalized;
        }
    }

    private void OnStop(InputAction.CallbackContext context)
    {
        isTouchInZone = false;
        direction = Vector2.zero;
        input = Vector2.zero;
        _touchHandle.localPosition = Vector2.zero;
    }

    private void Position(InputAction.CallbackContext context)
    {
        if (!isTouchInZone)
        {
            inputPosition = context.ReadValue<Vector2>();
            isTouchInZone = RectTransformUtility.RectangleContainsScreenPoint(_touchHandle, inputPosition);
        }
    }

    private void Move()
    {
        MoveEvent.Invoke(direction);
        RotateEvent.Invoke(direction);

    }

    private void Update()
    {
        if (direction != Vector2.zero) Move();
    }
}
