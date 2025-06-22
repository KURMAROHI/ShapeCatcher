using UnityEngine;
using UnityEngine.Pool;


namespace ShapeCatcher
{

    [CreateAssetMenu(menuName = "Shapes")]
    public class ShapePoolSO : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int Index;
        private int InitialSize = 5;
        [HideInInspector] public ObjectPool<GameObject> pool;
        public GameObject GetPrefab => prefab;
        public int GetInitialSize => InitialSize;
        public int GetIndex => Index;
    }
}
