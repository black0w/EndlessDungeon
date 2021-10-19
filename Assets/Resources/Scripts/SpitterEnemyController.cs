using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterEnemyController : MonoBehaviour
{

    public GameObject FireBallPrefab;

    private Transform Player;

    public int Health = 30;

    bool canShoot = true;

    public string RoomName;

  
    // Update is called once per frame
    void Update()
    {
        Player = GameObject.Find("Player").transform;
        if (canShoot)
        {
            GameObject FireBallClone = Instantiate(FireBallPrefab, transform.position, Quaternion.identity);
            FireBallClone.GetComponent<FireBallScript>().origin = "Enemy";
            var dis = Player.position - transform.position;
            var distance = dis.magnitude;
            Vector2 direction = dis / distance;
            FireBallClone.GetComponent<FireBallScript>().dir = direction;
            StartCoroutine(ShootCoroutine());
        }

        if(Health <=0)
        {
           
              Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position,1f);
              foreach(Collider2D col in cols)
              {
                  if(col.transform.name.Contains("Room"))
                  {           
                    col.transform.GetComponent<RoomController>().EnemyCounter -=1;
                    Destroy(this.gameObject);
                  }
              }


        }
    }

    bool isShooting = false;

    //Putting Delay for Shooting
    //IEnumerator is used in "Coroutines" and repeats unless we stop it
    IEnumerator ShootCoroutine()
    {
        if (isShooting)
            yield break;

        isShooting = true;
        canShoot = false;
        yield return new WaitForSeconds(1.2f);
        
        canShoot = true;
        isShooting = false;

    }

   
}
