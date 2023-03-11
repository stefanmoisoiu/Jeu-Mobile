using UnityEngine;

public class MenuColorPaletteSelector : MonoBehaviour
{
    [SerializeField] private UnlockableColorPalette[] unlockableColorPalettes;
    public static int currentColorPaletteIndex { get; set; }
    private int currentColorPalettePreviewIndex = 0;

    private void Start()
    {
        currentColorPalettePreviewIndex = currentColorPaletteIndex;
        PreviewColorPalette();
    }
    public void SelectColorPalettePreview(int indexOffset)
    {
        int newIndex = currentColorPalettePreviewIndex + indexOffset;
        if (newIndex < 0) newIndex = unlockableColorPalettes.Length - 1;
        if (newIndex >= unlockableColorPalettes.Length) newIndex = 0;

        currentColorPalettePreviewIndex = newIndex;

        PreviewColorPalette();
    }
    public void PreviewColorPalette()
    {
        Color[] previewColorPalette = ColorPaletteManager.GetColorsFromPaletteTexture(unlockableColorPalettes[currentColorPalettePreviewIndex].texture);
        ColorPaletteManager.SetColorPalette(previewColorPalette);
    }

    [System.Serializable]
    public struct UnlockableColorPalette
    {
        public Texture2D texture;
        public int maxScoreRequired;
    }
}
