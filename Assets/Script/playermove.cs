using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playermove : MonoBehaviour
{
    public int keys = 0;
    public float speed = 5.0f;
    public Text keyAmount;  // Display the current key count
    public Text youWin;     // Display the win message
    public GameObject gate; // The gate that gets destroyed when the player collects 3 keys
    private bool gameWon = false; // Flag to prevent multiple "You Win!!!" messages

    // Start is called before the first frame update
    void Start()
    {
        // Make sure the win message is initially empty
        youWin.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        // Player movement controls
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }

        // Check if player has collected 3 keys and destroy the gate
        if (keys == 3 && gate != null)
        {
            Destroy(gate); // Destroy the gate when 3 keys are collected
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player touches a regular key
        if (collision.gameObject.tag == "key")
        {
            keys++; // Increase the key count
            keyAmount.text = "Keys: " + keys; // Update the key UI
            Destroy(collision.gameObject); // Destroy the key after collection
        }

        // If the player touches the final key, display the win message
        if (collision.gameObject.tag == "finalkey" && !gameWon)
        {
            gameWon = true; // Prevent multiple win messages
            youWin.text = "You Win!!!"; // Show the win message
        }

        // Reset the level if the player hits an enemy
        if (collision.gameObject.tag == "enemy")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
        }

        // Prevent player from moving through walls (bounce off or stop movement)
        if (collision.gameObject.tag == "walls")
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(speed * Time.deltaTime, 0, 0); // Prevent left movement
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(-speed * Time.deltaTime, 0, 0); // Prevent right movement
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(0, -speed * Time.deltaTime, 0); // Prevent upward movement
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(0, speed * Time.deltaTime, 0); // Prevent downward movement
            }
        }
    }
}
