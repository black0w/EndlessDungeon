using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CharacterHealth : MonoBehaviour
{
    public Sprite[] Hearts;
    //0 - full;
    //1 - empty;
    public GameObject[] ArmourHearts;
    //0 - First
    //1 - Second
    //2 - Third 

    public Image FirstHeart;
    
    public Image SecondHeart;
    
    public Image ThirdHeart;

    public int Health = 30;
    string[] deathMessages = new string[]{"What a big surprice,\n you died...", "Well, you died...\nTry again?", "And thats how you died...","And thats how \n 'Your name Here' \n Died", "Stop failing, please?",
    "Will you even try?","Are you even tryin' ?", "Try again?", "You failed miserably...\nhow..?","Your story ends here\n...sadly" , "...The End"};
  
    public Text DeathText;
    
    public GameObject DeathScreen;
   
    private bool doOnce = false;
   
    // Start is called before the first frame update
    void Start()
    {
        FirstHeart.sprite = Hearts[0];
        SecondHeart.sprite = Hearts[0];
        ThirdHeart.sprite = Hearts[0];
      
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Health >= 30)
        {
            FirstHeart.sprite = Hearts[0];
            SecondHeart.sprite = Hearts[0];
            ThirdHeart.sprite = Hearts[0];
        }
        else if(Health >=20)
        {
            FirstHeart.sprite = Hearts[1];
            SecondHeart.sprite = Hearts[0];
            ThirdHeart.sprite = Hearts[0];
        }
        else if(Health >=10)
        {
            FirstHeart.sprite = Hearts[1];
            SecondHeart.sprite = Hearts[1];
            ThirdHeart.sprite = Hearts[0];
        }
        else if(Health <=0)
        {
            if (!doOnce)
            {
                doOnce = true;
                FirstHeart.sprite = Hearts[1];
                SecondHeart.sprite = Hearts[1];
                ThirdHeart.sprite = Hearts[1];

                DeathScreen.SetActive(true);
                DeathText.text = deathMessages[Random.Range(0, deathMessages.Length)];
                
            }

        

        }

        if(Health <=0 && Input.GetKeyDown(KeyCode.Return) )
        {
            GoToMainMenu();
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Hearth" && Health < 30)
        {
            Destroy(collision.gameObject);
            Health += 10;

        }
    }

}
