using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMode : MonoBehaviour
{
    public PickUpController pickUpController;
    public ParticleSystem particle;

    private void Start()
    {
        pickUpController = FindObjectOfType<PickUpController>();
        
    }

    private void Update()
    {
        //particle.transform.rotation = pickUpController.holdAreaLeft.rotation;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CanCopy();
        }
    } 

    void CanCopy()
    {
       if (pickUpController.holdAreaLeft.childCount > 0 & pickUpController.holdAreaRight.childCount > 0)  
       {
            if (pickUpController.heldObjSub.gameObject.name == "CrystalEmpty")
            {
                Debug.Log("canCopy");
                //Copy();
                particle.Play();
            }
        }

    }

    void Copy()
    {
        
    }
}
