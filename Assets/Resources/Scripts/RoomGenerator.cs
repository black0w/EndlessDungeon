using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{

  
    public GameObject StartupRoom;
    public bool FirstRoomInstantiated = false;

    public GameObject[] TopRooms;
    public GameObject[] TopRoomsPreview;
    public GameObject[] BottomRooms;
    public GameObject[] BottomRoomsPreview;
    public GameObject[] LeftRooms;
    public GameObject[] LeftRoomsPreview;
    public GameObject[] RightRooms;
    public GameObject[] RightRoomsPreview;
    public Transform CurrentRoom;

    Vector2 pos;

    GameObject RoomClone;
    GameObject RoomPreviewClone;
    Collider2D col;
    int RoomChoice;
    List<int> RoomChoices;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
     GameObject room = Instantiate(StartupRoom, new Vector2(0,0), Quaternion.identity);
        Camera.main.transform.position = new Vector3(room.transform.Find("Center").transform.position.x, room.transform.Find("Center").transform.position.y, -8.72f);
        room.transform.name = StartupRoom.transform.name;
        room.transform.SetParent(this.transform);
        FirstRoomInstantiated = true;
    }

    public void CreateRoomUp()
    {
        pos = new Vector2(CurrentRoom.transform.position.x, CurrentRoom.transform.position.y + 10.16f);
       
        if (Physics2D.OverlapCircle(pos, 0.1f) == null)
            CreateTheRoom(1, pos);
    }

    public void CreateRoomDown()
    {
        pos = new Vector2(CurrentRoom.transform.position.x, CurrentRoom.transform.position.y - 10.16f);
      
        if(Physics2D.OverlapCircle(pos,0.1f) == null)
        CreateTheRoom(2, pos);
    }

    public void CreateRoomLeft()
    {
        pos = new Vector2(CurrentRoom.transform.position.x - 18.14f, CurrentRoom.transform.position.y);
     
        if (Physics2D.OverlapCircle(pos, 0.1f) == null)
            CreateTheRoom(3, pos);
    }

    public void CreateRoomRight()
    {
        pos = new Vector2(CurrentRoom.transform.position.x + 18.14f, CurrentRoom.transform.position.y);
      
        if (Physics2D.OverlapCircle(pos, 0.1f) == null)
            CreateTheRoom(4, pos);
    }

    bool hasDoorError = false;
    bool hasWallError = false;
    private void CreateTheRoom(int type, Vector2 pos)
    {
      if(type == 1)
        {
            hasDoorError = false;
            hasWallError = false;
            if (RoomPreviewClone != null)
                Destroy(RoomPreviewClone);

            RoomChoice = Random.Range(0, BottomRooms.Length);

            RoomPreviewClone = Instantiate(BottomRoomsPreview[RoomChoice],pos, Quaternion.identity);
            for (int i = 0; i < RoomPreviewClone.transform.childCount; i++) 
            {

                if(RoomPreviewClone.transform.GetChild(i).tag == "Door" )
                {
                    Transform child = RoomPreviewClone.transform.GetChild(i);
                    Collider2D[] cols = Physics2D.OverlapCircleAll(child.position, 4f);
                    foreach(Collider2D col in cols)
                    {
                       if(col.transform.name.Contains("DoorWall"))
                        {
                            Debug.Log("Error found, Door: " + child.name + " is facing DoorWall: " + col.transform.name + " in Room:" + RoomPreviewClone.name);

                            hasDoorError = true;
                            break;
                        }

                       
                    }
                }
                if (RoomPreviewClone.transform.GetChild(i).name.Contains("DoorWall"))
                {
                    Transform child = RoomPreviewClone.transform.GetChild(i);
                    Collider2D[] cols = Physics2D.OverlapCircleAll(child.position, 4f);
                    foreach (Collider2D col in cols)
                    {
                        if (col.transform.tag == "Door")
                        {
                            Debug.Log("Error found, Wall: " + child.name + " is facing Door: " + col.transform.name + " in Room:" + RoomPreviewClone.name);

                            hasWallError = true;
                            break;
                        }


                    }
                }

                if (hasDoorError || hasWallError)
                    break;
                
            }
            if(hasDoorError || hasWallError)
            {
                CreateTheRoom(type, pos);
            }
            else
            {
                Debug.Log("No error found..removing the Preview room and creating the actual room..." + BottomRooms[RoomChoice]);
                if (RoomPreviewClone != null)
                    Destroy(RoomPreviewClone);
                Instantiate(BottomRooms[RoomChoice], pos, Quaternion.identity);
            }
        }

      if(type == 2)
        {
            hasDoorError = false;
            hasWallError = false;
            if (RoomPreviewClone != null)
                Destroy(RoomPreviewClone);

            RoomChoice = Random.Range(0, TopRooms.Length);

            RoomPreviewClone = Instantiate(TopRoomsPreview[RoomChoice], pos, Quaternion.identity);
            for (int i = 0; i < RoomPreviewClone.transform.childCount; i++)
            {

                if (RoomPreviewClone.transform.GetChild(i).tag == "Door")
                {
                    Transform child = RoomPreviewClone.transform.GetChild(i);
                    Collider2D[] cols = Physics2D.OverlapCircleAll(child.position, 4f);
                    foreach (Collider2D col in cols)
                    {
                        if (col.transform.name.Contains("DoorWall"))
                        {
                            Debug.Log("Error found, Door: " + child.name + " is facing DoorWall: " + col.transform.name + " in Room:" + RoomPreviewClone.name);

                            hasDoorError = true;
                            break;
                        }


                    }
                }
                if (RoomPreviewClone.transform.GetChild(i).name.Contains("DoorWall"))
                {
                    Transform child = RoomPreviewClone.transform.GetChild(i);
                    Collider2D[] cols = Physics2D.OverlapCircleAll(child.position, 4f);
                    foreach (Collider2D col in cols)
                    {
                        if (col.transform.tag == "Door")
                        {
                            Debug.Log("Error found, Wall: " + child.name + " is facing Door: " + col.transform.name + " in Room:" + RoomPreviewClone.name);

                            hasWallError = true;
                            break;
                        }


                    }
                }

                if (hasDoorError || hasWallError)
                    break;

            }
            if (hasDoorError || hasWallError)
            {
                CreateTheRoom(type, pos);
            }
            else
            {
                Debug.Log("No error found..removing the Preview room and creating the actual room..." + TopRooms[RoomChoice]);
                if (RoomPreviewClone != null)
                    Destroy(RoomPreviewClone);
                Instantiate(TopRooms[RoomChoice], pos, Quaternion.identity);
            }
        }

      if(type == 3)
        {
            hasDoorError = false;
            hasWallError = false;
            if (RoomPreviewClone != null)
                Destroy(RoomPreviewClone);

            RoomChoice = Random.Range(0, RightRooms.Length);

            RoomPreviewClone = Instantiate(RightRoomsPreview[RoomChoice], pos, Quaternion.identity);
            for (int i = 0; i < RoomPreviewClone.transform.childCount; i++)
            {

                if (RoomPreviewClone.transform.GetChild(i).tag == "Door")
                {
                    Transform child = RoomPreviewClone.transform.GetChild(i);
                    Collider2D[] cols = Physics2D.OverlapCircleAll(child.position, 4f);
                    foreach (Collider2D col in cols)
                    {
                        if (col.transform.name.Contains("DoorWall"))
                        {
                            Debug.Log("Error found, Door: " + child.name + " is facing DoorWall: " + col.transform.name + " in Room:" + RoomPreviewClone.name);

                            hasDoorError = true;
                            break;
                        }


                    }
                }
                if (RoomPreviewClone.transform.GetChild(i).name.Contains("DoorWall"))
                {
                    Transform child = RoomPreviewClone.transform.GetChild(i);
                    Collider2D[] cols = Physics2D.OverlapCircleAll(child.position, 4f);
                    foreach (Collider2D col in cols)
                    {
                        if (col.transform.tag == "Door")
                        {
                            Debug.Log("Error found, Wall: " + child.name + " is facing Door: " + col.transform.name + " in Room:" + RoomPreviewClone.name);

                            hasWallError = true;
                            break;
                        }


                    }
                }

                if (hasDoorError || hasWallError)
                    break;

            }
            if (hasDoorError || hasWallError)
            {
                CreateTheRoom(type, pos);
            }
            else
            {
                Debug.Log("No error found..removing the Preview room and creating the actual room..." + RightRooms[RoomChoice]);
                if (RoomPreviewClone != null)
                    Destroy(RoomPreviewClone);
                Instantiate(RightRooms[RoomChoice], pos, Quaternion.identity);
            }
        }
      if(type == 4)
        {
            hasDoorError = false;
            hasWallError = false;
            if (RoomPreviewClone != null)
                Destroy(RoomPreviewClone);

            RoomChoice = Random.Range(0, LeftRooms.Length);

            RoomPreviewClone = Instantiate(LeftRoomsPreview[RoomChoice], pos, Quaternion.identity);
            for (int i = 0; i < RoomPreviewClone.transform.childCount; i++)
            {

                if (RoomPreviewClone.transform.GetChild(i).tag == "Door")
                {
                    Transform child = RoomPreviewClone.transform.GetChild(i);
                    Collider2D[] cols = Physics2D.OverlapCircleAll(child.position, 4f);
                    foreach (Collider2D col in cols)
                    {
                        if (col.transform.name.Contains("DoorWall"))
                        {
                            Debug.Log("Error found, Door: " + child.name + " is facing DoorWall: " + col.transform.name + " in Room:" + RoomPreviewClone.name);

                            hasDoorError = true;
                            break;
                        }


                    }
                }
                if (RoomPreviewClone.transform.GetChild(i).name.Contains("DoorWall"))
                {
                    Transform child = RoomPreviewClone.transform.GetChild(i);
                    Collider2D[] cols = Physics2D.OverlapCircleAll(child.position, 4f);
                    foreach (Collider2D col in cols)
                    {
                        if (col.transform.tag == "Door")
                        {
                            Debug.Log("Error found, Wall: " + child.name + " is facing Door: " + col.transform.name + " in Room:" + RoomPreviewClone.name);

                            hasWallError = true;
                            break;
                        }


                    }
                }

                if (hasDoorError || hasWallError)
                    break;

            }
            if (hasDoorError || hasWallError)
            {
                CreateTheRoom(type, pos);
            }
            else
            {
                Debug.Log("No error found..removing the Preview room and creating the actual room... "+ LeftRooms[RoomChoice].name);
                if (RoomPreviewClone != null)
                    Destroy(RoomPreviewClone);
                Instantiate(LeftRooms[RoomChoice], pos, Quaternion.identity);
            }
        }

    }

   
    
        
    
}
