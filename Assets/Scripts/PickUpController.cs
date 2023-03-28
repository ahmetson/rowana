using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.InputSystem;


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

    public Animator anim;

    public GameObject heldObj;
    public GameObject heldObjSub;
    public GameObject radio;

    private Rigidbody heldObjRB;
    private Rigidbody heldObjRBSub;

    private Vector3 lastScale;

    private CopyMode copyScript;
    private DividerSources dividerSources;

    public InputMaster controls;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask layerPlatform;

    private void Start()
    {
        copyScript = FindObjectOfType<CopyMode>();
        dividerSources = FindObjectOfType<DividerSources>();

        

    }

    private void Awake()
    {
        controls = new InputMaster();
    }

    private void Update()
    {

        //UnMuteCrystal();
        //CheckLightFirst();
        //CheckLightSecond();
        //CheckLightThird();
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

    public void CheckLightFirst()
    {
        float angle = platformOne.transform.parent.GetComponentInChildren<Light>().spotAngle;
        float timer = 10f;

        if (platformOne.transform.childCount > 0)
        {
            

            angle = Mathf.Min(angle + Time.deltaTime * timer, 30f);
            platformOne.transform.parent.GetComponentInChildren<Light>().spotAngle = angle;

                //platformOne.transform.parent.GetComponentInChildren<Light>().spotAngle ++;
                //yield return new WaitForSeconds(1f);
            
            

        }
        if (platformOne.transform.childCount == 0)
        {
            

            angle = Mathf.Min(angle - Time.deltaTime * timer, 1f);
            platformOne.transform.parent.GetComponentInChildren<Light>().spotAngle = angle;

                //platformOne.transform.parent.GetComponentInChildren<Light>().spotAngle --;
                //yield return new WaitForSeconds(1f);
            
        }
    }

    public void CheckLightSecond()
    {

        float angle = platformTwo.transform.parent.GetComponentInChildren<Light>().spotAngle;
        float timer = 10f;
        

        if (platformTwo.transform.childCount > 0 & platformTwoSub.transform.childCount > 0)
        {
            
            angle = Mathf.Min(angle + Time.deltaTime * timer, 60f);
            platformTwo.transform.parent.GetComponentInChildren<Light>().spotAngle = angle;
        }

        else if (platformTwo.transform.childCount > 0 | platformTwoSub.transform.childCount > 0)
        {
            
            angle = Mathf.Min(angle + Time.deltaTime * timer, 30f);
            platformTwo.transform.parent.GetComponentInChildren<Light>().spotAngle = angle;
        }

        if (platformTwo.transform.childCount == 0 & platformTwoSub.transform.childCount == 0)
        {
            angle = Mathf.Min(angle - Time.deltaTime * timer, 1f);
            platformTwo.transform.parent.GetComponentInChildren<Light>().spotAngle = angle;
        }


    }

    public void CheckLightThird()
    {
        float angle = platformThree.transform.parent.GetComponentInChildren<Light>().spotAngle;
        float timer = 10f;

        if (platformThree.transform.childCount > 0 & platformThreeSub1.transform.childCount > 0 & platformThreeSub2.transform.childCount > 0)
        {
            angle = Mathf.Min(angle + Time.deltaTime * timer, 90f);
            platformThree.transform.parent.GetComponentInChildren<Light>().spotAngle = angle;
        }
        else if (platformThree.transform.childCount == 0 & platformThreeSub1.transform.childCount == 0 & platformThreeSub2.transform.childCount == 0)
        {
            angle = Mathf.Min(angle - Time.deltaTime * timer, 1f);
            platformThree.transform.parent.GetComponentInChildren<Light>().spotAngle = angle;
        }
        else if (platformThree.transform.childCount > 0 & platformThreeSub1.transform.childCount > 0)
        {
            angle = Mathf.Min(angle + Time.deltaTime * timer, 60f);
            platformThree.transform.parent.GetComponentInChildren<Light>().spotAngle = angle;
        }
        else if (platformThreeSub1.transform.childCount > 0 & platformThreeSub2.transform.childCount > 0)
        {
            angle = Mathf.Min(angle + Time.deltaTime * timer, 60f);
            platformThree.transform.parent.GetComponentInChildren<Light>().spotAngle = angle;
        }
        else if (platformThreeSub2.transform.childCount > 0 & platformThree.transform.childCount > 0)
        {
            angle = Mathf.Min(angle + Time.deltaTime * timer, 60f);
            platformThree.transform.parent.GetComponentInChildren<Light>().spotAngle = angle;
        }

        else if (platformThree.transform.childCount > 0 | platformThreeSub1.transform.childCount > 0 | platformThreeSub2.transform.childCount > 0)
        {
            angle = Mathf.Min(angle + Time.deltaTime * timer, 30f);
            platformThree.transform.parent.GetComponentInChildren<Light>().spotAngle = angle;
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
        radio.SetActive(false);
    }
    IEnumerator RadioActive()
    {
        yield return new WaitForSeconds(0.4f);
        radio.SetActive(true);
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