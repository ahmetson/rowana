using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpet : MonoBehaviour
{
    Save save;


    private void OnEnable()
    {
        GameObject game = GameObject.FindGameObjectWithTag("GameController");
        save = game.GetComponent<Save>();
    }

    public static Vector3 RespawnPosition()
    {
        GameObject carpet = GameObject.FindGameObjectWithTag("Respawn");
        if (carpet == null)
        {
            Debug.LogError("No Respawn carpet found in the level");
            return Vector3.zero;
        }

        return carpet.transform.position;
    }

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.tag != "Respawn")
            {
                save.SaveCarpet(gameObject.transform.position);
            }
        }
    }
}
