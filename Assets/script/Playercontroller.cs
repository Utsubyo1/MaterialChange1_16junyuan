using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    //Declaration only -start
    float speed = 10.0f;
    float maxlimit = 20.0f;
    float gravityModifuer = 2.5f;
    float jumpforce = 15.0f;  
    bool isOnground = true;
    //private int max_Jump = 0;
    Rigidbody playerRb;
    Renderer playerRbr;

    public Material[] playerMlrs;

    //Declaration only -end
    // Start is called before the first frame update
    void Start()
    {
        isOnground = true;
        Physics.gravity *= gravityModifuer;
        playerRb = GetComponent<Rigidbody>();
        playerRbr = GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float verticalinput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");


        //Move Player (Gameobject) according to user interaction
        transform.Translate(Vector3.forward * Time.deltaTime * verticalinput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);
        //prevent forward monent out-of-bound
        if (transform.position.z < -maxlimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -maxlimit);
            playerRbr.material.color = playerMlrs[2].color;
        }
        //prevent forward moment out-of-bound
        else if (transform.position.z > maxlimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxlimit);
            playerRbr.material.color = playerMlrs[3].color;
        }
        
        //prevent left moment out-of-bound
        if (transform.position.x < -maxlimit)
        {
            Debug.Log("Left Bound Reached");
            transform.position = new Vector3(-maxlimit, transform.position.y, transform.position.z);
            playerRbr.material.color = playerMlrs[4].color;
        }
        //prevent right moment out-of-bound
        else if (transform.position.x > maxlimit)
        {
            Debug.Log("Right Bound Reached");
            transform.position = new Vector3(maxlimit, transform.position.y, transform.position.z);
            playerRbr.material.color = playerMlrs[5].color;
        }
        Playerjump();
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        { 
            isOnground = true;
            playerRbr.material.color = playerMlrs[1].color;
            
        }
        

    }
    private void Playerjump()
    {
       if (Input.GetKeyDown(KeyCode.Space) && isOnground)
        {

            
            playerRb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            isOnground = false;

            playerRbr.material.color = playerMlrs[0].color;
        }
     
    }
}
