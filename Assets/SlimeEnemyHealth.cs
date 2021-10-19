using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemyHealth : MonoBehaviour
{

    public int Health = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Health <= 0)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 1f);
            foreach (Collider2D col in cols)
            {
                if (col.transform.name.Contains("Room"))
                {
                    col.transform.GetComponent<RoomController>().EnemyCounter -= 1;
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
