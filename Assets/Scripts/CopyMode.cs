using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class CopyMode : MonoBehaviour
{
  
    public List<AudioClip> savedAudio;
    public List<Material> savedMaterial;
    public List<Color> savedLight;


    private PickUpController pickUpController;
    public Animator anim;
    private MouseLook cameraMove;

    public GameObject ui;
    public List<Button> buttons;
    

    private void Start()
    {
        pickUpController = FindObjectOfType<PickUpController>();
        cameraMove = FindObjectOfType<MouseLook>();

        ui.SetActive(false);
        foreach (Button but in buttons)
        {
            but.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        CheckLists();
        if (Input.GetKeyDown(KeyCode.B)) 
        { 
            if (!ui.active)
            {
                ui.SetActive(true);
                cameraMove.mouseSensitivity = 0f;
                Cursor.lockState = CursorLockMode.None;

            }
            else
            {
                ui.SetActive(false); 
                cameraMove.mouseSensitivity = 50f;
                Cursor.lockState = CursorLockMode.Locked;
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
            anim.SetTrigger("TriggerToSpin");
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
}
