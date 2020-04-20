using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField]
    private int _health;
    [SerializeField]
    private AudioSource _healSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the player touches this
        if (collision.GetComponent<Player>())
        {
            _healSound.Play();
            collision.BroadcastMessage("OnRestoreHealth", _health, SendMessageOptions.DontRequireReceiver);
            // Play sound
            Destroy(gameObject);
        }
    }
}
