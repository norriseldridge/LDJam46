using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private AudioSource _gunShot;

    [SerializeField]
    private float _fireRate;
    private float _timeSinceFire = 0;

    [SerializeField]
    private Projectile _bulletSource;

    public void Shoot(float angle)
    {
        if (_timeSinceFire >= _fireRate)
        {
            _timeSinceFire = 0;

            _gunShot.Play();

            // TODO create a bullet it and start it moving
            Projectile temp = Instantiate(_bulletSource);
            temp.tag = gameObject.tag;
            temp.transform.position = transform.position;
            temp.Angle = angle;
        }
    }

    private void Update()
    {
        _timeSinceFire += Time.deltaTime;
    }
}
