using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerInTrigger : MonoBehaviour
{
    public GameObject TextGameobject;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !TextGameobject.activeSelf) //Makes Text Gameobject "Enable" if player enters the trigger.
        {
            TextGameobject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && TextGameobject.activeSelf) //Makes Text Gameobject "Disable" If player exits the trigger.
        {
            TextGameobject.SetActive(false);
        }
    }
}
