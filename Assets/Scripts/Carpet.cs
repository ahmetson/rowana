using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpet : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Save the location");
        }
    }
}
