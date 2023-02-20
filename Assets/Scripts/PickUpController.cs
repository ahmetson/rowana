using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PickUpController : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] Transform holdAreaRight;
    [SerializeField] Transform holdAreaLeft;
    [SerializeField] GameObject yellowPlatform;
    [SerializeField] GameObject bluePlatform;
    [SerializeField] GameObject greenPlatform;

    private GameObject heldObj;
    private GameObject heldObjSub;
    private Rigidbody heldObjRB;
    private Rigidbody heldObjRBSub;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask layerPlatform;

    private void Update()
    {
        //Right hand

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (heldObj == null)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange, layerMask))
                {
                    PickupObjectToRightHand(hit.transform.gameObject);
                }
            }
            else if (heldObj != null & (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange, layerPlatform)))
            {
                DropRightObjectAtPlatform(heldObj.transform.gameObject);
            } 
            else
            {
                DropRightObject();
            }
        }

        //Left hand

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (heldObjSub == null)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange, layerMask))
                {
                    PickupObjectToLeftHand(hit.transform.gameObject);
                }
            }
            else if (heldObjSub != null & (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange, layerPlatform)))
            {
                DropLeftObjectAtPlatform(heldObjSub.transform.gameObject);
            }
            else
            {
                DropLeftObject();
            }
        }


        if (heldObjSub != null | heldObj != null)
        {
            MoveObject();
        }
    }
   
    /*
    void ChangeHands()
    {
        
        if (heldObjSub == null & heldObj != null)
        {
            heldObjSub = heldObj;

            heldObj.transform.parent = holdAreaLeft;
            heldObj.transform.position = holdAreaLeft.position;

            heldObj = null;
        }
        else if (heldObjSub != null & heldObj == null)
        {
            heldObj = heldObjSub;

            heldObjSub.transform.parent = holdAreaRight;
            heldObjSub.transform.position = holdAreaRight.position;
            
            heldObjSub = null;
        }
    }
    */
    

    void MoveObject()
    {
        if (holdAreaLeft.childCount > 0)
        {
            if (Vector3.Distance(heldObjSub.transform.position, holdAreaLeft.position) > 0.1f)
            {
                Vector3 moveDirection = (holdAreaLeft.position - heldObjSub.transform.position);

                heldObjRBSub.AddForce(moveDirection * pickupForce);
            }
        }
        if (holdAreaRight.childCount > 0)
        {
            if (Vector3.Distance(heldObj.transform.position, holdAreaRight.position) > 0.1f)
            {
                Vector3 moveDirection = (holdAreaRight.position - heldObj.transform.position);

                heldObjRB.AddForce(moveDirection * pickupForce);
            }
        }

    }


    void PickupObjectToRightHand(GameObject pickObj)
    {
        heldObjRB = pickObj.GetComponent<Rigidbody>();
        heldObjRB.useGravity = false;
        heldObjRB.drag = 10;
        heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

        heldObjRB.transform.rotation = holdAreaRight.rotation;

        heldObjRB.transform.parent = holdAreaRight;

        heldObj = pickObj;

    }

    void PickupObjectToLeftHand(GameObject pickObjSub)
    {
        heldObjRBSub = pickObjSub.GetComponent<Rigidbody>();
        heldObjRBSub.useGravity = false;
        heldObjRBSub.drag = 10;
        heldObjRBSub.constraints = RigidbodyConstraints.FreezeRotation;

        heldObjRBSub.transform.rotation = holdAreaLeft.rotation;

        heldObjRBSub.transform.parent = holdAreaLeft;

        heldObjSub = pickObjSub;

    }

    void DropRightObject()
    {
        

        heldObjRB.useGravity = true;
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;

        heldObjRB.transform.parent = null;
        heldObj = null;

    }
    void DropLeftObject()
    {

        heldObjRBSub.useGravity = true;
        heldObjRBSub.drag = 1;
        heldObjRBSub.constraints = RigidbodyConstraints.None;

        heldObjRBSub.transform.parent = null;
        heldObjSub = null;

    }

    void DropRightObjectAtPlatform(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange, layerPlatform)) 
            {
                

                if (hit.collider & hit.transform.childCount == 0)
                {
                    
                    heldObjRB.transform.parent = hit.transform;
                    pickObj.transform.position = hit.transform.position;
                    pickObj.transform.rotation = hit.transform.rotation;

                    heldObjRB.constraints = RigidbodyConstraints.FreezePosition;

                    heldObjRB = pickObj.GetComponent<Rigidbody>();
                    heldObjRB.useGravity = true;
                    heldObjRB.drag = 1;


                    heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;
                    
                    heldObj = null;
                }
                else
                {
                    DropRightObject();
                }

            }
        }
    }

    void DropLeftObjectAtPlatform(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange, layerPlatform))
            {


                if (hit.collider & hit.transform.childCount == 0)
                {

                    heldObjRBSub.transform.parent = hit.transform;
                    pickObj.transform.position = hit.transform.position;
                    pickObj.transform.rotation = hit.transform.rotation;

                    heldObjRBSub.constraints = RigidbodyConstraints.FreezePosition;

                    heldObjRBSub = pickObj.GetComponent<Rigidbody>();
                    heldObjRBSub.useGravity = true;
                    heldObjRBSub.drag = 1;


                    heldObjRBSub.constraints = RigidbodyConstraints.FreezeRotation;

                    heldObjSub = null;
                }
                else
                {
                    DropLeftObject();
                }

            }
        }
    }
}