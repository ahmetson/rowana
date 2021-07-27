using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    private static string CurrentProgressKey  = "currentProgress";
    private static string CurrentCarpetKey = "currentCarpet";

    public void SaveProgress(LevelManager.Progress progress)
    {
        string progressName = progress.ToString();

        PlayerPrefs.SetString(CurrentProgressKey, progressName);
    }

    public bool IsSaved()
    {
        return PlayerPrefs.HasKey(CurrentProgressKey);
    }

    public LevelManager.Progress LoadProgress()
    {
        if (!IsSaved())
        {
            Debug.Log("Has no key");
            return LevelManager.Progress.Intro;
        }

        string progressName = PlayerPrefs.GetString(CurrentProgressKey);
        Debug.Log("Saved file " + progressName);

        LevelManager.Progress progress = (LevelManager.Progress)System.Enum.Parse(typeof(LevelManager.Progress), progressName);

        return progress;
    }

    public void SaveCarpet(Vector3 carpetPosition)
    {
        PlayerPrefs.SetFloat(CurrentCarpetKey + "x", carpetPosition.x);
        PlayerPrefs.SetFloat(CurrentCarpetKey + "z", carpetPosition.z);
        PlayerPrefs.SetFloat(CurrentCarpetKey + "y", carpetPosition.y);
    }

    public Vector3 LoadCarpet()
    {
        if (!PlayerPrefs.HasKey(CurrentCarpetKey + "x"))
        {
            return Vector3.zero;
        }

        float x = PlayerPrefs.GetFloat(CurrentCarpetKey + "x");
        float z = PlayerPrefs.GetFloat(CurrentCarpetKey + "z");
        float y = PlayerPrefs.GetFloat(CurrentCarpetKey + "y");

        return new Vector3(x, y, z);
    }

}
