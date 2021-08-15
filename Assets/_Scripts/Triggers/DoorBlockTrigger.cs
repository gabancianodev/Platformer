using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorBlockTrigger : MonoBehaviour {

    public UnityEvent DoorEvent;
    public UnityEvent AlternateEvent;

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            if (GameData.playerCoin >= GameData.W_LEVEL_COIN_UNLOCK) 
            {
                DoorEvent.Invoke();
            }
            else
            {
                AlternateEvent.Invoke();
            }
        }
    }
}
