using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private Text _killText;
    [SerializeField]
    private AudioSource _deadMusic;

    private Player _player;
    private Flower _flower;

    private void Start()
    {
        _canvas.enabled = false;

        _player = FindObjectOfType<Player>();
        _flower = FindObjectOfType<Flower>();
        _player.GetComponent<EntityWithHealth>().AddDeathListener(OnPlayerLose);
        _flower.AddOnDestoryListener(OnPlayerLose);
    }

    private void OnPlayerLose()
    {
        if (_player != null)
        {
            _player.GetComponent<EntityWithHealth>().RemoveDeathListener(OnPlayerLose);
        }

        if (_flower != null)
        {
            _flower.RemoveOnDestoryListener(OnPlayerLose);
        }

        if (_deadMusic)
            _deadMusic.Play();

        Music music = FindObjectOfType<Music>();
        if (music)
            FindObjectOfType<Music>().StopMusic();

        _canvas.enabled = true;
        _killText.text = Bandit.KilledCount + " Bandits Killed";

        StartCoroutine(WaitForInput());
    }

    private IEnumerator WaitForInput()
    {
        if (Input.GetButtonDown("Submit"))
        {
            OnRetry();
        }

        yield return null;
    }

    public void OnRetry()
    {
        // just reload the scene
        SceneManager.LoadScene(Scenes.GAMEPLAY);

        // Reset the count!
        Bandit.KilledCount = 0;
    }

    public void OnQuit()
    {
        // load the main menu scene
        SceneManager.LoadScene(Scenes.MAIN_MENU);

        // Reset the count!
        Bandit.KilledCount = 0;
    }
}
