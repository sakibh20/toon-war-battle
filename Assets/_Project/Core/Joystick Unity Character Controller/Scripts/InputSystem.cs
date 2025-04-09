using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Game_State_Manager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace skb_sec._Project.Core.Joystick_Unity_Character_Controller.Scripts
{
    public class InputSystem : MonoBehaviour
    {
        [SerializeField, Required] private GameStateDataSO gameStateDataSo;
        [SerializeField] private RectTransform knobParent;
        [SerializeField] private Transform knobTransform;
        
        [Space]
        [SerializeField] private bool variableSpeedOnDragDistance;

        [SerializeField] private ControllerInputSO controllerInputSo;

        private bool _firstClick;

        private Vector2 _lastMousePos;

        private RectTransform _knobRectTransform;

        private float _maxDrag;

        [HideInInspector]
        public Vector2 touchDelta;

        private bool _takeInput;
        

        private void Awake()
        {
            _knobRectTransform = knobTransform.GetComponent<RectTransform>();
        }
        
        private void Start()
        {
            _firstClick = true;
            
            _maxDrag = (knobParent.sizeDelta.y / 2) - (_knobRectTransform.sizeDelta.y / 2);

            Subscribe();
        }

        private void OnDisable()
        {
            UnSubscribe();
        }
        
        private void Subscribe()
        {
            controllerInputSo.OnPauseControllerInput += StopInput;
            controllerInputSo.OnResumeControllerInput += StartInput;

            gameStateDataSo.OnGameStateChanged += OnGameStateChanged;
        }

        private void UnSubscribe()
        {
            controllerInputSo.OnPauseControllerInput -= StopInput;
            controllerInputSo.OnResumeControllerInput -= StartInput;
            
            gameStateDataSo.OnGameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged()
        {
            if (gameStateDataSo.currentGameState == GameState.Running)
            {
                StartInput();
            }
        }
        
        private void StartInput()
        {
            _takeInput = true;
        }

        private void StopInput()
        {
            InputRemoved();
            _takeInput = false;
        }
        
        private void Update()
        {
            //No joystick
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                //Clicked on UI
                return;
            }

            //Input Stopped
            if (!_takeInput)
            {
                InputRemoved();
                return;
            }
            
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                
                if (touch.phase == TouchPhase.Began)
                {
                    RepositionJoystick(touch.position);
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    InputRemoved();
                }
                else
                {
                    ControlHandler();
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RepositionJoystick(Input.mousePosition);
                }
                else if (Input.GetMouseButton(0))
                {
                    ControlHandler();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    InputRemoved();
                }
            }
        }

        private void InputRemoved()
        {
            HideJoystick();
            ResetValues();
        }
        
        private void ResetValues()
        {
            knobTransform.localPosition = Vector3.zero;
            controllerInputSo.inputInfo.speedModifier = 0;
        }

        private void RepositionJoystick(Vector3 touchPos)
        {
            knobParent.transform.position = touchPos;

            ShowJoystick();
            
            ControlHandler();
        }

        private void ShowJoystick()
        {
            knobParent.gameObject.SetActive(true);
        }
        
        private void HideJoystick()
        {
            knobParent.gameObject.SetActive(false);
        }

        private void ControlHandler()
        {
            if (!knobParent.gameObject.activeSelf)
            {
                return;
            }
            
            touchDelta = Vector2.zero;
            
            if (Input.touchCount > 0)
            {
                touchDelta = Input.touches[0].deltaPosition;
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    _firstClick = true;
                }
                
                else if (Input.GetMouseButton(0))
                {
                    if (!_firstClick)
                    {
                        touchDelta = (Vector2)Input.mousePosition - _lastMousePos;
                    }

                    _firstClick = false;

                    _lastMousePos = Input.mousePosition;
                }
            }


            if (touchDelta.x != 0 || touchDelta.y != 0)
            {
                var localPosition = knobTransform.localPosition;
                
                localPosition = Vector3.ClampMagnitude(localPosition + new Vector3(touchDelta.x, touchDelta.y, 0), _maxDrag);
                knobTransform.localPosition = localPosition;
                
                controllerInputSo.inputInfo.speedModifier = localPosition.magnitude;
                
                if (variableSpeedOnDragDistance)
                {
                    controllerInputSo.inputInfo.speedModifier /= _maxDrag;
                }

                controllerInputSo.inputInfo.moveAngle = Vector2.Angle(new Vector2(0, 1), localPosition);
                 if (localPosition.x < 0)
                 {
                     controllerInputSo.inputInfo.moveAngle = 360 - controllerInputSo.inputInfo.moveAngle;
                 }
            }
        }
    }
}
