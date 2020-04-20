using UnityEngine;

public class Bandit : MonoBehaviour
{
    public static int KilledCount = 0;

    private Flower _flower;
    private Player _player;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _carrySpeed;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Shooter _shooter;
    
    [SerializeField]
    private float _quietPeriod;
    private float _currentQuietPeriod = 0;

    [SerializeField]
    private float _loudPeriod;
    private float _currentLoadPeriod = 0;
    private bool _quiet = true;

    private float _atFlowerTime = 0.25f;
    private float _currentAtFlowerTimer = 0;
    private bool _carryFlowerAway = false;

    [SerializeField]
    private HealthPickup _healthPickupSource;
    private float _chanceToDropHealth = 0.2f;
    private float _chanceToDropWhenCarry = 0.40f;

    private void Start()
    {
        GetComponent<EntityWithHealth>().AddDeathListener(OnDead);
    }

    private void DropPickUp(float percChance)
    {
        if (Random.Range(0.0f, 1.0f) <= percChance)
        {
            HealthPickup pickup = Instantiate(_healthPickupSource);
            pickup.transform.position = transform.position;
        }
    }

    private void OnDead()
    {
        if (_flower && _flower.transform.parent == transform)
        {
            _flower.transform.parent = null;
            DropPickUp(_chanceToDropWhenCarry);
        }
        else
        {
            DropPickUp(_chanceToDropHealth);
        }

        ++KilledCount;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (_flower == null)
        {
            _flower = FindObjectOfType<Flower>();
        }
        else
        {
            if (!_carryFlowerAway)
            {
                // try to get to the flower
                if (Vector3.Distance(transform.position, _flower.transform.position) > 0.1f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, _flower.transform.position, _speed * Time.deltaTime);
                    _animator.Play("BanditRun");
                }
                else
                {
                    _currentAtFlowerTimer += Time.deltaTime;
                    _animator.Play("BanditIdle");

                    if (_currentAtFlowerTimer > _atFlowerTime)
                    {
                        if (_flower && _flower.transform.parent == null)
                        {
                            _currentAtFlowerTimer = 0;
                            _carryFlowerAway = true;
                            _flower.transform.parent = transform;
                        }
                    }
                }
            }
        }
        
        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
        }
        else
        {
            if (_quiet)
            {
                _currentQuietPeriod += Time.deltaTime;
                if (_currentQuietPeriod >= _quietPeriod)
                {
                    _quiet = false;
                    _currentQuietPeriod = 0.0f;
                }
            } 
            else
            {
                _currentLoadPeriod += Time.deltaTime;
                if (_currentLoadPeriod >= _loudPeriod)
                {
                    _quiet = true;
                    _currentLoadPeriod = 0.0f;
                }

                // Shoot at the player
                float angle = Mathf.Deg2Rad * (Quaternion.LookRotation(Vector3.forward, _player.transform.position - transform.position).eulerAngles.z + 90);
                _spriteRenderer.flipX = (angle > Mathf.PI / 2 && angle < 3 * Mathf.PI / 2);
                _shooter.Shoot(angle);
            }

            if (_carryFlowerAway)
            {
                // move away from the player
                transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, -_carrySpeed * Time.deltaTime);
                _animator.Play("BanditRun");
            }
        }
    }
}
