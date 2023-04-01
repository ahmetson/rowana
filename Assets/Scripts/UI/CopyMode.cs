using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CopyMode : MonoBehaviour
{
  
    public List<AudioClip> savedAudio;
    public List<Material> savedMaterial;
    public List<Color> savedLight;

    public MenuScript menuScript;
    private PickUpController pickUpController;
    //public Animator anim;
    private MouseLook cameraMove;

    public GameObject ui;
    public List<Button> buttons;

    private InputMaster controls;

    private void Start()
    {
        pickUpController = FindObjectOfType<PickUpController>();
        cameraMove = FindObjectOfType<MouseLook>();
        menuScript = FindObjectOfType<MenuScript>();

        ui.SetActive(false);

        foreach (Button but in buttons)
        {
            but.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        controls = new InputMaster();
    }

    private void Update()
    {
        CheckLists();
        CrystalSettings();
    } 

    public void CrystalSettings()
    {
        if (pickUpController.radio.activeSelf)
        {
            if (controls.Inventory.SettingsCrystal.triggered)
            {
                if (!ui.activeInHierarchy)
                {
                    ui.SetActive(true);
                    cameraMove.mouseSensitivity = 0f;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    menuScript.enabled = false;
                }
                else
                {
                    ui.SetActive(false);
                    menuScript.BackToGame();
                    menuScript.enabled = true;
                    cameraMove.mouseSensitivity = menuScript.sensivity;
                }
            }
        }
    }


    void Copy(GameObject hitObj)
    {
        var hitAudioSourse = hitObj.GetComponentInChildren<AudioSource>();
        var hitMeshRenderer = hitObj.GetComponentInChildren<MeshRenderer>();
        var hitLight = hitObj.GetComponentInChildren<Light>();

        savedAudio.Add(hitAudioSourse.clip);
        savedMaterial.Add(hitMeshRenderer.material);
        savedLight.Add(hitLight.color);
        
    }

    public void CanCopy(GameObject hitObj)
    {
        if (hitObj.tag == "Unsaved")
        {
            //anim.SetTrigger("TriggerToSpin");
            hitObj.tag = "Saved";
            
            Copy(hitObj);
        }
        else
        {
            Debug.Log("Saved!!!");
        }
    }

    void CheckLists()
    {
        int i = -1;
        //var listAudioLength = savedAudio.Count;
        //var listMeshLength = savedMaterial.Count;
        foreach (var mat in savedMaterial) 
        {
            i++;
            buttons[i].gameObject.SetActive(true);
            buttons[i].GetComponent<Image>().color= mat.color;
            
        }
        
    }
    public void ButtonFunc(int i)
    {
        if (pickUpController.holdAreaRight.childCount != 0)
        {
            pickUpController.heldObj.GetComponentInChildren<AudioSource>().clip = savedAudio[i];
            pickUpController.heldObj.GetComponentInChildren<MeshRenderer>().material = savedMaterial[i];
            pickUpController.heldObj.GetComponentInChildren<Light>().color = savedLight[i];
        }
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
