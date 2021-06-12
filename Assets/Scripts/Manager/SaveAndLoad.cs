using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class SaveAndLoad
{

    public static void SaveColorByPosition(int Pos)
    {
        PlayerPrefs.SetString("_Clr_" + Pos, "Unlkd");
    }

    public static bool IsColorUnlockedAt(int Pos)
    {
        bool result = false;

        if(PlayerPrefs.GetString("_Clr_" + Pos).Equals("Unlkd"))
        {
            result = true;
        }

        return result;
    }
    public static void SetPlayerPrimaryColor(Color color)
    {
        string colorValue = color.r + "_" + color.g + "_" + color.b;
        PlayerPrefs.SetString("_Prmry_Clr", colorValue);

    }
    public static void SetPlayerSecondaryColor(Color color)
    {
        string colorValue = color.r + "_" + color.g + "_" + color.b;
        PlayerPrefs.SetString("_Secdry_Clr", colorValue);

    }
    public static Color GetPlayerPrimaryColor()
    {
        string[] str = PlayerPrefs.GetString("_Prmry_Clr", "1_1_1").Split('_');

        Color clr = new Color();
        if (str.Length > 0)
        {
            clr.r = int.Parse(str[0]);
            clr.g = int.Parse(str[1]);
            clr.b = int.Parse(str[2]);
        }
        clr.a = 1;
        return clr;

    }
    public static Color GetPlayerSecondaryColor()
    {
        string[] str = PlayerPrefs.GetString("_Secdry_Clr", "0_0_0").Split('_');

        Color clr = new Color();
        if (str.Length > 0)
        {
            clr.r = int.Parse(str[0]);
            clr.g = int.Parse(str[1]);
            clr.b = int.Parse(str[2]);
        }
        clr.a = 1;
        return clr;

    }
    public static void SaveCamaraViewBy(int val)
    {
        PlayerPrefs.SetInt("_Cam_Id", val);
    }
    public static int LoadCamaraViewBy()
    {
        return PlayerPrefs.GetInt("_Cam_Id", 0);
    }
}
