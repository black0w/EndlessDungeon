using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    
    public GameObject[] Items;
   
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       GameObject[] rooms  = GameObject.FindGameObjectsWithTag("DeadEndRoom");
       foreach(GameObject room in rooms)
       {
           if(!room.GetComponent<RoomController>().hasItems)
           {
           
           GameObject clone = Instantiate(Items[Random.Range(0,Items.Length)],room.GetComponent<RoomController>().ItemSpawnPoint.position,Quaternion.identity);
          clone.transform.position += new Vector3(0,0,-0.07f);
           room.GetComponent<RoomController>().hasItems = true;
           }
       }
       
    }
}
