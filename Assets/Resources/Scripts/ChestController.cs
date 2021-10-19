using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject[] ItemPrefabs;
    GameObject Clone;
    Vector2 dir;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Player")
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            Clone = Instantiate(ItemPrefabs[Random.Range(0,ItemPrefabs.Length)], transform.position, Quaternion.identity);
           
            dir = Random.insideUnitCircle * 5;

           Clone.GetComponent<Rigidbody2D>().AddForce( dir * 100 * Time.deltaTime ,ForceMode2D.Impulse);
           
            StartCoroutine(Wait());
            

        }   
    }

    bool isActive = false;
    IEnumerator Wait()
    {

        if (isActive)
            yield break;

        isActive = true;
        yield return new WaitForSeconds(1);
        Clone.GetComponent<CircleCollider2D>().enabled = true;
        Clone.GetComponent<Rigidbody2D>().AddForce(-dir * 100);
        
        Destroy(this.gameObject);


        isActive = false;
    }
}
