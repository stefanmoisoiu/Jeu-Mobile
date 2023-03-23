using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ColorPaletteManager : MonoBehaviour
{
    public static ColorPaletteManager Instance { get; private set; }
    [SerializeField] private Texture2D baseColorPalette;
    public delegate void OnColorPaletteUpdated(Color[] newColorPalette);
    public static event OnColorPaletteUpdated onColorPaletteUpdated;
    public static Color[] currentColorPalette; // => Lightest, Light, Mid, Dark, Darkest
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        currentColorPalette = GetColorsFromPaletteTexture(baseColorPalette);
    }
    public static Color[] GetColorsFromPaletteTexture(Texture2D palette)
    {
        Color[] colors = palette.GetPixels();
        return colors;
    }
    public static void SetColorPalette(Color[] newColorPalette)
    {
        currentColorPalette = newColorPalette;
        onColorPaletteUpdated?.Invoke(currentColorPalette);
    }
}
