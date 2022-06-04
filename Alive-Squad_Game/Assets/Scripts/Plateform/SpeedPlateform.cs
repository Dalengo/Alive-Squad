using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPlateform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.CompareTag("Player"))
        {
            PlayerMovement playermov = col.transform.GetComponent<PlayerMovement>();
            playermov.moveSpeed = 800;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.transform.CompareTag("Player"))
        {
            PlayerMovement playermov = col.transform.GetComponent<PlayerMovement>();
            playermov.moveSpeed = 400;
        }
    
    }
}
