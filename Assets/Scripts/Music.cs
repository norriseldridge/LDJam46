using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField]
    private AudioSource _music;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartMusic()
    {
        _music.Play();
    }

    public void StopMusic()
    {
        _music.Stop();
    }
}
