using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the starting point of the game during the testing.
/// 
/// It allows user to select the level to start
/// </summary>
public class TestProgress : MonoBehaviour
{
    /// <summary>
    /// If its true, then game will start from the custom progress. 
    /// </summary>
    public bool useProgressParam = true;

    /// <summary>
    /// From where the game starts
    /// </summary>
    public LevelManager.Progress progress;

    /// <summary>
    /// If its true, then the Scene is updated every time 
    /// </summary>
    public bool autoProgress = true;
    /// <summary>
    /// How fast the levels are changing. Its defined in seconds.
    /// </summary>
    public float progressSpeed = 5f;

    private LevelManager levelManager;
    private Save save;

    private static bool initialized = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        levelManager = gameObject.GetComponent<LevelManager>();
        save = gameObject.GetComponent<Save>();

        if (useProgressParam)
        {
            save.ClearCarpet();
            LevelManager.Progress currentProgress = levelManager.ValidProgress(progress);
            save.SaveProgress(currentProgress);
        }
        else
        {
            Debug.Log("Using the current scene where user is starting");
            if (!save.IsSaved())
            {
                LevelManager.Progress currentProgress = levelManager.CurrentProgress();
                save.SaveProgress(currentProgress);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Progress currentProgress = this.save.LoadProgress();

        Debug.Log("Current scene: " + currentProgress.ToString());

        if (autoProgress)
        {
            if (!initialized)
            {
                Debug.Log("couro");
                StartCoroutine("UpdateProgress");
                initialized = true;
            } else
            {
                Debug.Log("couroutine already started");
            }
        }
        else
        {
            levelManager.LoadScene(currentProgress);
        }
    }

    IEnumerator UpdateProgress()
    {
        while (true)
        {
            yield return new WaitForSeconds(progressSpeed);

            LevelManager.Progress currentProgress = save.LoadProgress();
            LevelManager.Progress nextProgress = levelManager.NextProgress(currentProgress);

            save.SaveProgress(nextProgress);

            Debug.Log("Next progress: " + nextProgress.ToString());

            levelManager.LoadScene(nextProgress);

            Debug.Log("Called... after interval");
        }

        yield return null;
    }

}
