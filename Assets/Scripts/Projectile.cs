using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    public float Angle { get; set; }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.AngleAxis((Angle * Mathf.Rad2Deg) - 90, Vector3.forward);
        transform.position += (new Vector3(Mathf.Cos(Angle), Mathf.Sin(Angle)) * _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, Vector3.zero) > 5)
        {
            Destroy(gameObject);
        }
    }
}
