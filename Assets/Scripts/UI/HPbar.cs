using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    private List<RawImage> HP;
    public int HPstat = 5;

    private void Awake()
    {
        HP = new List<RawImage>();
        HP.AddRange(GetComponentsInChildren<RawImage>());
    }

    private void Update()
    { 

        foreach (RawImage image in HP) 
        { 
            image.enabled = false;
            HP[HPstat - 1].enabled = true;
        }
        
    }
}
