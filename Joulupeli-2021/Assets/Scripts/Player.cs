using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    /// <summary>
    /// Handles player actions.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class Player : MonoBehaviour
    {
        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = GetComponent<Camera>();
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            //context.
        }
    }
}
