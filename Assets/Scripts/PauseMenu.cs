using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Canvas canvas;
    string levelName;

    // Start is called before the first frame update
    void Start()
    {
        canvas = gameObject.GetComponent<Canvas>();

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        EnableCanvas();
    }

    private void EnableCanvas() {
        Scene currentScene = SceneManager.GetActiveScene();
        levelName = currentScene.name;

        if (!levelName.Equals("Start Menu") && Input.GetKeyDown(KeyCode.Escape))
        {
            
            canvas.enabled = !canvas.enabled;
            if (canvas.enabled == true) {
                Time.timeScale = 0;
            }
            else
                Time.timeScale = 1;
        }
    }

    public void ContinueButton() {
        canvas.enabled = false;
        Time.timeScale = 1;
    }
}
