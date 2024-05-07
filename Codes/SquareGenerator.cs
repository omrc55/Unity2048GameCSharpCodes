using UnityEngine;

[System.Serializable]
public class SquareGenerator
{
    public string count;
    public int value;
    public Color32 color;

    public SquareGenerator(string newCount, int newValue, Color32 newColor)
    {
        count = newCount;
        value = newValue;
        color = newColor;
    }
}
