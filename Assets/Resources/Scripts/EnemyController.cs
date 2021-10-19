using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private float movementSpeed = 2.5f;
    Transform Target;
    private float distanceToStop = 1f;
    public int Health = 30;
    public int AttackDamage = 10;
    public string RoomName;

    [SerializeField]
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += new Vector3(0, 0,  -3.39f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(Target != null)
       if (Vector2.Distance(transform.position, Target.position) > distanceToStop)
        {
            transform.position = Vector2.MoveTowards(transform.position,Target.position, movementSpeed * Time.deltaTime);
                transform.position += new Vector3(0, 0, -3.39f);
        }
        else
        {
                StartCoroutine(hit());
        }
    
        if(Health <= 0 )
        {
           
            isDead = true;
         //   Debug.Log("Enemy Destroyed. "+ GameObject.Find(RoomName).GetComponent<RoomController>().EnemyCounter + "enemies left"); // Debug property
          //  
        }
    }

    bool isAttacking = false;
    IEnumerator hit()
    {
        if (isAttacking)
            yield break;

        isAttacking = true;
        Target.GetComponent<CharacterHealth>().Health -= AttackDamage;
        yield return new WaitForSeconds(1.5f);
       
        isAttacking = false;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.transform.name.Contains("Room"))
            {
                if(isDead)
                {
                    Debug.Log(other.transform.name);
                    other.transform.GetComponent<RoomController>().EnemyCounter -=1;
                    Destroy(this.gameObject);
                }
            }
    }
}
