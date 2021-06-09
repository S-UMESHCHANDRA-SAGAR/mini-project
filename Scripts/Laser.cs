using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed=8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //translate laser up    
        transform.Translate(Vector3.up*_speed*Time.deltaTime);//moving laser up at 8 meters per second
        if(transform.position.y>=8f)//if laser position is greater than 8 on y
        {
            
            Destroy(this.gameObject);//destroy the object
        }
    }
}