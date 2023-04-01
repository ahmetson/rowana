using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
    public MouseLook mouseLook;
    public CopyMode copyMode;

    public Slider sliderVolume;
    public Slider sliderSensivity;

    [HideInInspector] public float sensivity;

    public GameObject canvas;
    public GameObject crossHair;

    private InputMaster controls;


    private void Awake()
    {
        controls = new InputMaster();

        mouseLook = FindObjectOfType<MouseLook>();
        copyMode = FindObjectOfType<CopyMode>();
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("globalVolume"))
        {
            PlayerPrefs.SetFloat("globalVolume", 20);
        }
        else if (!PlayerPrefs.HasKey("mouseSensivity"))
        {
            PlayerPrefs.SetFloat("mouseSensivity", 20);
        }
        else
        {
            Load();
        }
    }
    private void Update()
    {
        if (controls.Menu.Escape.triggered)
        {
            PauseMenu();
        }
    }

    public void PauseMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        canvas.SetActive(true);
        crossHair.SetActive(false);
        mouseLook.mouseSensitivity = 0f;
        copyMode.enabled = false;
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void BackToGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mouseLook.mouseSensitivity = sensivity;
        copyMode.enabled = true;
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
        PlayerPrefs.SetFloat("mouseSensivity", sliderSensivity.value);
    }

    public void SetVolume()
    {
        AudioListener.volume = sliderVolume.value;
        PlayerPrefs.SetFloat("globalVolume", sliderVolume.value);
    }

    private void Load()
    {
        sliderVolume.value = PlayerPrefs.GetFloat("globalVolume");
        sliderSensivity.value = PlayerPrefs.GetFloat("mouseSensivity");
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
