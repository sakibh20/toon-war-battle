using Pathfinding;
using UnityEngine;

namespace skb_sec._Project.Scripts.Path_Find
{
    public static class PathUpdater
    {
        public static void Rescan(Collider newObjectCollider){
            Bounds b = newObjectCollider.bounds;
            GraphUpdateObject guo = new GraphUpdateObject(b);
            AstarPath.active.UpdateGraphs(guo);
        }
    }
}
