using System.Collections;
using UnityEngine;

public class EntityWithHealth : MonoBehaviour
{
    public delegate void OnHealthChange(DamageSource damageSource, int newHealthValue);
    public delegate void OnDead();

    [SerializeField]
    private int _health;
    public int Health { get { return _health; } }
    private OnHealthChange _onHealthChange;
    private OnDead _onDead;

    [SerializeField]
    private float _onDamageBounce;
    private bool _canTakeDamage = true;
    private float _iFrameDuration = 0.1f;
    [SerializeField]
    private int _iFrames = 10;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Rigidbody2D _rb;

    private void OnDestroy()
    {
        if (_onHealthChange != null)
        {
            foreach (System.Delegate d in _onHealthChange.GetInvocationList())
            {
                _onHealthChange -= (OnHealthChange)d;
            }
        }

        if (_onDead != null)
        {
            foreach (System.Delegate d in _onDead.GetInvocationList())
            {
                _onDead -= (OnDead)d;
            }
        }
    }

    void OnRestoreHealth(int amount)
    {
        _health += amount;
        if (_onHealthChange != null)
            _onHealthChange.Invoke(null, _health);
    }

    void OnTakeDamage(DamageSource damageSource)
    {
        if (_canTakeDamage && damageSource.tag != gameObject.tag)
        {
            _health -= damageSource.Damage;
            if (_health <= 0)
            {
                _health = 0;

                if (_onDead != null)
                {
                    _onDead.Invoke();
                }
            }

            if (_onHealthChange != null)
                _onHealthChange.Invoke(damageSource, _health);

            // Bounce back
            Vector2 bounce = transform.position - damageSource.transform.position;
            bounce.Normalize();
            _rb.AddForce(_onDamageBounce * bounce, ForceMode2D.Impulse);

            // Start IFrames
            StartCoroutine(WaitIFrames());
        }
    }

    public void AddHealthChangeListener(OnHealthChange onHealthChange)
    {
        _onHealthChange += onHealthChange;
    }

    public void RemoveHealthChangeListener(OnHealthChange onHealthChange)
    {
        _onHealthChange -= onHealthChange;
    }

    public void AddDeathListener(OnDead onDead)
    {
        _onDead += onDead;
    }

    public void RemoveDeathListener(OnDead onDead)
    {
        _onDead -= onDead;
    }

    private IEnumerator WaitIFrames()
    {
        Color red = new Color(0.6f, 0, 0, 0.4f);
        Color white = new Color(1, 1, 1, 0.4f);
        _canTakeDamage = false;
        for (int i = 0; i < _iFrames; ++i)
        {
            _spriteRenderer.color = i % 2 == 0 ? red : white;
            yield return new WaitForSeconds(_iFrameDuration);
        }
        _spriteRenderer.color = Color.white;
        _canTakeDamage = true;
    }
}
