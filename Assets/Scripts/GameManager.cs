using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    [SerializeField] private List<string> scenesCompleted = new List<string>();

    [SerializeField] public Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        buttons = Button.FindObjectsOfType<Button>();

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        ActivateActiveButtons();
    }

    private void ActivateActiveButtons() {
        buttons = Button.FindObjectsOfType<Button>();
        foreach (string scene in scenesCompleted)
        {
            int lvl = int.Parse(scene.Split()[1]) + 1;
            foreach (Button b in buttons)
            {
                if (b.name.Equals("Level " + lvl))
                {
                    b.interactable = true;
                }
            }
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartTutorial()
    {
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Tutorial"));
    }

    public void StartLevel1()
    {
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Level 1"));
    }

    public void StartLevel2()
    {
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Level 2"));
    }

    public void StartLevel3()
    {
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Level 3"));
    }

    public void StartLevelMenu()
    {
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Level Menu"));
    }
    public void StartStartMenu()
    {
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Start Menu"));
    }

    private void LevelCompleted(string completed) {
        scenesCompleted.Add(completed);
        StartLevelMenu();
    }
}
