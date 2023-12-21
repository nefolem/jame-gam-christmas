using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _player;
    [SerializeField] private float _threshold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = CursorPosition.GetMousePosition();
        Vector3 targetPos = (_player.transform.position + mousePos) / 2f;

        targetPos.x = Mathf.Clamp(targetPos.x, -_threshold + _player.position.x, _threshold + _player.position.x);
        targetPos.z = Mathf.Clamp(targetPos.z, -_threshold + _player.position.z, _threshold + _player.position.z);

        transform.position = targetPos;
    }
}
