using System;
using UnityEngine;

namespace skb_sec._Project.Core.Joystick_Unity_Character_Controller.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    public class SimpleMove : MonoBehaviour
    {
        private CharacterController _controller;

        [Space, Header("Speed Variables")]
        [SerializeField] private float maxSpeed = 7.0F;
        [SerializeField] private float rotateSpeed = 5.0F;

        [Space]
        [SerializeField] private ControllerInputSO controllerInputSo;

        private Transform _player;
        private float _speed;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _player = _controller.transform;
            _controller.detectCollisions = true;

            Subscribe();
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            controllerInputSo.OnPauseControllerInput += PauseControllerInput;
            controllerInputSo.OnResumeControllerInput += ResumeControllerInput;
        }

        private void UnSubscribe()
        {
            controllerInputSo.OnPauseControllerInput -= PauseControllerInput;
            controllerInputSo.OnResumeControllerInput -= ResumeControllerInput;
        }

        private void Update()
        {
            if (controllerInputSo.inputInfo.speedModifier <= 0) return;
            Rotate();
            Move();
        }
        
        private void PauseControllerInput()
        {
            _speed = 0;
            _controller.enabled = false;
        }

        private void ResumeControllerInput()
        {
            _controller.enabled = true;
        }

        private void Rotate()
        {
            var newRotation = Quaternion.Euler(new Vector3(0, controllerInputSo.inputInfo.moveAngle, 0));

            _player.rotation = Quaternion.Slerp(_player.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        private void Move()
        {
            _speed = maxSpeed * controllerInputSo.inputInfo.speedModifier;

            _speed = Mathf.Clamp(_speed, 0, maxSpeed);

            Vector3 forward = _player.TransformDirection(Vector3.forward);

            _controller.SimpleMove(forward * _speed);
        }
    }
}