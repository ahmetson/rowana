using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PickUpController : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] Transform holdArea;
    [SerializeField] GameObject yellowPlatform;
    [SerializeField] GameObject bluePlatform;
    [SerializeField] GameObject greenPlatform;

    private GameObject heldObj;
    private Rigidbody heldObjRB;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask layerPlatform;

    private void Update()
    {

        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit hit;

            if (heldObj == null)
            {
                
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange, layerMask))
                {
                    PickupObject(hit.transform.gameObject);
                }
            }
            else if (heldObj != null & (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange, layerPlatform)))
            {
                
                DropObjectAtPlatform(heldObj.transform.gameObject);
            }
            else 
            {
                DropObject();
            }
        }
        if (heldObj != null)
        {  
            MoveObject();
        }
    }


    void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);

            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            heldObjRB.useGravity = false;
            heldObjRB.drag = 10;
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjRB.transform.rotation = holdArea.rotation;

            heldObjRB.transform.parent = holdArea;
            heldObj = pickObj;

        }
    }
    void DropObject()
    {

        heldObjRB.useGravity = true;
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;

        heldObjRB.transform.parent = null;
        heldObj = null;
    }

    void DropObjectAtPlatform(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            heldObjRB.useGravity = true;
            heldObjRB.drag = 1;


            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange, layerPlatform)) 
            {
                

                if (hit.collider & hit.transform.childCount == 0)
                {
                    
                    heldObjRB.transform.parent = hit.transform;
                    pickObj.transform.position = hit.transform.position;
                    pickObj.transform.rotation = hit.transform.rotation;
                    heldObj = null;
                }
                else
                {
                    DropObject();
                }
            }
        }
    }
}