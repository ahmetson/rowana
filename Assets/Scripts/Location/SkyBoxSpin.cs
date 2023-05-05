using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxSpin : MonoBehaviour
{
    private float rotateSpeed = 1.2f;
    public Light globalLignt;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotateSpeed);
        globalLignt.transform.RotateAround(Vector3.zero, Vector3.down, 0.01f);
    }
}
