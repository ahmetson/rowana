using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMainCharacter : MonoBehaviour
{
    public bool startPoint = false;

    Save save;

    void Awake()
    {
        GameObject game = GameObject.FindGameObjectWithTag("GameController");
        save = game.GetComponent<Save>();

        Vector3 carpetPosition;

        if (startPoint)
        {
            carpetPosition = Carpet.RespawnPosition();
        }
        else
        {
            carpetPosition = save.LoadCarpet();
            if (carpetPosition.Equals(Vector3.zero))
            {
                carpetPosition = Carpet.RespawnPosition();
            }
        }

        SetToCarpet(carpetPosition);
    }

    public void SetToCarpet(Vector3 carpetPosition)
    {
        Debug.Log("Set the starpoints");
        Vector3 position = new Vector3(carpetPosition.x, gameObject.transform.parent.position.y, carpetPosition.z);

        vThirdPersonController motion = gameObject.GetComponent<vThirdPersonController>();
        motion.useRootMotion = true;

        gameObject.transform.parent.position = position;
        motion.useRootMotion = true;
    }

    private void OnDisable()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        gameObject.transform.parent.gameObject.SetActive(true);
    }
}
