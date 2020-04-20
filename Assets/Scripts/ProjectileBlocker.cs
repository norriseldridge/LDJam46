using UnityEngine;

public class ProjectileBlocker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Projectile>())
        {
            Destroy(collision.gameObject);
        }
    }
}
