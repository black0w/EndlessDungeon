using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuManager : MonoBehaviour
{

    public Light leftLight;
    public Light rightLight;
   

    public GameObject CreditsPanel;

    public GameObject[] objectsToBeDisabled;

    private bool isCreditsPanelActive = false;


    private void Awake()
    {
        Time.timeScale = 1;
        CreditsPanel.SetActive(false);
    }
   
    
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Repeat());
        if(Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
        CreditsPanel.SetActive(isCreditsPanelActive);

        if(isCreditsPanelActive && Input.GetKeyDown(KeyCode.Escape))
        {
            ShowCloseCredits();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowCloseCredits()
    {
        isCreditsPanelActive = !isCreditsPanelActive;
        foreach (GameObject obj in objectsToBeDisabled)
        {
            obj.SetActive(!isCreditsPanelActive);
        }     
    }

    public void QuitGame()
    {
       
        Application.Quit(); //this only works on built Application but not in the Editor
    }

    bool isActive = false;
    IEnumerator Repeat()
    {
        if (isActive)
            yield break;

        isActive = true;
        yield return new WaitForSeconds(0.1f);

        float intensity = Random.Range(0.5f, 0.8f);
        leftLight.intensity = intensity;
        rightLight.intensity = intensity;

        isActive = false;
    }
}
