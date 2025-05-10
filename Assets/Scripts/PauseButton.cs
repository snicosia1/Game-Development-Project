using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{

public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        
        SceneManager.LoadScene("MainMenu");
        }
    }
}