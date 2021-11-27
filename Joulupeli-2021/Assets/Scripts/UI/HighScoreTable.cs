using Assets.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.UI
{
    /// <summary>
    /// Table that shows high scores and the latest score.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class HighScoreTable : MonoBehaviour
    {

        [SerializeField]
        private GameObject highScoreEntryPrefab;

        [SerializeField]
        private GameObject newestScoreEntryPrefab;

        [SerializeField]
        private GameObject horizontalLinePrefab;

        private IList<HighScoreEntry> highScoreEntries = new List<HighScoreEntry>();
        private HighScoreEntry ownScoreEntry;

        private void Start()
        {
            GameMemory memory = GameMemory.Instance;
            memory.OnNewScore += UpdateHighScores;
            UpdateHighScores(memory.HighScores, memory.NewestScore);
        }

        private void OnDestroy()
        {
            GameMemory.Instance.OnNewScore -= UpdateHighScores;
        }

        private void UpdateHighScores(ICollection<HighScore> highScores, HighScore newScore)
        {
            InitHighScoreEntriesIfNeeded();

            List<HighScore> highScoreList = highScores.ToList();
            // Sort from greatest to smallest.
            highScoreList.Sort((left, right) => right.Score.CompareTo(left.Score));

            int i = 0;
            foreach(HighScore score in highScoreList)
            {
                if (i < highScoreEntries.Count)
                {
                    highScoreEntries[i].SetHighScore(score);
                }
                if (score == newScore)
                {
                    ownScoreEntry.SetOrderNumber(i + 1);
                }

                i++;
            }

            ownScoreEntry.SetHighScore(newScore);
        }

        private void InitHighScoreEntriesIfNeeded()
        {
            if (highScoreEntries.Count == 0)
            {
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }

                foreach (int orderNumber in Enumerable.Range(1, 10))
                {
                    GameObject entryObject = Instantiate(highScoreEntryPrefab, transform);
                    HighScoreEntry highScoreEntry = entryObject.GetComponent<HighScoreEntry>();
                    highScoreEntry.SetOrderNumber(orderNumber);
                    highScoreEntry.SetHighScore(null);
                    highScoreEntries.Add(highScoreEntry);
                }

                Instantiate(horizontalLinePrefab, transform);

                GameObject ownScoreObject = Instantiate(newestScoreEntryPrefab, transform);
                ownScoreEntry = ownScoreObject.GetComponent<HighScoreEntry>();
            }
        }
    }
}