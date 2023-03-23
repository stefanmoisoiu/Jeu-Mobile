using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MenuColorPaletteSelector : MonoBehaviour
{
    [SerializeField] private GameObject lockImage;
    [SerializeField] private Image[] lightestColorPalettePreviews, lightColorPalettePreviews, midColorPalettePreviews, darkColorPalettePreviews, darkestColorPalettePreviews;
    [SerializeField] private TMP_Text requiredScoreText;
    [SerializeField] private string highScoreSaveData = "HighScore", colorPaletteIndexSaveData = "ColorPaletteIndex";
    [SerializeField] private UnlockableColorPalette[] unlockableColorPalettes;
    private int currentColorPalettePreviewIndex = 0;

    private void Start()
    {
        ConnectServices.onConnected += OnConnectedToUGS;
    }
    private async void OnConnectedToUGS()
    {
        int paletteIndex = await GetSavedPaletteIndex();
        currentColorPalettePreviewIndex = paletteIndex;

        Color[] newColorPalette = ColorPaletteManager.GetColorsFromPaletteTexture(unlockableColorPalettes[paletteIndex].texture);
        ColorPaletteManager.SetColorPalette(newColorPalette);
        SetPreviewColorPalette(newColorPalette);
    }
    private async Task<int> GetSavedPaletteIndex()
    {
        CloudSaveManager.SavedValue savedValue = await CloudSaveManager.GetSaveData(colorPaletteIndexSaveData, 0);
        int value = int.Parse(((string)savedValue.value));
        Debug.Log($"Saved color palette index: {value}");
        return value;
    }
    public async void SelectColorPalettePreview(int indexOffset)
    {
        int newIndex = currentColorPalettePreviewIndex + indexOffset;
        if (newIndex < 0) newIndex = unlockableColorPalettes.Length - 1;
        if (newIndex >= unlockableColorPalettes.Length) newIndex = 0;

        currentColorPalettePreviewIndex = newIndex;

        CloudSaveManager.SavedValue highScoreSavedValue = await CloudSaveManager.GetSaveData(highScoreSaveData, 0);
        int value = int.Parse(((string)highScoreSavedValue.value));

        Color[] newColorPalette = ColorPaletteManager.GetColorsFromPaletteTexture(unlockableColorPalettes[currentColorPalettePreviewIndex].texture);

        if (value >= unlockableColorPalettes[currentColorPalettePreviewIndex].highScoreRequired)
        {
            CloudSaveManager.SavedValue newColorPaletteIndexSaveData = new(colorPaletteIndexSaveData, currentColorPalettePreviewIndex);
            CloudSaveManager.SaveData(newColorPaletteIndexSaveData);
            ColorPaletteManager.SetColorPalette(newColorPalette);
            lockImage.SetActive(false);
        }
        else
        {
            lockImage.SetActive(true);
            requiredScoreText.text = unlockableColorPalettes[currentColorPalettePreviewIndex].highScoreRequired.ToString();
        }
        SetPreviewColorPalette(newColorPalette);
    }
    private void SetPreviewColorPalette(Color[] colorPalette)
    {
        for (int i = 0; i < lightestColorPalettePreviews.Length; i++) lightestColorPalettePreviews[i].color = colorPalette[0];
        for (int i = 0; i < lightColorPalettePreviews.Length; i++) lightColorPalettePreviews[i].color = colorPalette[1];
        for (int i = 0; i < midColorPalettePreviews.Length; i++) midColorPalettePreviews[i].color = colorPalette[2];
        for (int i = 0; i < darkColorPalettePreviews.Length; i++) darkColorPalettePreviews[i].color = colorPalette[3];
        for (int i = 0; i < darkestColorPalettePreviews.Length; i++) darkestColorPalettePreviews[i].color = colorPalette[4];
        requiredScoreText.color = colorPalette[0];
    }

    [System.Serializable]
    public struct UnlockableColorPalette
    {
        public Texture2D texture;
        public int highScoreRequired;
    }
}
