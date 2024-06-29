using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image barFill;

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void gotoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayGame(int sceneID)
    {
        if (loadingScreen == null || barFill == null)
        {
            Debug.LogError("LoadingScreen or BarFill is not assigned in the inspector.");
            return;
        }

        StartCoroutine(LoadSceneAsync(sceneID));
    }

    private IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        operation.allowSceneActivation = false;

        loadingScreen.SetActive(true);

        while (operation.progress < 0.9f)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            barFill.fillAmount = progressValue;

            yield return null;
        }

        // Set the progress bar to full
        barFill.fillAmount = 1.0f;

        // Wait for an additional second before activating the scene
        yield return new WaitForSeconds(1f);

        // Allow the scene to activate
        operation.allowSceneActivation = true;

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}


//public void Options(int levelID)
//{
//    string levelName = "Game " + levelID;
//    SceneManager.LoadScene(levelName);

//}



//public void MainMenu()
//{
//    SceneManager.LoadScene("MainMenu");
//}

//public void PlayerProfile()
//{
//    SceneManager.LoadScene("PlayerProfile");
//}




