using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : UIManager {
    
    public Slider ui_healthBar;
    public Slider ui_ammoBar;
    public Text ui_coinCount;
	
    void Awake()
    {
        GameData.W_LEVEL_COIN_UNLOCK = 50;
    }

	void Update () {
        UpdateHealthBarUI(GameData.playerHealth);
        UpdateAmmoBarUI(GameData.playerAmmo);
        UpdateCoinCounter(GameData.playerCoin);
	}

    public void UpdateHealthBarUI(int playerHealth)
    {
        ui_healthBar.value = playerHealth;
    }

    public void UpdateAmmoBarUI(int playerAmmo)
    {
        ui_ammoBar.value = playerAmmo;
    }

    public void UpdateCoinCounter(int playerCoins)
    {
        ui_coinCount.text = playerCoins.ToString();
    }
}
