using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorPalette : MonoBehaviour
{
    [SerializeField] private RendererPalette[] _rendererPalettes;
    public enum PaletteColorType
    {
        Lightest,
        Light,
        Mid,
        Dark,
        Darkest,
    }
    private void Start()
    {
        ColorPaletteManager.onColorPaletteUpdated += SetRendererPalette;
        SetRendererPalette(ColorPaletteManager.currentColorPalette);
    }
    private void SetRendererPalette(Color[] newColorPalette)
    {
        for (int i = 0; i < _rendererPalettes.Length; i++)
        {
            Color targetPaletteColor = newColorPalette[(int)_rendererPalettes[i].paletteColorType];

            if (_rendererPalettes[i].camera) _rendererPalettes[i].camera.backgroundColor = targetPaletteColor;
            if (_rendererPalettes[i].sprite) _rendererPalettes[i].sprite.color = targetPaletteColor;
            if (_rendererPalettes[i].line) { _rendererPalettes[i].line.startColor = targetPaletteColor; _rendererPalettes[i].line.endColor = targetPaletteColor; }
            if (_rendererPalettes[i].text) _rendererPalettes[i].text.color = targetPaletteColor;
            if (_rendererPalettes[i].image) _rendererPalettes[i].image.color = targetPaletteColor;
            if (_rendererPalettes[i].particleSystem)
            {
                ParticleSystem.MainModule pMain = _rendererPalettes[i].particleSystem.main;
                pMain.startColor = targetPaletteColor;
            }
        }
    }
    [System.Serializable]
    public struct RendererPalette
    {
        public Camera camera;
        public SpriteRenderer sprite;
        public LineRenderer line;
        public Image image;
        public TMP_Text text;
        public ParticleSystem particleSystem;
        public PaletteColorType paletteColorType;
    }
}
