<<<<<<< Updated upstream
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< Updated upstream
<<<<<<< Updated upstream
public class GameController : MonoBehaviour
{
    [SerializeField]
    private Transform[] Images;  // Fixed array name (was Image[])
    [SerializeField]
    private GameObject winText;  // Fixed missing reference for winText
    public static bool youWin;

    // Start is called before the first frame update
    void Start()
    {
        winText.SetActive(false);  // Corrected winText reference
        youWin = false;
    }

    // Update is called once per frame
     public void Update()
    {
        // Fixed the condition to check the rotations properly
        if (Images[0].rotation.z == 0 &&
            Images[1].rotation.z == 0 &&
            Images[2].rotation.z == 0 &&
            Images[3].rotation.z == 0 &&
            Images[4].rotation.z == 0 &&
            Images[5].rotation.z == 0 &&
            Images[6].rotation.z == 0 &&
            Images[7].rotation.z == 0 &&
            Images[8].rotation.z == 0 &&
            Images[9].rotation.z == 0 &&
            Images[10].rotation.z == 0 &&
            Images[11].rotation.z == 0)
        {
            // Set youWin to true (assuming you had this variable for the win condition)
             youWin = true;
            winText.SetActive(true);  // Activate win text
        }
=======
=======
>>>>>>> Stashed changes
public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    }
}
