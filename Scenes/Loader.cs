using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        if (scene == ScenesEnum.LoadingScreen) return;
        
        SceneManager.LoadSceneAsync((int)ScenesEnum.LoadingScreen);

        SceneManager.LoadSceneAsync((int)scene);
    }

    // private IEnumerator LoadSceneFancy(ScenesEnum scene)
    // {
    //     //receive loading bar
    //     Debug.Log("bruh4");

    //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)scene);

    //     while (asyncLoad.isDone == false)
    //     {
    //         Debug.Log($"Loading progress: { asyncLoad.progress }");
    //         yield return null;
    //     }

    // }
}
