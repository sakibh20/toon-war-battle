using System;
using UnityEngine;

namespace skb_sec._Project.Scripts.UI_Look_At_Camera
{
    [RequireComponent(typeof(Canvas))]
    public class AssignCamera : MonoBehaviour
    {
        private Camera _mCamera;

        private void OnEnable()
        {
            SetCamera();
        }

        private void OnValidate()
        {
            SetCamera();
        }

        private void SetCamera()
        {
            _mCamera = Camera.main;
            GetComponent<Canvas>().worldCamera = _mCamera;
        }
    }
}
