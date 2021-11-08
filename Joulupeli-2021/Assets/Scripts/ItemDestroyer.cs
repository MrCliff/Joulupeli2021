using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Destroys all <see cref="CatchableItem"/>s that touch the <see cref="EdgeCollider2D"/> on this GameObject.
    /// </summary>
    [RequireComponent(typeof(EdgeCollider2D))]
    public class ItemDestroyer : MonoBehaviour
    {
        [SerializeField]
        private Camera mainCamera;

        [SerializeField]
        private ItemSpawner spawner;

        [SerializeField]
        private float destroyTriggerBelowBottomOfViewport = 3;

        [SerializeField]
        [Tooltip("How much there should be margin on the X axis. This is added both left and right.")]
        private float destroyTriggerMargin = 10;

        private EdgeCollider2D triggerCollider;

        private void Awake()
        {
            AssignTriggerCollider();
        }

        private void Update()
        {
            UpdateDestroyArea();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            CatchableItem other;
            if (collision.TryGetComponent(out other))
            {
                spawner.ItemPool.Destroy(other);
            }
        }

        private void AssignTriggerCollider()
        {
            triggerCollider = GetComponent<EdgeCollider2D>();
            triggerCollider.isTrigger = true;
        }

        /// <summary>
        /// Updates the spawn area to match the main cameras viewport width.
        /// </summary>
        private void UpdateDestroyArea()
        {
            float zeroRelativeToCameraZ = 0 - mainCamera.transform.position.z;
            Vector3 cameraLowerLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, zeroRelativeToCameraZ));
            Vector3 cameraLowerRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, zeroRelativeToCameraZ));
            Vector3 leftEnd = cameraLowerLeft + Vector3.down * destroyTriggerBelowBottomOfViewport + Vector3.left * destroyTriggerMargin;
            Vector3 rightEnd = cameraLowerRight + Vector3.down * destroyTriggerBelowBottomOfViewport + Vector3.right * destroyTriggerMargin;

            triggerCollider.SetPoints(new List<Vector2> { leftEnd, rightEnd });
        }

        private void OnDrawGizmosSelected()
        {
            AssignTriggerCollider();
            UpdateDestroyArea();
        }
    }
}
