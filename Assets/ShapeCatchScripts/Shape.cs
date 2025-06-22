using UnityEngine;

namespace ShapeCatcher
{
    public abstract class Shape : MonoBehaviour
    {
        public abstract int getandsetIndex { get; set; }
        public abstract Sprite getSprite{ get; }
    }

}
