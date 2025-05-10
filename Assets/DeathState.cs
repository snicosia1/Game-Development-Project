using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathStateBehavior : StateMachineBehaviour
{
  
    private bool sceneLoaded = false;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
            SceneManager.LoadScene("MainMenu");
            sceneLoaded = true;
        
    }
}
