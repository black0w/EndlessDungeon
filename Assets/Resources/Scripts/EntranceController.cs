using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.gameObject.name == "Entrance01") //top-bot
        {
            if(collision.transform.tag == "Player")
            {
                GameObject[] Dr = GameObject.FindGameObjectsWithTag("Entrance");
                foreach(GameObject d in Dr)
                {
                    if(Vector2.Distance(transform.position,d.transform.position)< 3 && d.transform.name == "Entrance02")
                    {
                        collision.transform.position = d.transform.position;
                        break;
                    }
                }             
                collision.transform.position += new Vector3(0,-2,0);
               Camera.main.transform.position += new Vector3(0, -10.16f, 0);

            }
        }

        if(this.gameObject.name =="Entrance02") // bot-top
        {
            if (collision.transform.tag == "Player")
            {
                GameObject[] Dr = GameObject.FindGameObjectsWithTag("Entrance");
                foreach (GameObject d in Dr)
                {
                    if (Vector2.Distance(transform.position, d.transform.position) < 3 && d.transform.name == "Entrance01")
                    {
                        collision.transform.position = d.transform.position;
                        break;
                    }
                }    
                collision.transform.position += new Vector3(0, 2, 0);
                Camera.main.transform.position += new Vector3(0, 10.16f, 0);
            }
        }

        if(this.gameObject.name =="Entrance03") //left-right
        {
            if (collision.transform.tag == "Player")
            {
                GameObject[] Dr = GameObject.FindGameObjectsWithTag("Entrance");
                foreach (GameObject d in Dr)
                {
                    if (Vector2.Distance(transform.position, d.transform.position) < 3 && d.transform.name == "Entrance04")
                    {
                        collision.transform.position = d.transform.position;
                        break;
                    }
                }          
                collision.transform.position += new Vector3(2, 0, 0);
                Camera.main.transform.position += new Vector3(18.14f, 0, 0);
            }
        }

        if (this.gameObject.name == "Entrance04") //right - left
        {
            if (collision.transform.tag == "Player")
            {
                GameObject[] Dr = GameObject.FindGameObjectsWithTag("Entrance");
                foreach (GameObject d in Dr)
                {
                    if (Vector2.Distance(transform.position, d.transform.position) < 3 && d.transform.name == "Entrance03")
                    {
                        collision.transform.position = d.transform.position;
                        break;
                    }
                }
             
                collision.transform.position += new Vector3(-2, 0, 0);
                Camera.main.transform.position += new Vector3(-18.14f, 0, 0);
            }
        }
    }
    bool canGo = false;
    bool Delay = false;
    IEnumerator WaitBeforeGoing()
    {
        if (Delay)
            yield break;

        Delay = true;

        //Waiting for 1 second before the next step
        yield return new WaitForSeconds(1f);
        canGo = true;
       

        Delay = false;

    }
}
