using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;
    
    /*void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (SceneManager.GetActiveScene().buildIndex < 1)
            {
                LoadNextLevel();
            }
        }
    }*/
    
    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < 1)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(1);
        
        //Load scene
        SceneManager.LoadScene(levelIndex);
    }
    
    
}
