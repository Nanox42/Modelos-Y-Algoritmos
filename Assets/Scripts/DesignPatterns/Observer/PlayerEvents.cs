using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerEvents : MonoBehaviour
{
    private int coins = 0;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Coin")) //other.transform.GetChild(0).name == "Coin"
        {
            coins++;
            Destroy(other.gameObject);
            CoinManager.instance.CoinCollected(coins);
        }
    }

}
