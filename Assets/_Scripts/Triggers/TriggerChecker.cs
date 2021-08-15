using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChecker : MonoBehaviour {

    void OnDestroy()
    {
        if (gameObject.CompareTag("Coin"))
        {
            GameData.tc_coin = true;
        }
        
        if (gameObject.CompareTag("PowerUp_Health"))
        {
            GameData.tc_pHealth = true;
        }

        if (gameObject.CompareTag("PowerUp_Ammo"))
        {
            GameData.tc_pAmmo = true;
        }

    }
}
