using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HighScoreText : MonoBehaviour
{
    private TMP_Text _text;
    [SerializeField] private string highScoreSaveName = "HighScore";

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        ConnectServices.onConnected += UpdateText;
    }
    public async void UpdateText()
    {
        CloudSaveManager.SavedValue currentValue = await CloudSaveManager.GetSaveData(highScoreSaveName, 0);
        int currentHighScore = int.Parse(((string)currentValue.value));
        _text.text = currentHighScore.ToString();
    }
}
