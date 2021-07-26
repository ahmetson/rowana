using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public enum Progress
    {
        Intro,
        Kingdom,
        FoggyForestRoad,
        FoggyForestCaves,
        RiverDeltas,
        FarmFeilds,
        Swamp,
        Rock,
        BigMountain,
        Home,
        Ending
    }

    public struct ProgressData
    {
        public string scene;
        public Progress nextProgress;

        public ProgressData(string scene, Progress nextProgress)
        {
            if (scene == null) throw new System.ArgumentException();

            this.scene = scene;
            this.nextProgress = nextProgress;
        }
    }

    public Dictionary<Progress, ProgressData> scenes = new Dictionary<Progress, ProgressData>();

    void Awake()
    {
        scenes.Add(Progress.Intro, new ProgressData("Intro", Progress.Kingdom));
        scenes.Add(Progress.Kingdom, new ProgressData("Kingdom", Progress.FoggyForestRoad));
        scenes.Add(Progress.FoggyForestRoad, new ProgressData("ForestRoad", Progress.FoggyForestCaves));
        scenes.Add(Progress.FoggyForestCaves, new ProgressData("ForestCaves", Progress.RiverDeltas));
        scenes.Add(Progress.RiverDeltas, new ProgressData("RiverDeltas", Progress.FarmFeilds));
        scenes.Add(Progress.FarmFeilds, new ProgressData("FarmFields", Progress.Swamp));
        scenes.Add(Progress.Swamp, new ProgressData("Swamp", Progress.Rock));
        scenes.Add(Progress.Rock, new ProgressData("Rock", Progress.BigMountain));
        scenes.Add(Progress.BigMountain, new ProgressData("BigMountain", Progress.Home));
        scenes.Add(Progress.Home, new ProgressData("Kingdom", Progress.Ending));
        scenes.Add(Progress.Ending, new ProgressData("Ending", Progress.Intro));
    }

    public Progress ValidProgress(Progress progress)
    {
        if (progress == Progress.Intro || progress == Progress.Ending)
        {
            return scenes[progress].nextProgress;
        }

        return progress;
    }

    public Progress CurrentProgress()
    {
        Scene scene = SceneManager.GetActiveScene();

        Progress progress =  scenes.FirstOrDefault(x => x.Value.scene == scene.name).Key;

        return ValidProgress(progress);
    }

    public Progress NextProgress(Progress currentProgress)
    {
        return scenes[currentProgress].nextProgress;
    }

    public void LoadScene(Progress currentProgress)
    {
        string scene = scenes[currentProgress].scene;

        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
