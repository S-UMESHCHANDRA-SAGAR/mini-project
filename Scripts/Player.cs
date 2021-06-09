using System.Collections;//.net libraries
using System.Collections.Generic;
using UnityEngine;//unity libraries
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour//Monobehaviour is a class which allows us to drag and drop scripts and behaviours to game objects and it run start and update fns.
{
    [SerializeField]//serializefield allows the developers to read and overwrite the private variables in the inspector tab.
    private float _speed=3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate=0.5f;
    private float _cantFire=-1f;
    [SerializeField]
    private int  _lives=3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private int _score;
    private UIManager _uiManager;
    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;

    [SerializeField] GameObject crashVFX;
    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject mainMenuButton;
    [SerializeField] GameObject resetHighScoreButton;

    //[SerializeField] Button restatButton;
    //SerializeField] Button mainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        //restatButton.interactable =false;
        //mainMenuButton.interactable=false;
        restartButton.SetActive(false);//disables the restart button 
        mainMenuButton.SetActive(false);//disables the mainmenu button
        resetHighScoreButton.SetActive(false);//disables the resetHighScoreButton button
        //take the current position= new position(0,0,0);
        transform.position=new Vector3(0,0,0);//assigning new vector3 position
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();//accessing SpawnManager component of gameobject Spawn_Manager and storing it in _spwanManager
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();//accessing UIManager component of gameobject Canvas and storing it in _uiManager
        _audioSource=GetComponent<AudioSource>();//accessing AudioSource component and storing in _audioSource
        
        _audioSource.clip=_laserSoundClip;//Assigning laserSoundClip to audioSource clip
    
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();//calling calculatemovement
        FireLaser();//calling firelaser
        
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");//storing horizontal inputs in horizontalInput to move left and right
        float verticalInput = Input.GetAxis("Vertical");//storing vertical inputs in verticalInput to move up and down
        //if the player doesn't move then the horizontalInput will be 0, it moves to right by pressing d then horizontalInput value is 1,moves left by press a and value -1.
        //new Vector3(1,0,0)per frame it moves 1 meter right and for 1 sec it moves 60meters, to change it and move 1meter per 1 sec we use Time.
        //transform.Translate(Vector3.right*5*Time.deltaTime);moving 5 meters per sec.
        
        transform.Translate(new Vector3(horizontalInput,verticalInput,0)*_speed*Time.deltaTime);//using transform,Translate we change the position of the gameobjects
        

        if(transform.position.y>=0)//if player position on the y is greater than 0
        {
            transform.position=new Vector3(transform.position.x,0,0);//then assigning y position to 0
        }
        else if(transform.position.y<=-3.8f)//else if position on the y is less than -3.8f
        {
            transform.position=new Vector3(transform.position.x,-3.8f,0);//then assigning y position to -3.8f
        }
        //transform.position=new Vector3(transform.position.x,Mathf.Clamp(transform.position.y,-3.8f,0),0);alternate way  min=-3.8f, max=0

        
        if(transform.position.x>=9.08f)//if player position on the x is greater than 10.27
        {
            transform.position=new Vector3(9.08f,transform.position.y,0);//then assigning x position to 10.27
        }
        else if(transform.position.x<=-9.08f)//else if position on the x is less than -10.27
        {
        transform.position=new Vector3(-9.08f,transform.position.y,0);//then assigning x position to -10.27
        }
    }

    void FireLaser()
    {
        
        if(Input.GetKeyDown(KeyCode.Space)&&Time.time>_cantFire)//if we hit space key
        {
            _cantFire=Time.time+_fireRate;//instantiate method is used to clone gameobjects
            Instantiate(_laserPrefab,new Vector3(transform.position.x,transform.position.y+1.5f,0),Quaternion.identity);//spawns laser gameObject at player position with default rotation
            _audioSource.Play();//playing laser sound
        }
    }

    public void Damage()
    {
        _lives--;//decrementing player lives

        _uiManager.UpdateLives(_lives);
        if(_lives<1)//player died
            {
                //communicate with spawnmanager
                //and let them know to stop spawning
                _spawnManager.OnPlayerDeath();
                KillPlayer();
                
            }
    }
    void KillPlayer()
    {
        Instantiate(crashVFX,transform.position,Quaternion.identity);//spawns crashVFC when player dies
        Destroy(this.gameObject);//destroying player if lives are less than 0
        //restatButton.interactable =true;
        //mainMenuButton.interactable=true;
        restartButton.SetActive(true);//enables the restart button
        mainMenuButton.SetActive(true);//enables the mainmenu button
        resetHighScoreButton.SetActive(true);//disables the resetHighScoreButton button
        Invoke("ReloadLevel",0.5f);
        //SceneManager.LoadScene(0);
    }
    void ReloadLevel()
    {
        SceneManager.LoadScene(0);//loads main scene
    }

    public void AddScore()
    {
        _score += 10;//incrementing score if laser collide with the enemy
        _uiManager.UpdateScore(_score);
    }
}