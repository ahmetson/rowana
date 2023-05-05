using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualLight : MonoBehaviour
{
    [Header("Platforms")]

    public GameObject platformOne;

    public GameObject platformTwo;
    public GameObject platformTwoSub;

    public GameObject platformThree;
    public GameObject platformThreeSub1;
    public GameObject platformThreeSub2;

    private void Update()
    {
        CheckLightFirst();
        CheckLightSecond();
        CheckLightThird();
    }
    private void CheckLightFirst()
    {
        float angle = platformOne.transform.parent.GetComponentInChildren<Light>().spotAngle;
        float timer = 10f;

        if (platformOne.transform.childCount > 0)
        {
            angle = Mathf.Min(angle + Time.deltaTime * timer, 30f);
            platformOne.transform.parent.GetComponentInChildren<Light>().spotAngle = angle;
        }
        if (platformOne.transform.childCount == 0)
        {
            angle = Mathf.Min(angle - Time.deltaTime * timer, 1f);
            platformOne.transform.parent.GetComponentInChildren<Light>().spotAngle = angle;
        }
    }

    private void CheckLightSecond()
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

    private void CheckLightThird()
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
}
