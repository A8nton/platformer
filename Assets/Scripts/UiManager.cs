using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {
    
    [SerializeField]
    private Text _coinsText, _livesText;

    public void UpdateCoins(int coins) {
        _coinsText.text = "Coins: " + coins;
    }

    public void UpdateLives(int lives) {
        _livesText.text = "Lives: " + lives;
    }
}