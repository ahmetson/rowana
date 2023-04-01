using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.InputSystem;


public class PickUpController : MonoBehaviour
{
    [Header("Pickup Settings")]
    [HideInInspector] public GameObject holderUnderMap;
    [HideInInspector] public GameObject heldObj;
    [HideInInspector] public GameObject heldObjSub;
    [HideInInspector] public Transform holdAreaLeft;
    
    [Header("Vars")]
    private Rigidbody heldObjRB;
    private Rigidbody heldObjRBSub;
    private Vector3 lastScale;

    [Header("Scripts And Modules")]
    private CopyMode copyScript;
    private DividerSources dividerSources;
    private InputMaster controls;
    private Animator anim;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;

    [Header("Layers")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask layerPlatform;

    [Header("Objects")]
    [SerializeField] public GameObject radio;
    [SerializeField] public Transform holdAreaRight;

    private void Awake()
    {
        controls = new InputMaster();

        copyScript = FindObjectOfType<CopyMode>();
        dividerSources = FindObjectOfType<DividerSources>();

        anim = GetComponentInChildren<Animator>();

        holderUnderMap = GameObject.FindWithTag("HolderUnderMap");
        holdAreaLeft = GameObject.FindWithTag("HoldAreaLeft").transform;
    }

    private void Update()
    {
        Swap();
        if (controls.Player.LeftMouse.triggered)
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

        if (controls.Player.Swap.triggered) //Bind Swap
        {
            ChangeHands();
        }

        if (controls.Player.RightMouse.triggered)
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

    void ChangeHands()
    {
        if (radio.activeSelf)
        {
            if (heldObjSub == null & heldObj != null)
            {
                heldObjSub = heldObj;

                heldObjSub.transform.parent = holdAreaLeft;
                heldObjSub.transform.position = holdAreaLeft.position;
                heldObjSub.transform.rotation = holdAreaLeft.rotation;
                heldObjSub.transform.localScale = lastScale;
                heldObj.GetComponentInChildren<Animator>().SetBool("Spin", true);

                anim.SetTrigger("isPickUp");

                heldObj = null;
            }
            else if (heldObjSub != null & heldObj == null)
            {
                lastScale = heldObjSub.transform.localScale;

                heldObj = heldObjSub;

                heldObj.transform.parent = holdAreaRight;
                heldObj.transform.position = holdAreaRight.position;
                heldObj.transform.rotation = holdAreaRight.rotation;
                heldObj.transform.localScale = new Vector3(2f, 2f, 2f);
                heldObj.GetComponentInChildren<Animator>().SetBool("Spin", false);

                anim.SetTrigger("isDroped");

                heldObjSub = null;
            }
        }
    }

    void MoveObject()
    {
        if (holdAreaLeft.childCount > 0)
        {
            //holdAreaLeft.GetComponentInChildren<AudioSource>().volume = 1f;

            if (Vector3.Distance(heldObjSub.transform.position, holdAreaLeft.position) > 0.1f)
            {
                Vector3 moveDirection = (holdAreaLeft.position - heldObjSub.transform.position);

                heldObjRBSub.AddForce(moveDirection * pickupForce);
            }
        }
        if (holdAreaRight.childCount > 0)
        {
            //holdAreaRight.GetComponentInChildren<AudioSource>().volume = 1f;

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

        pickObjSub.GetComponentInChildren<Animator>().SetBool("Spin", true);
        heldObjSub = pickObjSub;

        anim.SetTrigger("isPickUp");

        dividerSources.UnMute(heldObjSub.GetComponent<Collider>());
    }

    void DropLeftObject(GameObject pickObjSub)
    {
        heldObjRBSub = pickObjSub.GetComponent<Rigidbody>();

        heldObjRBSub.useGravity = true;
        heldObjRBSub.drag = 1;
        heldObjRBSub.constraints = RigidbodyConstraints.None;

        heldObjRBSub.AddForce(holdAreaLeft.up * -5, ForceMode.Impulse);

        //heldObjSub.GetComponentInChildren<AudioSource>().volume = 0f;
        //heldObjSub.GetComponentInChildren<Animator>().enabled = false;

        dividerSources.Mute(heldObjSub.GetComponent<Collider>());

        heldObjRBSub.transform.parent = null;

        heldObjSub.GetComponentInChildren<Animator>().SetBool("Spin", false);
        heldObjSub = null;

        anim.SetTrigger("isDroped");


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

                    //heldObjSub.GetComponentInChildren<AudioSource>().volume = 1f;

                    heldObjSub.tag = "Unsaved";

                    heldObjSub = null;

                    anim.SetTrigger("isDroped");

                    dividerSources.CheckPlatform(hit.collider);
                }
                else
                {
                    DropLeftObject(pickObj);
                }
            }
        }
    }

    public void Swap()
    {

        if (holdAreaLeft.childCount == 0)
        {
            if (controls.Inventory._1.triggered)
            {
                anim.SetInteger("numInventory", 1);
                StartCoroutine(RadioDisActive());
            }
            if (controls.Inventory._2.triggered)
            {
                anim.SetInteger("numInventory", 2);
                StartCoroutine(RadioActive());
            }

        }
    }
    IEnumerator RadioDisActive()
    {
        yield return new WaitForSeconds(0.3f);
        if (radio.GetComponentInChildren<Rigidbody>())
        {
            GameObject crystal = radio.GetComponentInChildren<Rigidbody>().gameObject;
            crystal.transform.parent = holderUnderMap.transform;
            crystal.transform.position = holderUnderMap.transform.position;

            radio.SetActive(false);
        }
        else
        {
            radio.SetActive(false);
        }
    }
    IEnumerator RadioActive()
    {
        yield return new WaitForSeconds(0.4f);
        if (holderUnderMap.GetComponentInChildren<Rigidbody>())
        {
            radio.SetActive(true);

            GameObject crystal = holderUnderMap.GetComponentInChildren<Rigidbody>().gameObject;
            crystal.transform.parent = holdAreaRight;
            crystal.transform.position = holdAreaRight.position;
            crystal.transform.rotation = holdAreaRight.rotation;
        }
        else
        {
            radio.SetActive(true);
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