using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {
    void Start() {

    }

    public void OpenScene() { 
        SceneManager.LoadScene("Level1");

    }
}