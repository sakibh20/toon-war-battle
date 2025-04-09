using Sirenix.OdinInspector;
using UnityEngine;

namespace Save_System.Scripts
{
    [CreateAssetMenu(fileName = "Data Save Settings",menuName = "Data Save Setting")]
    public class DataSavingSettings : ScriptableObject
    {
        [GUIColor(1f,1f,0f)]
        [Space] public string securityKey;

    }
}
