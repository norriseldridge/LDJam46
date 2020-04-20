using System.Collections;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private GameObject _heartSource;

    [SerializeField]
    private Transform _heartsContainer;

    // Start is called before the first frame update
    void Start()
    {
        Player player = FindObjectOfType<Player>();
        EntityWithHealth playerEntity = player.GetComponent<EntityWithHealth>();
        SetHearts(playerEntity.Health);
        playerEntity.AddHealthChangeListener(OnPlayerHealthChange);
    }

    public void SetHearts(int count)
    {
        StartCoroutine(ShowHeartsOneByOne(count));
    }

    private IEnumerator ShowHeartsOneByOne(int count)
    {
        int hadCount = _heartsContainer.transform.childCount;
        if (_heartsContainer.transform.childCount < count)
        {
            // add the correct number
            for (int i = hadCount; i < count; ++i)
            {
                GameObject temp = Instantiate(_heartSource, _heartsContainer);
                Vector3 targetScale = temp.transform.localScale;
                temp.transform.localScale = Vector3.zero;
                while (temp.transform.localScale.magnitude < targetScale.magnitude)
                {
                    temp.transform.localScale += Vector3.one * 3 * Time.deltaTime;
                    yield return null;
                }
            }
        }
        else
        {
            for (int i = count; i < hadCount; ++i)
            {
                Destroy(_heartsContainer.GetChild(0).gameObject);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private void OnPlayerHealthChange(DamageSource damageSource, int newHealth)
    {
        SetHearts(newHealth);
    }
}
