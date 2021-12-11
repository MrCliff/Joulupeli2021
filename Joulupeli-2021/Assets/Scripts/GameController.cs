using Assets.Scripts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    /// <summary>
    /// Controller that handles the general game states.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        public const string PlayerTag = "Player";
        public const string GameControllerTag = "GameController";

        private const string ControlSchemeTouch = "Touch";

        private const string ActionMapInGame = "InGame";
        private const string ActionMapUI = "UI";

        private const string ActionPoint = "Point";
        private const string ActionClick = "Click";

        [SerializeField]
        private GameObject startGamePanel;

        [SerializeField]
        private GameObject highScorePanel;

        [SerializeField]
        private GameObject gameOverPanel;

        [SerializeField]
        private ItemSpawner itemSpawner;

        [SerializeField]
        private PlayerInput playerInput;

        [SerializeField]
        private InputSystemUIInputModule inputModule;

        private GameMemory gameMemory;

        private bool gameEnded = false;

        private void Start()
        {
            gameMemory = GameMemory.Instance;

            DeactivateAllPanels();
            SwitchActionMapTo(ActionMapUI);
            startGamePanel.SetActive(true);

            Debug.Log("Current input devices " + string.Join(", ", InputSystem.devices));
            playerInput.onControlsChanged += input => Debug.Log("New controls are " + input.currentControlScheme);
            playerInput.onActionTriggered += input => Debug.Log("Action triggered: " + input.ToString());
            //ActivateTouchInputsIfPossible();
        }

        /// <summary>
        /// Ends the game and shows the game over panel.
        /// </summary>
        public void EndGame()
        {
            if (gameEnded) return;
            gameEnded = true;

            itemSpawner.StopSpawning();

            SwitchActionMapTo(ActionMapUI);
            gameOverPanel.SetActive(true);
        }

        public void OpenHighScores()
        {
            gameMemory.LoadHighScores();
            TMP_InputField nameInputField = gameOverPanel.GetComponentInChildren<TMP_InputField>();
            gameMemory.AddScore(new HighScore(nameInputField.text, gameMemory.Points));
            gameMemory.SaveHighScores();

            DeactivateAllPanels();
            highScorePanel.SetActive(true);
        }

        public void StartGame()
        {
            DeactivateAllPanels();
            SwitchActionMapTo(ActionMapInGame);

            itemSpawner.StartSpawning();
        }

        public void RestartGame()
        {
            DeactivateAllPanels();
            ReloadCurrentScene();

            gameMemory.ResetPoints();
            gameMemory.ResetLives();
        }

        public void ExitGame()
        {
            Debug.Log("Quitting the game...");
            Application.Quit();
        }

        private void SwitchActionMapTo(string actionMapName)
        {
            playerInput.SwitchCurrentActionMap(actionMapName);
            InputAction pointAction = inputModule.actionsAsset.FindActionMap(actionMapName).FindAction(ActionPoint);
            inputModule.point = InputActionReference.Create(pointAction);
            InputAction clickAction = inputModule.actionsAsset.FindActionMap(actionMapName).FindAction(ActionClick);
            inputModule.leftClick = InputActionReference.Create(clickAction);
        }

        public void ActivateTouchInputsIfPossible()
        {
            if (Touchscreen.current == null)
            {
                return;
            }

            InputDevice[] devices = { Touchscreen.current };
            Debug.Log($"Switching input control scheme to {ControlSchemeTouch} with devices " + string.Join<InputDevice>(", ", devices));
            playerInput.SwitchCurrentControlScheme(ControlSchemeTouch, devices);
        }

        /// <summary>
        /// Deactivates all UI-panels.
        /// </summary>
        private void DeactivateAllPanels()
        {
            startGamePanel.SetActive(false);
            highScorePanel.SetActive(false);
            gameOverPanel.SetActive(false);
        }

        private void ReloadCurrentScene()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.buildIndex);
        }

        /// <summary>
        /// Returns the currently active GameController.
        /// </summary>
        /// <returns></returns>
        public static GameController FindInstance()
        {
            return GameObject.FindWithTag(GameControllerTag).GetComponent<GameController>();
        }
    }
}
