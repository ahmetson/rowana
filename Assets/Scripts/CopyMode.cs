using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CopyMode : MonoBehaviour
{
  
    public List<AudioClip> savedAudio;
    public List<Material> savedMaterial;
    

    public PickUpController pickUpController;
    public Animator anim;
    

    private void Start()
    {
        pickUpController = FindObjectOfType<PickUpController>();
        
    }

    private void Update()
    {
        
    } 

    void Copy(GameObject hitObj)
    {
        var hitAudioSourse = hitObj.GetComponentInChildren<AudioSource>();
        var hitMeshRenderer = hitObj.GetComponentInChildren<MeshRenderer>();

        savedAudio.Add(hitAudioSourse.clip);
        savedMaterial.Add(hitMeshRenderer.material);
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
}
