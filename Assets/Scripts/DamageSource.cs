using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField]
    private int _damage;
    public int Damage { get { return _damage; } }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.BroadcastMessage("OnTakeDamage", this, SendMessageOptions.DontRequireReceiver);
    }
}
