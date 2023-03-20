using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public MouseLook mouseLook;

    public Slider sliderVolume;
    public Slider sliderSensivity;

    public float sensivity = 20f;

    public GameObject canvas;
    public GameObject crossHair;
    

    private void Start()
    {
        if (!PlayerPrefs.HasKey("globalVolume"))
        {
            PlayerPrefs.SetFloat("globalVolume", 1);
        } else
        {
            Load();
        }        
        if (!PlayerPrefs.HasKey("mouseSensivity"))
        {
            PlayerPrefs.SetFloat("mouseSensivity", 1);
        } else
        {
            Load();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            canvas.SetActive(true);
            crossHair.SetActive(false);
            mouseLook.mouseSensitivity = 0f;
        }
        Debug.Log(mouseLook.mouseSensitivity);
    }
    private void Awake()
    {
        mouseLook = FindObjectOfType<MouseLook>();
    }
    public void PlayTutorial()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void BackToGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mouseLook.mouseSensitivity = sensivity;
        
    }
 
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetMouseSensivity()
    {
        sensivity = sliderSensivity.value;
        Save();
    }

    public void SetVolume()
    {
        AudioListener.volume = sliderVolume.value;
        Save();
    }

    private void Load()
    {
        sliderVolume.value = PlayerPrefs.GetFloat("globalVolume");
        sliderSensivity.value = PlayerPrefs.GetFloat("mouseSensivity");
    }   
    private void Save()
    {
        PlayerPrefs.SetFloat("globalVolume", sliderVolume.value);
        PlayerPrefs.SetFloat("mouseSensivity", sliderSensivity.value);
    }
}
