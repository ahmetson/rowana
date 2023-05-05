using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;



public class ChoiceScript : MonoBehaviour
{
    public Camera cam;
    public Renderer handCrystal;
    
    public LayerMask layerMask;
    public List<Material> materials;
    private Material mat;

    public void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;
        

        if (Physics.Raycast(ray, out hit, 4f, layerMask))
        {
            print("Im looking at " + hit.transform.name);
            Debug.DrawLine(ray.origin, hit.point);

            if (Input.GetMouseButtonDown(0)) {


                /*
                if (hit.collider.GetComponentInParent<Renderer>().material == materials[4]) //yellow
                {
                    hit.collider.GetComponentInParent<Renderer>().material = mat;
                    mat = materials[4];
                    handCrystal.material = materials[4];
                } 
                else if (hit.collider.GetComponentInParent<Renderer>().material == materials[0]) //blue
                {
                    hit.collider.GetComponentInParent<Renderer>().material = mat;
                    mat = materials[0];
                    handCrystal.material = materials[0];
                }
                else if (hit.collider.GetComponentInParent<Renderer>().material == materials[2]) //green
                {
                    hit.collider.GetComponentInParent<Renderer>().material = mat;
                    mat = materials[2];
                    handCrystal.material = materials[2];
                }
                else if (hit.collider.GetComponentInParent<Renderer>().material == materials[1]) //empty
                {
                    hit.collider.GetComponentInParent<Renderer>().material = mat;
                    mat = materials[1];
                    handCrystal.material = materials[1];
                }
               */ 

                mat = handCrystal.material;
                handCrystal.material = hit.collider.GetComponentInParent<Renderer>().material;
                hit.collider.GetComponentInParent<Renderer>().material = mat;

                

                /*   hit.collider.GetComponentInParent<Renderer>().material = mat;

                   if (hit.transform.name == "insideYellow")
                   {

                       mat = hit.collider.GetComponentInParent<Renderer>().material;
                       hit.collider.GetComponentInParent<Renderer>().material = handCrystal.material;
                       handCrystal.material = mat;

                       hit.transform.name = "insideEmpty";
                   }
                   else if (hit.transform.name == "insideBlue")
                   {

                       mat = hit.collider.GetComponentInParent<Renderer>().material;
                       hit.collider.GetComponentInParent<Renderer>().material = handCrystal.material;
                       handCrystal.material = mat;

                       hit.transform.name = "insideEmpty";
                   }
                   else if (hit.transform.name == "insideGreen")
                   {

                       mat = hit.collider.GetComponentInParent<Renderer>().material;

                       handCrystal.material = mat;

                       hit.transform.name = "insideEmpty";
                   }
                   else if (hit.transform.name == "insideEmpty")
                   {
                       hit.collider.GetComponentInParent<Renderer>().material = mat;

                       //Debug.Log(handCrystal.material);


                       if (mat == materials[4]) 
                       {
                           hit.transform.name = "insideYellow";              
                       }
                       else if (mat == materials[0])
                       {
                           hit.transform.name = "insideBlue";
                           Debug.Log("done blue");
                       }
                       else if (mat == materials[2])
                       {
                           hit.transform.name = "insideGreen";     
                       }

                       mat = materials[1];
                       handCrystal.material = materials[1];


                   } */
            }
        }
        else
        {
            print("I'm looking at nothing!");
            
        }
    }
}
