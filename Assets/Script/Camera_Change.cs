using Unity.VisualScripting;
using UnityEngine;

public class Camera_Change : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _camera_pos1;
    [SerializeField] private GameObject _camera_pos2;
    private bool _trigger = true;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&_trigger)//修正ポイント
        {
            _camera.transform.position =_camera_pos2.transform.position;
            _camera.transform.rotation = _camera_pos2.transform.rotation;
            _trigger =!_trigger;
        }
        else if (other.CompareTag("Player") && !_trigger)//修正ポイント
        {
            _camera.transform.position = _camera_pos1.transform.position;
            _camera.transform.rotation = _camera_pos1.transform.rotation;
            _trigger = !_trigger;
        }
    }
}
