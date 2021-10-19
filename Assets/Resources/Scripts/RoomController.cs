using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public bool hasItems = false;
    public Transform ItemSpawnPoint;
    public bool[] CheckDoors;
    public int EnemyCounter;

    private GameObject[] Doors;
  
    public GameObject[] EnemyPrefabs;

    public Transform[] EnemySpawnPoints;

    public bool playerWasHere = false;

    public GameObject PlayerPrefab;

    public Transform PlayerSpawnPoint;

    bool doOnce = false;

    public bool readyToSpawn = false;
    public Sprite[] DoorSprites;
    public Material[] DoorMats;
    private int spawnPoint;
     GameObject clone;
    // Start is called before the first frame update
    void Start()
    {
        
       
       
        if (gameObject.name == "StartupRoom")
        {
            Doors = GameObject.FindGameObjectsWithTag("Door");
            OpenDoors();
            GameObject PlayerClone = Instantiate(PlayerPrefab, PlayerSpawnPoint.position, Quaternion.identity);
            PlayerClone.name = "Player";
        }

        spawnPoint = EnemySpawnPoints.Length - 1;
    }

       

    // Update is called once per frame
    void Update()
    {
        

        if (playerWasHere && EnemyCounter <= 0 && !doOnce)
              {
                  doOnce = true;
                  GameObject.Find("Player").GetComponent<CharacterMovement>().PlayerLevel+=1;
               //    Debug.Log("Player Leveled Up");
                OpenDoors();
              }

    }

   
    private void OnTriggerEnter2D(Collider2D other) {

      
        if (other.gameObject.tag == "Player" && !playerWasHere && this.gameObject.name != "StartupRoom"  )
             {
            Doors = GameObject.FindGameObjectsWithTag("Door");
            playerWasHere = true;
                CloseDoors();
                SpawnEnemy(other);

             }  

        if(other.gameObject.tag == "Player")
        {
            GameObject.Find("Rooms").GetComponent<RoomGenerator>().CurrentRoom = this.transform;
        }
    }


    private void CloseDoors()
    {
     
        foreach(GameObject Door in Doors)
            {
                
                Door.GetComponent<BoxCollider2D>().isTrigger = false;
            Door.GetComponent<SpriteRenderer>().material = DoorMats[1];
            Door.GetComponent<SpriteRenderer>().sprite = DoorSprites[1];
             //   Debug.Log(Door.gameObject.name + " was Closed");
            }
    }
    
    private void OpenDoors()
    {
         
        foreach(GameObject Door in Doors)
            {
                Door.GetComponent<BoxCollider2D>().isTrigger =true;
            Door.GetComponent<SpriteRenderer>().material = DoorMats[0]; //Added custom material so the door can react to light
            Door.GetComponent<SpriteRenderer>().sprite = DoorSprites[0];
          //  Debug.Log(Door.gameObject.name + " was Opened"); //for debugging property
              
            }
              
    }
   
    private void SpawnEnemy(Collider2D other)
    {
        if (GameObject.Find("Player").GetComponent<CharacterMovement>().PlayerLevel == 1)
        {
            clone = Instantiate(EnemyPrefabs[0], EnemySpawnPoints[Random.Range(0, EnemySpawnPoints.Length)].position, Quaternion.identity);
            clone.transform.position += new Vector3(0, 0, -3.39f);
            clone.GetComponent<EnemyController>().RoomName = this.gameObject.name;
            EnemyCounter = 1;
        }
        else if (GameObject.Find("Player").GetComponent<CharacterMovement>().PlayerLevel == 2)
        {
            clone = Instantiate(EnemyPrefabs[1], EnemySpawnPoints[Random.Range(0, EnemySpawnPoints.Length)].position, Quaternion.identity);
            clone.transform.position += new Vector3(0, 0, -3.39f);
            clone.GetComponent<SpitterEnemyController>().RoomName = this.gameObject.name;
            EnemyCounter = 1;
        }
        else  if (GameObject.Find("Player").GetComponent<CharacterMovement>().PlayerLevel == 3)
        {
            clone = Instantiate(EnemyPrefabs[1], EnemySpawnPoints[Random.Range(0, EnemySpawnPoints.Length)].position, Quaternion.identity);
            clone.transform.position += new Vector3(0, 0, -3.39f);
            clone = Instantiate(EnemyPrefabs[0], EnemySpawnPoints[Random.Range(0, EnemySpawnPoints.Length)].position, Quaternion.identity);
            clone.transform.position += new Vector3(0, 0, -3.39f);
            clone.GetComponent<EnemyController>().RoomName = this.gameObject.name;
            EnemyCounter = 2;
        }
        else
        {

           
          int count = Random.Range(2, 5);
            EnemyCounter = count;
            Debug.Log("Enemies in this room: "+EnemyCounter.ToString());
            for (int i = 0;  i < count ; i++)
            {
             
              
               
                    clone = Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)], EnemySpawnPoints[spawnPoint].position, Quaternion.identity);
                clone.transform.position += new Vector3(0, 0, -3.39f);
                 if (clone.gameObject.name.Contains("Spitter"))
                        clone.GetComponent<SpitterEnemyController>().RoomName = this.gameObject.name;
                    else
                        clone.GetComponent<EnemyController>().RoomName = this.gameObject.name;
                    spawnPoint--;
                
               
            } 
        }
      
    }
    
}


