using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {


    public void OpenScene1() { 

        SceneManager.LoadScene("Level1");

    }
    public void OpenScene2() { 

        SceneManager.LoadScene("Level2");

    }
}