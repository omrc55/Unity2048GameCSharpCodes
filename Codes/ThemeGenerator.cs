using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ThemeGenerator
{
    public Color32 bgColor;
    public Color32 bgColorOpen;
    public Color32 textColor;
    public List<SquareGenerator> squares;


    public ThemeGenerator(Color32 newBgColor, Color32 newBgColorOpen, Color32 newTextColor, List<SquareGenerator> newSquares)
    {
        bgColor = newBgColor;
        bgColorOpen = newBgColorOpen;
        textColor = newTextColor;
        squares = newSquares;
    }
}
