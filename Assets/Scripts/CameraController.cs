using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    void LateUpdate()
    {
        transform.position = _player.transform.position - Vector3.forward*10;
    }
}
