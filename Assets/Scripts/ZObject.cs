using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZObject : MonoBehaviour
{
    [SerializeField]
    private float _offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y + _offset); // Set z to the y
    }
}
