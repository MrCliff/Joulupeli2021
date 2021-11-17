using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Spawns items at random positions along the main camera's top edge.
    /// </summary>
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField]
        private Camera mainCamera;

        [SerializeField]
        private float spawnAreaAboveTopEdge = 3;

        [SerializeField]
        private float spawnAreaMargin = 1;

        [SerializeField]
        [Min(0)]
        private float initialSpawnIntervalSec = 2;

        [SerializeField]
        [Range(0, 1)]
        private float spawnSpeedupPercent = 0.1f;

        [SerializeField]
        private string itemPropertyResourceDirectoryPath = "ItemProperties";

        [SerializeField]
        private ItemPool itemPool = new ItemPool();

        [SerializeField]
        private CatchableItem.CommonProperties commonItemProperties;

        private ItemProperties[] itemProperties;
        private Vector3 spawnLineLeftEnd;
        private Vector3 spawnLineRightEnd;
        private int spawnCycle = 0;

        private Coroutine spawningRoutine;

        /// <summary>
        /// Pool for all <see cref="CatchableItem"/> objects.
        /// </summary>
        public ItemPool ItemPool { get { return itemPool; } }

        private void Awake()
        {
            itemProperties = LoadItemProperties();
        }

        private void Start()
        {
            //StartSpawning();
        }

        public void StartSpawning()
        {
            UpdateSpawnArea();
            spawningRoutine = StartCoroutine(SpawnItems());
        }

        public void StopSpawning()
        {
            StopCoroutine(spawningRoutine);
        }

        /// <summary>
        /// Updates the spawn area to match the main cameras viewport width.
        /// </summary>
        private void UpdateSpawnArea()
        {
            float zeroRelativeToCameraZ = 0 - mainCamera.transform.position.z;
            Vector3 cameraUpperLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, zeroRelativeToCameraZ));
            Vector3 cameraUpperRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, zeroRelativeToCameraZ));
            spawnLineLeftEnd = cameraUpperLeft + Vector3.up * spawnAreaAboveTopEdge + Vector3.right * spawnAreaMargin;
            spawnLineRightEnd = cameraUpperRight + Vector3.up * spawnAreaAboveTopEdge + Vector3.left * spawnAreaMargin;
        }

        /// <summary>
        /// Coroutine that waits for a bit and then spawns item in an infinite loop.
        /// </summary>
        /// <returns>Wait instructions returned using yield return.</returns>
        private IEnumerator SpawnItems()
        {
            while (true)
            {
                yield return new WaitForSeconds(initialSpawnIntervalSec * Mathf.Pow(1 - spawnSpeedupPercent, spawnCycle));

                UpdateSpawnArea();
                SpawnRandomItem();

                spawnCycle++;
            }
        }

        /// <summary>
        /// Spawns random item at random position.
        /// </summary>
        private void SpawnRandomItem()
        {
            Vector2 spawnPos = new Vector2(
                Random.Range(spawnLineLeftEnd.x, spawnLineRightEnd.x),
                Random.Range(spawnLineLeftEnd.y, spawnLineRightEnd.y)
            );
            ItemProperties properties = itemProperties[Random.Range(0, itemProperties.Length)];
            CatchableItem item = itemPool.GetItem(properties);
            item.transform.position = spawnPos;
            item.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            item.AssignRandomPropertiesFrom(commonItemProperties);
        }

        /// <summary>
        /// Loads all <see cref="ItemProperties"/> objects from the specified resource directory.
        /// </summary>
        /// <returns>Loaded resources.</returns>
        private ItemProperties[] LoadItemProperties()
        {
            return Resources.LoadAll<ItemProperties>(itemPropertyResourceDirectoryPath);
        }

        private void OnDrawGizmos()
        {
            UpdateSpawnArea();
            if (spawnLineLeftEnd != null && spawnLineRightEnd != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(spawnLineLeftEnd, spawnLineRightEnd);
            }
        }
    }
}
