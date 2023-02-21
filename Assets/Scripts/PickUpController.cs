using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PickUpController : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] public Transform holdAreaRight;
    [SerializeField] public Transform holdAreaLeft;
    [SerializeField] GameObject yellowPlatform;
    [SerializeField] GameObject bluePlatform;
    [SerializeField] GameObject greenPlatform;

    public GameObject heldObj;
    public GameObject heldObjSub;
    private Rigidbody heldObjRB;
    private Rigidbody heldObjRBSub;
    private Vector3 lastScale;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask layerPlatform;

    private void Update()
    {
        Debug.Log(heldObjSub);

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
            else if (heldObjSub != null)
            {
                DropLeftObject(heldObjSub.transform.gameObject);
            }

        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ChangeHands();
        }
        

        if (heldObjSub != null | heldObj != null)
        {
            MoveObject();
        }
    }
   
    
    void ChangeHands()
    {

        if (heldObjSub == null & heldObj != null)
        {
            heldObjSub = heldObj;

            heldObjSub.transform.parent = holdAreaLeft;
            heldObjSub.transform.position = holdAreaLeft.position;
            heldObjSub.transform.localScale = lastScale;

            heldObj = null;
        }
        else if (heldObjSub != null & heldObj == null)
        {
            lastScale = heldObjSub.transform.localScale;

            heldObj = heldObjSub;
            
            heldObj.transform.parent = holdAreaRight;
            heldObj.transform.position = holdAreaRight.position;
            heldObj.transform.localScale = new Vector3(150f, 150f, 150f);

            heldObjSub = null;
        }
    }
    
    

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

    void DropLeftObject(GameObject pickObjSub)
    {
        heldObjRBSub = pickObjSub.GetComponent<Rigidbody>();

        heldObjRBSub.useGravity = true;
        heldObjRBSub.drag = 1;
        heldObjRBSub.constraints = RigidbodyConstraints.None;

        heldObjRBSub.AddForce(holdAreaLeft.up * 100, ForceMode.Impulse);

        heldObjRBSub.transform.parent = null;
        heldObjSub = null;

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
                    heldObjRBSub = pickObj.GetComponent<Rigidbody>();

                    heldObjRBSub.transform.parent = hit.transform;
                    pickObj.transform.position = hit.transform.position;
                    pickObj.transform.rotation = hit.transform.rotation;

                    heldObjRBSub.constraints = RigidbodyConstraints.FreezePosition;

                    heldObjRBSub.useGravity = true;
                    heldObjRBSub.drag = 1;


                    heldObjRBSub.constraints = RigidbodyConstraints.FreezeRotation;

                    heldObjSub = null;
                }
                else
                {
                    DropLeftObject(pickObj);
                }

            }
        }
    }
}