using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    [Header("Scenes")]
    public GameObject MenuUI;
    public GameObject CreditScene;

    [Header("References")]
    public Animator menuAnimator;
    public GameManager gameManager;

    public void OnStartPressed()
    {
        menuAnimator.SetBool("Exit", true);
        gameManager.openingScreen = false;
        gameManager.parallaxAnimator.SetTrigger("FadeOut");
        gameManager.parallaxIsOn = false;
        //gameManager.transitionToPlay = true; // Set TransitionToPlay = true
    }

    public void OnQuitPressed()
    {
        Application.Quit(); // Exit the application
    }
    
    public void OnCreditPressed()
    {
        gameManager.isInCredits = true;
        menuAnimator.enabled = false;
        MenuUI.SetActive(false);
        CreditScene.SetActive(true); // Go to CreditScene
    }

    public void OnReturnPressed()
    {
        gameManager.isInCredits = false;
        menuAnimator.enabled = true;
        MenuUI.SetActive(true);
        CreditScene.SetActive(false); // Return to the previous scene
    }

    public void OnRestartPressed()
    {
        GameManager.HasWon = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name); // Restart the game
    }

    public void LoadGame()
    {
        Vector3 ScenePosition = new Vector3(gameManager.MainCamera.transform.position.x, 8.56f, gameManager.MainCamera.transform.position.z);
        gameManager.MainCamera.transform.position = ScenePosition;

        gameManager.InGameOverlay.SetActive(true); // Set InGameOverlay to true
        gameManager.inGameAnimator.SetTrigger("CountDown");

        gameManager.parallaxGroup.SetActive(false); // Set ParallaxGroup to false
        gameManager.parallaxTrigger.SetActive(false); // Set ParallaxTrigger to false
    }
}
