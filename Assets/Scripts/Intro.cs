using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField]
    private Image _fadeInImage;
    private Music _music;
    [SerializeField]
    private Player _player;
    [Range(0.1f, 1f)]
    [SerializeField]
    private float _playerWalkSpeed;
    [SerializeField]
    private AudioSource _step;
    [SerializeField]
    private float _stepDelay;
    private float _remainingStepDelay;
    [SerializeField]
    private string[] _dialog;
    [SerializeField]
    private Canvas _titleCanvas;
    [SerializeField]
    private HUD _hudCanvas;
    [SerializeField]
    private Canvas _introCanvas;
    [SerializeField]
    private LevelLogic _levelLogic;

    void Awake()
    {
        _titleCanvas.enabled = false;
        _hudCanvas.enabled = false;
        _remainingStepDelay = _stepDelay;
    }

    private void Start()
    {
        _music = FindObjectOfType<Music>();
        StartCoroutine(RunCinematic());
    }

    IEnumerator RunCinematic()
    {
        // Don't allow the player to move
        _player.SetCrossSightActive(false);
        _player.GetComponent<Player>().enabled = false;

        // Walk the player to the flower
        yield return WalkPlayerToFlower();

        // Start the idle animation
        _player.GetComponent<Animator>().Play("FarmerIdle");

        // Disable the follow script
        Camera.main.GetComponent<Follow>().enabled = false;

        // Wait
        yield return new WaitForSeconds(0.25f);

        // Display text
        foreach (string message in _dialog)
        {
            yield return DisplayMessage.ShowMessage(message);
        }

        // Player is now in control
        _player.GetComponent<Player>().enabled = true;

        // Move the player's cross sight out
        _player.CrossSightDistance = 0.3f;
        _player.SetCrossSightActive(true);

        // Start the music
        _music.StartMusic();

        // Wait to sync the show with the first "gun shot" sound in the song
        yield return new WaitForSeconds(3.69f);

        // Show the title
        yield return ShowTitle();

        // Show the hud
        yield return new WaitForSeconds(0.5f);
        _hudCanvas.enabled = true;

        // Start the level
        _levelLogic.enabled = true;
    }

    IEnumerator WalkPlayerToFlower()
    {
        while (_player.transform.position.x < -0.18f)
        {
            _fadeInImage.color = new Color(0, 0, 0, _fadeInImage.color.a - Time.deltaTime);
            _remainingStepDelay -= Time.deltaTime;
            if (_remainingStepDelay <= 0)
            {
                _remainingStepDelay = _stepDelay;
                _step.pitch = Random.Range(0.8f, 0.9f);
                _step.Play();
            }
            
            _player.transform.position += Vector3.right * _playerWalkSpeed * Time.deltaTime;
            _player.GetComponent<Animator>().Play("FarmerWalk");
            yield return null;
        }
        _introCanvas.enabled = false;
    }

    IEnumerator ShowTitle()
    {
        _titleCanvas.enabled = true;
        yield return new WaitForSeconds(2.3f);
        _titleCanvas.enabled = false;
    }
}
