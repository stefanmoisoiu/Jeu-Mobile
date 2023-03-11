using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ColorPaletteManager : MonoBehaviour
{
    public delegate void OnColorPaletteUpdated(Color[] newColorPalette);
    public static event OnColorPaletteUpdated onColorPaletteUpdated;
    [SerializeField] private Texture2D _colorPaletteTexture;
    public static Color[] currentColorPalette; // => Lightest, Light, Mid, Dark, Darkest

    private void Awake()
    {
        currentColorPalette = GetColorsFromPaletteTexture(_colorPaletteTexture);
    }
    private Color[] GetColorsFromPaletteTexture(Texture2D palette)
    {
        Color[] colors = palette.GetPixels();
        onColorPaletteUpdated?.Invoke(colors);
        return colors;
    }
}
