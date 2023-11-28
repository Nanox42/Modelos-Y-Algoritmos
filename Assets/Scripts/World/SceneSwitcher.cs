using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private void Update()
    {
        // if(Input.GetKey("f"))
        // {
        //     ReloadLevel();
        // }
    }
    public void ReloadLevel()
    {
        Debug.Log("start scene");
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
