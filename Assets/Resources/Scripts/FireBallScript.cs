using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour
{

    public Vector2 dir = Vector2.zero;
    int Damage;

    public string origin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        Damage = GameObject.Find("Player").GetComponent<CharacterMovement>().Damage;
        if(dir != Vector2.zero)
        {
          
            transform.Translate(dir * 10f * Time.deltaTime, Space.World);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((origin == "Player" &&collision.gameObject.tag == "Enemy") || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Door"
            || (origin == "Enemy" && collision.gameObject.tag == "Player") || (origin == "Player" && collision.gameObject.tag == "SpitterEnemy"))
        {
            
            dir = Vector2.zero;
            GetComponent<Animator>().SetBool("hit", true);
            GetComponent<CircleCollider2D>().enabled = false;
            if(origin == "Player" && collision.gameObject.tag == "Enemy")
            {
                collision.GetComponent<EnemyController>().Health -= Damage;
            }
            if(origin == "Player" &&   collision.gameObject.tag == "SpitterEnemy")
            {
                collision.GetComponent<SpitterEnemyController>().Health -= Damage;
            }

            if(origin == "Player" && collision.gameObject.tag =="SlimeEnemy")
            {
                collision.GetComponent<SlimeEnemyHealth>().Health -= Damage;
            }
            if ( origin == "Enemy" && collision.gameObject.tag == "Player" )
            {
                collision.GetComponent<CharacterHealth>().Health -= Damage;
                Destroy(gameObject, 0.4f);
            }
            if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Door")
            {
                Destroy(gameObject, 0.4f);
            }



        }
    }
}
