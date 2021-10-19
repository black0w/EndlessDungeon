using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public GameObject SettingsMenu;
    bool SettingMenuIsOpen = false;

    public Light[] Lights;

    private void Start()
    {
        Lights = FindObjectsOfType<Light>();    
    }


    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Repeat());
        SettingsMenu.SetActive(SettingMenuIsOpen);

        if(Input.GetKeyDown(KeyCode.Escape) && GameObject.Find("Player").GetComponent<CharacterHealth>().Health > 0)
        {
            SettingMenuIsOpen = !SettingMenuIsOpen;
        }

        if(SettingsMenu.activeSelf ||(GameObject.Find("Player")!= null && GameObject.Find("Player").GetComponent<CharacterHealth>().Health<=0))
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ResumeGame()
    {
        SettingMenuIsOpen = false;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }


    bool justDoit = false;
    IEnumerator Repeat()
    {
        if (justDoit)
            yield break;

        justDoit = true;
        yield return new WaitForSeconds(0.1f);

        float intensity = Random.Range(0.5f, 0.8f);
        foreach(Light l in Lights)
        {
            l.intensity = intensity;
        }
       

        justDoit = false;
    }
}
