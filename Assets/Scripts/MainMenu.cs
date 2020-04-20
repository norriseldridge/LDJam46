using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Image _fadeImage;
    [SerializeField]
    private AudioSource _selectSound;
    [SerializeField]
    private AudioSource _menuMusic;

    [SerializeField]
    private Image _playerImage;
    [SerializeField]
    private Sprite[] _playerTextures;
    private int _playerIndex = 0;
    private float _playerImageDelay = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            InputSetting.UseController = true;
            OnClickPlay();
        }

        _playerImageDelay += Time.deltaTime;
        if (_playerImageDelay > 0.4f)
        {
            _playerImageDelay = 0.0f;
            ++_playerIndex;

            if (_playerIndex >= _playerTextures.Length)
            {
                _playerIndex = 0;
            }
            _playerImage.sprite = _playerTextures[_playerIndex];
        }
    }

    public void OnClickPlay()
    {
        StartCoroutine(FadeOutAndLoad());
    }

    IEnumerator FadeOutAndLoad()
    {
        _selectSound.Play();
        while (_fadeImage.color.a < 1f)
        {
            _menuMusic.volume -= 0.7f * Time.deltaTime;
            _fadeImage.color = new Color(0, 0, 0, _fadeImage.color.a + 0.7f * Time.deltaTime);
            yield return null;
        }
        _fadeImage.color = Color.black;
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(Scenes.GAMEPLAY);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
