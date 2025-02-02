using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject _object;
    [SerializeField]
    private Transform[] Images;
    [SerializeField]
    private GameObject winText;
    public static bool youWin;
    
        void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        winText.SetActive(false);
        youWin = false;

       
    }

    // Update is called once per frame
    public void Update()
    {
        // Check if all Images' rotations are at 0
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
            youWin = true;
            Cursor.lockState = CursorLockMode.Locked;
            StartCoroutine(WinSequence());
        }
    }

    private IEnumerator WinSequence()
    {
        yield return new WaitForSeconds(2f);  // Wait for 2 seconds
        winText.SetActive(true);  // Activate win text
        Destroy(_object);  // Destroy the _object after delay

       

        yield return new WaitForSeconds(4f);  // Wait for 4 seconds before changing the scene

        // Unload the current scene
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);

        // Load the "Envi" scene
        SceneManager.LoadScene("Envir1", LoadSceneMode.Single);

        // Wait until the new scene is fully loaded
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Envir");

        // After the scene is loaded, find the player object and restore its position
       
    }
}
