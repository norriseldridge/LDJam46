using UnityEngine;

public class Flower : MonoBehaviour
{
    public delegate void OnDestoryed();

    private OnDestoryed _onDestroyed;

    public void AddOnDestoryListener(OnDestoryed onDestoryed)
    {
        _onDestroyed += onDestoryed;
    }

    public void RemoveOnDestoryListener(OnDestoryed onDestoryed)
    {
        _onDestroyed -= onDestoryed;
    }

    private void OnDestroy()
    {
        if (_onDestroyed != null)
        {
            _onDestroyed.Invoke();
        }
    }
}
