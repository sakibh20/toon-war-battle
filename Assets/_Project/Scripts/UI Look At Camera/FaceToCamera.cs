using UnityEngine;

namespace skb_sec._Project.Scripts.UI_Look_At_Camera
{
    public class FaceToCamera : MonoBehaviour
    {
        private Camera _mainCamera;

        [SerializeField] private bool continuous;

        private bool _onUpdate;

        private void Start()
        {
            _mainCamera = Camera.main;
            
            Face();

            _onUpdate = continuous;
        }

        private void Update()
        {
            if (!_mainCamera)
            {
                return;
            }

            if (_onUpdate)
            {
                Face();   
            }
        }

        private void Face()
        {
            if (gameObject.activeSelf)
            {
                transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward, _mainCamera.transform.rotation * Vector3.up);
            }
        }
    }
}
