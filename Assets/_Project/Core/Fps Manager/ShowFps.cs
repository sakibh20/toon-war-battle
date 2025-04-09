using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace _Project.Core.Fps_Manager
{
    public class ShowFps : MonoBehaviour
    {
        [SerializeField] private bool useCustomResolution;
        [SerializeField, ShowIf("useCustomResolution")] private int resolutionWidth;
        [SerializeField, ShowIf("useCustomResolution")] private int resolutionHeight;
        
        [SerializeField] private TMP_Text fpsText;
        private float _deltaTime;

        [SerializeField] private bool showFps;
        [SerializeField] private bool setDefaultFps;
        [SerializeField, ShowIf("setDefaultFps")] private int targetFps = 60;

        private void Start()
        {
            if (useCustomResolution)
            {
                Screen.SetResolution(resolutionWidth, resolutionHeight, false);
            }

            #if !UNITY_EDITOR
            if (setDefaultFps)
            {
                Application.targetFrameRate = targetFps;   
            }
            #endif
        }

        private void Update () 
        {
            if (showFps)
            {
                _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
                string fps = ((int) (1.0f / _deltaTime)).ToString();
                fpsText.text =  fps;   
            }
        }
    }
}
