using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogscript : MonoBehaviour
{
    public static int selectedLevel;
    public int level;

    public void LoadScene(string sceneName)
    {
        selectedLevel = level;
        SceneManager.LoadScene(sceneName);
    }
}
