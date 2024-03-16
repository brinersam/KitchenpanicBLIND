using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum ScenesEnum
    {
        LoadingScreen = 0,
        MainMenu = 1,
        Gameplay = 2,
    }

    public static void LoadScene(ScenesEnum scene)
    {
        SceneManager.LoadScene((int)scene);
    }
}
