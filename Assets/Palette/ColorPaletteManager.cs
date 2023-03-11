using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ColorPaletteManager : MonoBehaviour
{
    public delegate void OnColorPaletteUpdated(Color[] newColorPalette);
    public static event OnColorPaletteUpdated onColorPaletteUpdated;
    [SerializeField] private Texture2D test;
    public static Color[] currentColorPalette; // => Lightest, Light, Mid, Dark, Darkest

    private void Awake()
    {
        //currentColorPalette = GetColorsFromPaletteTexture(test);
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
