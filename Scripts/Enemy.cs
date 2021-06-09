using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed=5.5f;
    [SerializeField] GameObject deathFX;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player=GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.down*_speed*Time.deltaTime);//enemy move down at 4 meters per second
        if(transform.position.y<=-6)//if bottom of screen
        {
            transform.position=new Vector3(Random.Range(-9.08f,9.08f),7,0);//respawning enemy at top with a new random x position
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag=="Player")//if other tag is player
        {
            Player player=other.transform.GetComponent<Player>();//getting Player components of other gameobject
            if(player!=null) //checking player component exist or not
            {
                player.Damage();//damaging the player
            }
            //or
            //other.transform.GetComponent<Player>().Damage();
            Instantiate(deathFX,transform.position,Quaternion.identity);//spawns deathFX when enemy dies
            Destroy(this.gameObject);//destroying us(enemy)
        }

        
        if(other.tag=="Laser")//if other tag is laser
        {
            Destroy(other.gameObject);//destroying laser
            if(_player!=null)
            {
                _player.AddScore();//adding score
            }
            Instantiate(deathFX,transform.position,Quaternion.identity);//spawns deathFX when enemy dies
            Destroy(this.gameObject);//destroying us(enemy)
        }
    }
}