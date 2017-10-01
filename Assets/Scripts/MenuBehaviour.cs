using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehaviour : MonoBehaviour {

	public void triggerMenuBehaviour(int i)
    {
        switch(i)
        {
            default:

            case (0):
                SceneManager.LoadScene("level");
                break;

            case (1):
                Application.Quit();
                break;
        }
    }
}
