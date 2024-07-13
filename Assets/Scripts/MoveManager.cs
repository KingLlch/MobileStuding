using UnityEngine;

public class MoveManager : MonoBehaviour
{
    [SerializeField] private InputController _inputController;
    [SerializeField] private float speed = 0.01f;

    private void Awake()
    {
        _inputController.MoveEvent.AddListener(Move);
        _inputController.RotateEvent.AddListener(Rotate);
    }

    private void Move(Vector2 move)
    {
        transform.position += new Vector3(move.x, move.y, 0) * speed;
    }

    private void Rotate(Vector2 rotate)
    {
        transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(rotate.x, rotate.y) * Mathf.Rad2Deg); ;
    }
}
