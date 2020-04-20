using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMessage : MonoBehaviour
{
    public static DisplayMessage GetInstance()
    {
        return GameObject.FindObjectOfType<DisplayMessage>();
    }

    public static IEnumerator ShowMessage(string message)
    {
        DisplayMessage instance = GetInstance();
        if (instance != null)
        {
            return instance.PrintMessage(message);
        }

        return null;
    }

    [SerializeField]
    private Canvas _canvas;

    [SerializeField]
    private Text _text;

    [SerializeField]
    private float _textDelay;

    [SerializeField]
    private AudioSource _tapSound;

    private void Start()
    {
        _canvas.enabled = false;
    }

    public IEnumerator PrintMessage(string message)
    {
        _canvas.enabled = true;
        _text.text = "";

        char last = ' ';
        foreach (char c in message)
        {
            _text.text += c;
            if (last == ' ')
            {
                _tapSound.Play();
            }
            float mod = (((int)c) / 800.0f);
            _tapSound.pitch = 0.9f + mod;
            _tapSound.volume = 0.5f + mod;
            last = c;
            yield return new WaitForSeconds(_textDelay);
        }

        // Wait to move on until the use presses to move on or its been 2 seconds
        float delay = 0;
        while (true)
        {
            delay += Time.deltaTime;
            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Submit") || delay >= 2.0f)
                break;
            yield return null;
        }

        _canvas.enabled = false;
    }
}
