using UnityEngine;

public class FlowerStolenZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Flower>() || collision.GetComponentInChildren<Flower>())
        {
            // Flower stolen! Destory it!
            Destroy(collision.gameObject);
        }
    }
}
