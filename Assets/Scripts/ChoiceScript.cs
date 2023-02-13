using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceScript : MonoBehaviour
{
    public Camera cam;
    public Renderer rend;
    public LayerMask layerMask;
    public List<Material> materials;


    void Start()
    {
      //  cam = GetComponent<Camera>();
      //  rend = GetComponent<Renderer>();
    }

    public void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 4f, layerMask))
        {
            print("Im looking at " + hit.transform.name);
            Debug.DrawLine(ray.origin, hit.point);
            
            if (Input.GetMouseButton(0)) 
            {
                if (hit.transform.name == "outsideYellow")
                    rend.material = materials[4];
                if (hit.transform.name == "outsideBlue")
                    rend.material = materials[0];
                if (hit.transform.name == "outsideGreen")
                    rend.material = materials[2];

            }
        }
        else
        {
            print("I'm looking at nothing!");
            
        }
    }
}
