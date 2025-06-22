using UnityEngine;
using UnityEngine.Pool;
namespace ShapeCatcher
{
    public class ShapePooler : MonoBehaviour
    {
        // public ShapePool[] shapePools;
        public ShapePoolSO[] shapePools;

        private void Awake()
        {
            SpawnPreWarmPool();
        }

        private void SpawnPreWarmPool()
        {
            for (int index = 0; index < shapePools.Length; index++)
            {
                var shape = shapePools[index];

                shape.pool = new ObjectPool<GameObject>(
                    createFunc: () =>
                    {
                        Debug.Log("index Calling");
                        GameObject obj = Instantiate(shape.GetPrefab);
                        if (obj.TryGetComponent<Shape>(out Shape shapescript))
                        {
                            shapescript.getandsetIndex = shape.GetIndex;
                        }
                        obj.name = shape.GetPrefab.name + "_Pooled_" + index;
                        return obj;
                    },
                    actionOnGet: obj => obj.SetActive(true),
                    actionOnRelease: obj => obj.SetActive(false),
                    actionOnDestroy: obj => Destroy(obj),
                    collectionCheck: false,
                    defaultCapacity: shape.GetInitialSize,
                    maxSize: 20
                );

                // Pre-warm the pool
                Debug.Log($"[{shape.GetPrefab.name}] Initializing {shape.GetInitialSize} objects");
                for (int i = 0; i < shape.GetInitialSize; i++)
                {
                    var obj = shape.pool.Get();
                    Debug.Log($"Created: {obj.name}");
                    shape.pool.Release(obj);
                }
            }
        }

        // Randomly get from one of the pools
        public GameObject GetRandomShape()
        {
            int index = Random.Range(0, shapePools.Length);
            Debug.Log("index|" + index);
            return shapePools[index].pool.Get();
        }

        public void ReturnToPool(GameObject obj, int poolIndex)
        {
            shapePools[poolIndex].pool.Release(obj);
        }


        public void GetTargetIndexAndShape(out Sprite sprite, out int targetindex)
        {
            ShapePoolSO shape = shapePools[Random.Range(0, shapePools.Length)];
            GameObject shapeprefab = shape.GetPrefab;
            Shape shapeScript = shapeprefab.GetComponent<Shape>();
            targetindex = shape.GetIndex;
            sprite = shapeScript.getSprite;
        }


    }


    [System.Serializable]
    public class ShapePool
    {
        public GameObject prefab;
        public int InitialSize = 5;
        public int Index;
        [HideInInspector] public ObjectPool<GameObject> pool;
    }

}
