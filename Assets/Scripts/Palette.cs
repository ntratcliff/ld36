using UnityEngine;
using System.Collections.Generic;

class Palette
{
    public static Color[] Colors =
    {
        new Color(193f/255f, 58f/255f, 58f/255f),
        new Color(155f/255f, 220f/255f, 139f/255f),
        new Color(167f/255f, 166f/255f, 193f/255f),
        new Color(85f/255f, 153f/255f, 192f/255f)
    };

    public static string[] Hex =
    {
        "c13a3a",
        "9bdc8b",
        "a7a6c1",
        "5599c0"
    };

    public static Color GetColor(PaletteColor color)
    {
        return Colors[(int)color];
    }

    public static bool GetPaletteColor(Color c, out PaletteColor color)
    {
        for (int i = 0; i < Colors.Length; i++)
        {
            if (Colors[i] == c)
            {
                color = (PaletteColor)i;
                return true;
            }
        }

        color = PaletteColor.InvalidColor;
        return false;
    }

    public static string GetHex(PaletteColor color)
    {
        return Hex[(int)color];
    }
}

public enum PaletteColor
{
    Red,
    Green,
    Purple,
    Blue,
    InvalidColor
}
