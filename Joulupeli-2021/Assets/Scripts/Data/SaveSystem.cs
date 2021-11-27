using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Data
{
    /// <summary>
    /// Utility class that can be used to save and load data.
    /// </summary>
    public class SaveSystem
    {
        private readonly string highScoreSavePath = Application.persistentDataPath + "/HighScores.bin";

        /// <summary>
        /// Saves the given high scores to a file.
        /// </summary>
        /// <param name="highScores">High scores to save.</param>
        public void Save(ICollection<HighScore> highScores)
        {
            Debug.Log(string.Format("Saving high scores to file \"{0}\"", highScoreSavePath));
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(highScoreSavePath, FileMode.OpenOrCreate);

            formatter.Serialize(fileStream, highScores);
            fileStream.Close();
        }

        /// <summary>
        /// Loads saved high scores from the save file.
        /// </summary>
        /// <returns>Highscores as a collection.</returns>
        public ICollection<HighScore> LoadHighScores()
        {
            if (File.Exists(highScoreSavePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fileStream = new FileStream(highScoreSavePath, FileMode.Open);

                object data = formatter.Deserialize(fileStream);
                fileStream.Close();

                return data as ICollection<HighScore>;
            }
            else
            {
                return new List<HighScore>();
            }
        }
    }
}