using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScript : MonoBehaviour
{
    [Header("Location Changes")]
    public Vector3 veticalTransformWater = new Vector3 (0, 0.25f, 0);
    public List<Material> skybox;
    public GameObject fog;
    public GameObject rain;
    public List<GameObject> fireCamp = new List<GameObject>();

    [Header("Rex Changes")]
    public int enemyDamage = 1;
    public float enemySpeed = 6f;
    public Vector3 enemyScale = new Vector3(50f,50f,50f);

    [Header("Script Settings")]
    private ClassLamp checkSuccess;
    private bool hasRun = false;
    private GameObject victoryUI;
    
    private void Awake()
    {
        checkSuccess = FindObjectOfType<ClassLamp>();
        victoryUI = GetComponentInChildren<Image>(true).gameObject;

        fog = GameObject.FindWithTag("Fog");
        rain = GameObject.FindWithTag("Rain");

        fireCamp.AddRange(GameObject.FindGameObjectsWithTag("FireCamp")); 
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutor1"))
        {
            checkSuccess.CheckTutorVictory();
        } 
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Swamp2"))
        {
            checkSuccess.CheckLevelOneVictory();
        }
    }

    public void TutorVictory(int successProgress)
    {
        if (!hasRun & successProgress == 3)
        {
            victoryUI.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; 
            hasRun = true;
        }
    }

    public void LevelOneVictory(int successProgress)
    {

    }
}
