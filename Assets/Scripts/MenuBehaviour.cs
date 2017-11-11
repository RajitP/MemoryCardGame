using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehaviour : MonoBehaviour {

    GameObject g;

    private void Start()
    {
        g = GameObject.FindGameObjectWithTag("EditorOnly");
    }

    public void triggerMenuBehaviour(int i)
    {
        switch(i)
        {
            default:

            case (0):
                SceneManager.LoadScene("level");
                break;

            case (1):
                SceneManager.LoadScene("level - smart");
                break;

            case (2):
                SceneManager.LoadScene("level - genius");
                break;

            case (3):
                Application.Quit();
                break;
        }
    }
}
