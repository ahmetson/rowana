using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    Save save;
    LevelManager levelManager;

    private void OnEnable()
    {
        GameObject game = GameObject.FindGameObjectWithTag("GameController");
        save = game.GetComponent<Save>();
        levelManager = game.GetComponent<LevelManager>();
    }

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // clean the carpet savings
            save.ClearCarpet();

            // move to next progress
            LevelManager.Progress currentProgress = levelManager.CurrentProgress();
            LevelManager.Progress nextProgress = levelManager.NextProgress(currentProgress);
            
            save.SaveProgress(nextProgress);

            levelManager.LoadScene(nextProgress);
        }
    }
}
