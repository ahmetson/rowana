using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.HID;


public class PickUpController : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] public Transform holdAreaRight;
    [SerializeField] public Transform holdAreaLeft;

    [SerializeField] public GameObject platformOne;

    [SerializeField] public GameObject platformTwo;
    [SerializeField] public GameObject platformTwoSub;

    [SerializeField] public GameObject platformThree;
    [SerializeField] public GameObject platformThreeSub1;
    [SerializeField] public GameObject platformThreeSub2;


    public GameObject heldObj;
    public GameObject heldObjSub;

    private Rigidbody heldObjRB;
    private Rigidbody heldObjRBSub;

    private Vector3 lastScale;

    private CopyMode copyScript;
    

    //private List<string> clipList;



    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask layerPlatform;

    private void Start()
    {
        copyScript = FindObjectOfType<CopyMode>();
        
        

    }
    private void Update()
    {

        //UnMuteCrystal();

        CheckLightFirst();
        CheckLightSecond();
        CheckLightThird();

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

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange, layerMask))
            {
                copyScript.CanCopy(hit.rigidbody.gameObject);
            }
        }

        if (heldObjSub != null | heldObj != null)
        {
            MoveObject();
        }
    }


    void UnMuteCrystal()
    {
        if (holdAreaLeft.childCount > 0)
        {
            holdAreaLeft.GetComponentInChildren<AudioSource>().volume = 1f;

        } 

        if (holdAreaRight.childCount > 0)
        {
            holdAreaRight.GetComponentInChildren<AudioSource>().volume = 1f;

        } 
    }
   
    
    void ChangeHands()
    {
        //�����, ����������, ����� (���������� ����)

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
            holdAreaLeft.GetComponentInChildren<AudioSource>().volume = 1f;

            if (Vector3.Distance(heldObjSub.transform.position, holdAreaLeft.position) > 0.1f)
            {
                Vector3 moveDirection = (holdAreaLeft.position - heldObjSub.transform.position);
                
                heldObjRBSub.AddForce(moveDirection * pickupForce);
            }
        }
        if (holdAreaRight.childCount > 0)
        {
            holdAreaRight.GetComponentInChildren<AudioSource>().volume = 1f;

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

        heldObjRBSub.AddForce(holdAreaLeft.up * 5, ForceMode.Impulse);

        heldObjSub.GetComponentInChildren<AudioSource>().volume = 0f;

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

                    heldObjSub.GetComponentInChildren<AudioSource>().volume = 1f;

                    heldObjSub.tag = "Unsaved";

                    heldObjSub = null;

                }
                else
                {
                    DropLeftObject(pickObj);
                }

            }
        }
    }

    public void CheckLightFirst()
    {
        if (platformOne.transform.childCount > 0)
        {
            platformOne.transform.parent.GetComponentInChildren<Light>().spotAngle = 30f;
            
            //platformOne.GetComponentInChildren<Light>().spotAngle = 60f;

        }
        if (platformOne.transform.childCount == 0)
        {
            platformOne.transform.parent.GetComponentInChildren<Light>().spotAngle = 0f;
            //platformOne.GetComponentInChildren<Light>().spotAngle = 0f;
        }
    }

    public void CheckLightSecond()
    {
        if (platformTwo.transform.childCount > 0)
        {
            platformTwo.transform.parent.GetComponentInChildren<Light>().spotAngle = 30f;
        }
        if (platformTwoSub.transform.childCount > 0)
        {
            platformTwo.transform.parent.GetComponentInChildren<Light>().spotAngle = 30f;
        }
        if (platformTwo.transform.childCount > 0 & platformTwoSub.transform.childCount > 0)
        {
            platformTwo.transform.parent.GetComponentInChildren<Light>().spotAngle = 60f;
        }
        if (platformTwo.transform.childCount == 0 & platformTwoSub.transform.childCount == 0)
        {
            platformTwo.transform.parent.GetComponentInChildren<Light>().spotAngle = 0f;
        }
    }

    public void CheckLightThird()
    {

        if (platformThree.transform.childCount > 0 | platformThreeSub1.transform.childCount > 0 | platformThreeSub2.transform.childCount > 0)
        {
            platformThree.transform.parent.GetComponentInChildren<Light>().spotAngle = 30f;
        }

        if (platformThree.transform.childCount == 0 & platformThreeSub1.transform.childCount == 0 & platformThreeSub2.transform.childCount == 0)
        {
            platformThree.transform.parent.GetComponentInChildren<Light>().spotAngle = 0f;
        }
        if (platformThree.transform.childCount > 0 & platformThreeSub1.transform.childCount > 0)
        {
            platformThree.transform.parent.GetComponentInChildren<Light>().spotAngle = 60f;
        }
        if (platformThreeSub1.transform.childCount > 0 & platformThreeSub2.transform.childCount > 0)
        {
            platformThree.transform.parent.GetComponentInChildren<Light>().spotAngle = 60f;
        }
        if (platformThreeSub2.transform.childCount > 0 & platformThree.transform.childCount > 0)
        {
            platformThree.transform.parent.GetComponentInChildren<Light>().spotAngle = 60f;
        }
        if (platformThree.transform.childCount > 0 & platformThreeSub1.transform.childCount > 0 & platformThreeSub2.transform.childCount > 0)
        {
            platformThree.transform.parent.GetComponentInChildren<Light>().spotAngle = 90f;
        }
    }

}