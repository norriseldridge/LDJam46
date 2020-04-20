using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private AudioSource _stepSound;

    [SerializeField]
    private AudioSource _takeDamageSound;

    [SerializeField]
    private EntityWithHealth _entity;

    public float CrossSightDistance { get; set; }
    private float _aimRotation;
    [SerializeField]
    private Transform _crossSight;

    [SerializeField]
    private Shooter _shooter;
    private bool _fireButtonWasPressed = false;

    private void Start()
    {
        _entity.AddHealthChangeListener(OnHealthChange);
        _entity.AddDeathListener(OnDead);
    }

    private void OnDead()
    {
        Destroy(gameObject);
    }

    public void SetCrossSightActive(bool active)
    {
        _crossSight.gameObject.SetActive(active);
    }

    // Update is called once per frame
    void Update()
    {
        // Handles keyboard and controller input
        Vector2 velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (velocity.magnitude > 0)
        {
            _animator.Play("FarmerRun");
            if (!_stepSound.isPlaying)
            {
                _stepSound.pitch = Random.Range(0.9f, 1.1f);
                _stepSound.Play();
            }
            
            transform.Translate(velocity * _speed * Time.deltaTime);
        }
        else
        {
            _animator.Play("FarmerIdle");
        }

        // are we using a controller or not?
        Vector3 angleVector = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
        if (!InputSetting.UseController)
        {
            // calculate the angle from the mouse
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angleVector = mousePos - transform.position;
        }
        
        if (angleVector.magnitude > 0.0f)
        {
            _aimRotation = Mathf.Deg2Rad * (Quaternion.LookRotation(Vector3.forward, angleVector).eulerAngles.z + 90);
            _spriteRenderer.flipX = (_aimRotation > Mathf.PI / 2 && _aimRotation < 3 * Mathf.PI / 2);
            _crossSight.localPosition = (new Vector3(Mathf.Cos(_aimRotation), Mathf.Sin(_aimRotation)) * CrossSightDistance);
        }
        
        // shoot
        if (Input.GetButtonDown("Fire1") || (Input.GetAxis("Fire1") > 0.5f && !_fireButtonWasPressed))
        {
            _fireButtonWasPressed = true;
            _shooter.Shoot(_aimRotation);
        }

        // Trigger had to be pulled down and then released per shot
        if (Input.GetAxis("Fire1") <= 0.05f)
        {
            _fireButtonWasPressed = false;
        }
    }

    private void OnHealthChange(DamageSource damageSource, int newHealth)
    {
        // Play hit sound
        if (!_takeDamageSound.isPlaying)
            _takeDamageSound.Play();
    }
}
