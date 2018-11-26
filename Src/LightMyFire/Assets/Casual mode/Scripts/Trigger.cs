using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent UE;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            col.gameObject.GetComponent<Player>().RegisterCollider(gameObject.transform.position, UE);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            col.gameObject.GetComponent<Player>().DeleteCollider(UE);
    }
}
