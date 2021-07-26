using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    private static string CurrentProgressKey  = "currentProgress";

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
}
