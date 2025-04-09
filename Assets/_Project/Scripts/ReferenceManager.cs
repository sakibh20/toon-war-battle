using Doozy.Engine.UI;
using Unity.AI.Navigation;
using UnityEngine;

namespace skb_sec._Project.Scripts
{
    [DefaultExecutionOrder(-1)]
    public class ReferenceManager : MonoBehaviour
    {
        public NavMeshSurface navmeshSurface;
        
        public Transform player;

        public UIView overlayView;
        
        public static ReferenceManager instance;

        private void Awake()
        {
            if (!instance)
            {
                instance = this;
            }
        }
    }
}
