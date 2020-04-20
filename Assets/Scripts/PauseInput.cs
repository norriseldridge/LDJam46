using UnityEngine;

public class PauseInput : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Did we press pause?
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.Pause();
        }
    }
}
