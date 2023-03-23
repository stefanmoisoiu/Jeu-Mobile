using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HighScoreSave : MonoBehaviour
{
    [SerializeField] private string highScoreSaveName = "HighScore";
    public async void TrySaveHighScore(int newScore)
    {
        CloudSaveManager.SavedValue currentValue = await CloudSaveManager.GetSaveData(highScoreSaveName, 0);
        int currentHighScore = int.Parse(((string)currentValue.value));
        if (newScore <= currentHighScore) return;
        CloudSaveManager.SaveData(new(highScoreSaveName, newScore));
    }
}
