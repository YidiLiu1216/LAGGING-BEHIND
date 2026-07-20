using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] float rotationSpeed=1.0f;
    [SerializeField] float rotationAmount =10f;
    [SerializeField] GameObject titileLine;
    [SerializeField] GameObject title;
    [SerializeField] GameObject StartPanel;

    [SerializeField] float delayToStart = 1.0f;
    bool isTransitioning;
    public void startGame()
    {
        StartCoroutine(DoSceneChange());
    }
    public void exitGame()

    {
        Application.Quit();
    }
    private void Update()
    {
        if (titileLine.activeInHierarchy)
        {
            float t = Time.time * (2f * Mathf.PI / rotationSpeed);

            // Mathf.Sin oscillates between -1 and +1
            float z = Mathf.Sin(t) * rotationAmount;

            titileLine.transform.localRotation = Quaternion.Euler(0f, 0f, z);
            if (Input.anyKey)
            {
                titileLine.SetActive(false);
                title.SetActive(false);
                StartPanel.SetActive(true);
            }
        }
        else {
            
        }
    }
    public void PressAnyButtontoStart() {

    }


    private System.Collections.IEnumerator DoSceneChange()
    {

        // play sound
        StartSceneAduio.instance.PlayMenuSelectSFX();

        // wait (unscaled so it works even if paused)
        yield return new WaitForSecondsRealtime(1.0f);

        // load new scene
        SceneManager.LoadScene("level1");
    }
}

