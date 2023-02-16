using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PickUpController : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] Transform holdArea;
    [SerializeField] Transform yellowPlatform;
    [SerializeField] Transform bluePlatform;
    [SerializeField] Transform greenPlatform;

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
                Debug.Log("gg");
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
    
    void MoveToPlatform()
    {
        if (Vector3.Distance(heldObj.transform.position, yellowPlatform.position) > 0.1f)
        {
            Vector3 moveDirection = (yellowPlatform.position - heldObj.transform.position);

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
            heldObjRB.useGravity = false;
            heldObjRB.drag = 10;
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjRB.transform.parent = yellowPlatform;
            heldObj = null;

            MoveToPlatform();
        }
    }
}