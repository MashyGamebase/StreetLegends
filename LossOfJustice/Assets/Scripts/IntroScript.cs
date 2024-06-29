using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroScript : MonoBehaviour
{
    public float wait_time = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitIntro());
    }

   IEnumerator waitIntro()
    {
        yield return new WaitForSeconds(wait_time);

        SceneManager.LoadScene(1);
    }
}
