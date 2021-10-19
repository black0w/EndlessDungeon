using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterMovement : MonoBehaviour
{

   
    Rigidbody2D rb; //Responsible for 2D Physics
     public int Damage = 10;
     public int PlayerLevel; //Responsible for adjusting the Enemy Difficulty
    private int LastPlayerLevel; //Included for debugging will be removed in the future
    //We use [SerializeField] to see the private variable in the editor
    [SerializeField]
    private float MovementSpeed = 5f; //The movement Speed of our Player
    
    public Sprite[] PlayerHeadSprites;  //Array of Sprites holding our Player's Sprites
    //[0] - Look down
    //[1] - Look up
    //[2] - Look left
    //[3] - Look right

    public GameObject PlayerBody; //Because the Head of the player and the body are separated GameObjects
                                 //so we connect the body to this script so we can access its Animator Component
    
    public GameObject FireBall; //Connecting the "Fireball" GameObject Prefab so we can Instantiate it and 
                               //access its Components
    private bool canShoot = true;
    public GameObject DamageText;
    SpriteRenderer BodySpriteRednerer; //Player's Body Sprite Renderer
   
    float MoveX;
    float MoveY;
    GameObject FireBallClone; //We use this GameObject prefix to assing the FireBall Object when we instantiate it
                              //Otherwise we wont have access to it when its instantiated


    RoomGenerator roomGenerator;
    //The start function which activates whenever we start the Application
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //We connect the Object rb to the Player's Componnent Rigidbody2D
       
        BodySpriteRednerer = GetComponent<SpriteRenderer>(); //WE connect the Object BodySpriteRenderer to the Player's Componnent SpriteRenderer

        roomGenerator = GameObject.Find("Rooms").GetComponent<RoomGenerator>();

        PlayerLevel = 1;
        LastPlayerLevel = PlayerLevel;
    }

    // Update is called once per frame
    private void Update()
    {
        //For Debugging properties
        if(PlayerLevel != LastPlayerLevel)
        {
            Debug.Log(PlayerLevel.ToString());
            LastPlayerLevel = PlayerLevel;
        }

       
     Shooting();  
    }
    
    //Handling the Player's Shooting
    private void Shooting()
    {
         if (canShoot)
        {
            if (Input.GetKey(KeyCode.UpArrow)) //Getting The Input from Keyboard
            {
                FireBallClone = Instantiate(FireBall, transform.position, Quaternion.identity); //Creating new FireBall GameOBject
                                                                                                //from the Prefab to a certain Position and Rotation 

                FireBallClone.GetComponent<FireBallScript>().origin = "Player";

                FireBallClone.GetComponent<FireBallScript>().dir = Vector2.up; //We call the FireBall's variable "dir" and give it Vector2.up
                                                                               //property which is equal to (X,Y)=(0,1);
                
                BodySpriteRednerer.flipX = false;   //We set the SpriteRenderer Propery "flipX" to false  
                BodySpriteRednerer.sprite = PlayerHeadSprites[1]; 
           
                Destroy(FireBallClone, 0.6f); //We destroy the FireBallClone Object, that we previously instantiated, after 0.6 seconds
                StartCoroutine(ShootCoroutine());   //We start Coroutine to Delay the Shooting so we dont Shoot all the time and have
                                                    //fire rate
                

            }else
            if (Input.GetKey(KeyCode.DownArrow))
            {
                FireBallClone = Instantiate(FireBall, transform.position, Quaternion.identity);
                FireBallClone.GetComponent<FireBallScript>().origin = "Player";
                FireBallClone.GetComponent<FireBallScript>().dir = Vector2.down;
                BodySpriteRednerer.flipX = false;
                BodySpriteRednerer.sprite = PlayerHeadSprites[0];
               
                Destroy(FireBallClone, 0.6f);
                StartCoroutine(ShootCoroutine());
             

            }else
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                FireBallClone = Instantiate(FireBall, transform.position, Quaternion.identity);
                FireBallClone.GetComponent<FireBallScript>().origin = "Player";
                FireBallClone.GetComponent<FireBallScript>().dir = Vector2.left;
                BodySpriteRednerer.flipX = true;
                BodySpriteRednerer.sprite = PlayerHeadSprites[2];
               
                Destroy(FireBallClone, 0.6f);
                StartCoroutine(ShootCoroutine());
               

            }else
            if (Input.GetKey(KeyCode.RightArrow))
            {
                FireBallClone = Instantiate(FireBall, transform.position, Quaternion.identity);
                FireBallClone.GetComponent<FireBallScript>().origin = "Player";
                FireBallClone.GetComponent<FireBallScript>().dir = Vector2.right;
                BodySpriteRednerer.flipX = false;
                BodySpriteRednerer.sprite = PlayerHeadSprites[3];
               
                Destroy(FireBallClone, 0.6f);
                StartCoroutine(ShootCoroutine());

            }
            else
            {

                StartCoroutine(PutDelay());
            }
        }
        
    }

    //Updating on a scale of time
   private void FixedUpdate()
    {
        
        Movement();
        MovementAnimations();

    }

    //Handling the Movement of the Player
    private void Movement()
    {
         MoveX = Input.GetAxis("Horizontal"); 
         MoveY = Input.GetAxis("Vertical"); //We get the Input Axis from the AWDS Buttons of the keyboard
                                                // and assing them to MoveX and MoveY, from these axis we get Floats from 0 to 1 

        Vector3 Movement = new Vector2(MoveX, MoveY); //We create Vector3 Movement from the MoveX & MoveY axis we took earlier
      
        Movement = Movement * MovementSpeed * Time.deltaTime;//We multiply the movement Vector to our MovementSpeed 
                                                            //and Time.deltaTime(which is resposinble to have stable movement
                                                            //independent from the FPS we have)

        rb.MovePosition(transform.position + Movement); //We move our player using its RigidBody2D function 'MovePosition'
                                                        //via adding out new Movement Vector to the Player's Current Position
    }
    
    //Handling the Animations of the Player 
    private void MovementAnimations()
    {
          if(MoveX < 0)
        {
            PlayerBody.GetComponent<Animator>().SetBool("allOff",false);
              PlayerBody.GetComponent<Animator>().SetBool("MoveRight",false);
              PlayerBody.GetComponent<Animator>().SetBool("MoveDown",false);
             PlayerBody.GetComponent<Animator>().SetBool("MoveUp",false);
                PlayerBody.GetComponent<Animator>().SetBool("MoveLeft",true);
        }
       else if(MoveX > 0)
        {
            PlayerBody.GetComponent<Animator>().SetBool("allOff",false);
            PlayerBody.GetComponent<Animator>().SetBool("MoveLeft",false);
            PlayerBody.GetComponent<Animator>().SetBool("MoveDown",false);
             PlayerBody.GetComponent<Animator>().SetBool("MoveUp",false);
            PlayerBody.GetComponent<Animator>().SetBool("MoveRight",true);
        }
        else if(MoveY > 0)
        {
            PlayerBody.GetComponent<Animator>().SetBool("allOff",false);
            PlayerBody.GetComponent<Animator>().SetBool("MoveLeft",false);
            PlayerBody.GetComponent<Animator>().SetBool("MoveRight",false);
             PlayerBody.GetComponent<Animator>().SetBool("MoveDown",false);
             PlayerBody.GetComponent<Animator>().SetBool("MoveUp",true);
            
        }
        else if(MoveY < 0)
        {
              PlayerBody.GetComponent<Animator>().SetBool("allOff",false);
             PlayerBody.GetComponent<Animator>().SetBool("MoveLeft",false);
            PlayerBody.GetComponent<Animator>().SetBool("MoveRight",false);
             PlayerBody.GetComponent<Animator>().SetBool("MoveUp",false);
             PlayerBody.GetComponent<Animator>().SetBool("MoveDown",true);

        }
        else
        {
           PlayerBody.GetComponent<Animator>().SetBool("MoveRight",false);
           PlayerBody.GetComponent<Animator>().SetBool("MoveLeft",false);
             PlayerBody.GetComponent<Animator>().SetBool("MoveDown",false);
             PlayerBody.GetComponent<Animator>().SetBool("MoveUp",false);
             PlayerBody.GetComponent<Animator>().SetBool("allOff",true);
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
        yield return new WaitForSeconds(0.6f);

        canShoot = true;
        isShooting = false;
           
    }

    //We use this boolean so we're sure that the Coroutine "PutDelay" is being activated only once
    bool Delay = false;

    //Putting delay for Animating the Player's Head
    //IEnumerator is used in "Coroutines" and repeats unless we stop it
    IEnumerator PutDelay()
    {
        if (Delay)
            yield break;

        Delay = true;
        
        //Waiting for 1 second before the next step
        yield return new WaitForSeconds(1f);

        BodySpriteRednerer.sprite = PlayerHeadSprites[0];
      
        Delay = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.name);
        if(collision.transform.tag == "Door")
        {
            if(collision.transform.name.Contains("Up"))
            {
                roomGenerator.CreateRoomUp();
            }
            if(collision.transform.name.Contains("Down"))
            {
                roomGenerator.CreateRoomDown();
            }
            if(collision.transform.name.Contains("Left"))
            {
                roomGenerator.CreateRoomLeft();
            }
            if (collision.transform.name.Contains("Right"))
            {
                roomGenerator.CreateRoomRight();
            }
        }

        if(collision.transform.tag == "PowerUp")
        {
            Debug.Log("Damage Boosted to 30");
            Damage = 30;
            Destroy(collision.gameObject);
            StartCoroutine(ResetDamage());
        }
    }

        IEnumerator ResetDamage()
        {
            DamageText.GetComponent<TextMesh>().text = "Damage: "+Damage;
            StartCoroutine(showDamageText());
            yield return new WaitForSeconds(15f);       
            Damage = 10;
             DamageText.GetComponent<TextMesh>().text = "Damage: "+Damage;
             StartCoroutine(showDamageText());
             Debug.Log("Damage Reset");
        }

       
        IEnumerator showDamageText()
        {
            DamageText.GetComponent<Animator>().SetBool("doOnce",true);
            yield return new WaitForSeconds(0.1f);
              DamageText.GetComponent<Animator>().SetBool("doOnce",false);
        }
    
}
