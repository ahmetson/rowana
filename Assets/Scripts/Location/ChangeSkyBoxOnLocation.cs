using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeSkyBoxOnLocation : MonoBehaviour
{
    public Material dark;
    public Material forest;
    public Material sun;
    public Light globalLignt;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag ("Player"))
        {    
            
            if (GetComponent<Collider>().CompareTag("Flat"))
            {
                RenderSettings.skybox = sun;
                globalLignt.intensity = 3f;
            }
            if (GetComponent<Collider>().CompareTag("Swamp"))
            {
                RenderSettings.skybox = dark;
                globalLignt.intensity = 3f;
            }
            if (GetComponent<Collider>().CompareTag("Forest"))
            {
                RenderSettings.skybox = forest;
                globalLignt.intensity = 1f;
            }       
        }
        
    }
}
