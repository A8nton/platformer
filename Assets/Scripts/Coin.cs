using UnityEngine;

public class Coin : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            FindObjectOfType<Player>().UpdateCoins();
            Destroy(gameObject);
        }
    }
}