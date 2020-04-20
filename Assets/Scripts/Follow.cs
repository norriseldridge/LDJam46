using UnityEngine;

public class Follow : MonoBehaviour
{

    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _speed;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, GetTargetPosition(), _speed * Time.deltaTime);
    }

    private Vector3 GetTargetPosition()
    {
        if (_target != null)
        {
            Vector3 temp = _target.position;
            temp.z = transform.position.z;
            return temp; 
        }

        return transform.position;
    }
}
