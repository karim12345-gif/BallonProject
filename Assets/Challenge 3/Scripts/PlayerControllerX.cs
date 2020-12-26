using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver=true;
    public bool riseballon = true;


    private float lowEnough = 0.60f;
    private float pushForce = 0.5f;
    public float floatForce = 13.0f; // for the force 
    public float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;  // for the particles which we have to attach to the player 
    public ParticleSystem fireworksParticle; // also particle which we have to attach to the player 

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip ballBounce; // the sound will make when it hits the ground 


    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier; // for the gravitt of the player or ballon 
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 2, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver)  // when the game is not over then this works perctefly  , so now the ballon will float up 
        {
            playerRb.AddForce(Vector3.up * floatForce ,ForceMode.Impulse); // this is going to add force to the ballon to go up 
        }


        // bounds 
        if(transform.position.y < lowEnough  && !gameOver && riseballon )
       {
           playerRb.AddForce(Vector3.up*2 , ForceMode.Impulse);
          
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("oh shit , Game Over!");
            Destroy(other.gameObject); // other is , the other game objects that the ballon will hit 
           
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            gameOver = false;
            Debug.Log("moneyyyyyyyyy, gang!");
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject); // destory what ever the game object collides with 
           
        }

        // trying to add boucne to the ballon when it colldies with the ground 
        if (other.gameObject.CompareTag("Ground") && !gameOver)
       {

           playerRb.MovePosition(Vector3.up * pushForce);
           
            playerAudio.PlayOneShot(ballBounce,1.0f);

        }

    }

}
