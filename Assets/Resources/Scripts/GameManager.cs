using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Developer Mode")]
    public bool isDeveloperModeOn;
    public bool disallowBossToAttack;
    public bool hideMainMenu;

    [Header("Attributes")]
    public static bool HasWon = false;
    public float OpeningSpeed = 0.75f;
    public float goToGameSpeed = 2f;
    public float parallaxSpeed = 2f;

    [Header("Triggers")]
    public bool openingScreen = false;
    public bool isInCredits = false;
    public bool transitionToPlay = false;
    public bool parallaxIsOn = true;
    public bool runTimer = false;

    [Header("Parallax")]
    public GameObject parallaxGroup;
    public GameObject parallaxTrigger;
    public Animator parallaxAnimator;
    public GameObject[] cloudGroups;

    [Header("References")]
    public UnityEngine.UI.Slider BossRageSlider;
    public Animator inGameAnimator;
    public GameObject InGameOverlay;
    public GameObject DefeatOverlay;
    public GameObject VictoryOverlay;
    public GameObject MainCamera;
    public Menu MenuScript;
    public PlayerMovement_v2 playerMovement; // Player control script
    public Methods methodScript; // Script that is used in all animation events
    public BossBehaviour bossBehaviour;
    public Score scoreCounter;

    [Header("Background Parallax")]
    public GameObject[] bgSprite;
    public Transform pTransform;

    [Header("Audio")]
    public AudioSource VictoryAudio;
    public AudioSource DefeatAudio;
    public AudioSource BeepAudio;

    void Awake()
    {
        parallaxGroup.SetActive(true);
    }

    void Start ()
    {
        MainCamera.transform.position = new Vector3(0, 70f, MainCamera.transform.position.z);
        openingScreen = true;
        bossBehaviour.isInMenu = true;

        playerMovement.enabled = false;
    }
	
	void Update ()
    {
        #region Developer Mode
        if (isDeveloperModeOn)
        {
            if (hideMainMenu)
            {
                hideMainMenu = false;
                MenuScript.OnStartPressed();
                MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, 8.56f, MainCamera.transform.position.z);
                InGameOverlay.transform.GetChild(0).gameObject.SetActive(false);
                methodScript.OnPlayerControl();
            }
            if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftShift))
            {
                //UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
            else if (Input.GetKeyDown(KeyCode.Y) && Input.GetKey(KeyCode.LeftShift))
            {
                MenuScript.OnStartPressed();
                InGameOverlay.GetComponent<Animator>().enabled = false;
                InGameOverlay.SetActive(true); // Set InGameOverlay to true
                parallaxGroup.SetActive(false); // Set ParallaxGroup to false
                parallaxTrigger.SetActive(false); // Set ParallaxTrigger to false
                Vector3 ScenePosition = new Vector3(MainCamera.transform.position.x, 8.56f, MainCamera.transform.position.z);
                MainCamera.transform.position = ScenePosition;
                InGameOverlay.transform.GetChild(0).gameObject.SetActive(false);
                methodScript.OnPlayerControl();
            }
            else if (Input.GetKeyDown(KeyCode.V) && Input.GetKey(KeyCode.LeftShift))
            {
                bossBehaviour.bossHealth -= 500f;
            }
            else if (Input.GetKeyDown(KeyCode.B) && Input.GetKey(KeyCode.LeftShift))
            {
                bossBehaviour.bossCurRage = bossBehaviour.bossMaxRage;
            }
            else if (Input.GetKeyDown(KeyCode.N)&& Input.GetKey(KeyCode.LeftShift))
            {
                playerMovement.playerHealth = 0;
            }
        }
        #endregion

        #region Background Parallax
        bgSprite[0].transform.position = new Vector3(-pTransform.position.x * 0.01f, bgSprite[0].transform.position.y, bgSprite[0].transform.position.z);
        bgSprite[1].transform.position = new Vector3(-pTransform.position.x * 0.02f, bgSprite[0].transform.position.y, bgSprite[0].transform.position.z);
        bgSprite[2].transform.position = new Vector3(-pTransform.position.x * 0.03f, bgSprite[0].transform.position.y, bgSprite[0].transform.position.z);
        bgSprite[4].transform.position = new Vector3(-pTransform.position.x * 0.03f, bgSprite[0].transform.position.y, bgSprite[0].transform.position.z);
        bgSprite[5].transform.position = new Vector3(-pTransform.position.x * 0.01f, bgSprite[0].transform.position.y, bgSprite[0].transform.position.z);
        bgSprite[6].transform.position = new Vector3(pTransform.position.x * 0.01f, bgSprite[0].transform.position.y, bgSprite[0].transform.position.z);

        #endregion

        BossRageSlider.value = Mathf.Lerp(BossRageSlider.value, bossBehaviour.bossCurRage, Time.deltaTime * 5f);

        if (runTimer)
        {
            scoreCounter.timeSpent += Time.deltaTime;
        }

        if (parallaxIsOn)
        {
            cloudGroups[0].transform.position += Vector3.up * Time.deltaTime * parallaxSpeed;
            cloudGroups[1].transform.position += Vector3.up * Time.deltaTime * (parallaxSpeed + 1f);
        }

        if (openingScreen)
        {
            Vector3 MenuPosition = new Vector3(MainCamera.transform.position.x, 36.5f, MainCamera.transform.position.z);
            MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, MenuPosition, Time.deltaTime * OpeningSpeed);

            if (MainCamera.transform.position.y < 40)
            {
                if (!isInCredits)
                {
                    MenuScript.menuAnimator.SetBool("Start", true);
                }
            }

            if (Vector3.Distance(MainCamera.transform.position, MenuPosition) <= 0.1f)
            {
                MainCamera.transform.position = MenuPosition;
                openingScreen = false;
            }
        }

        //if (transitionToPlay) // Allow override of Transitions
        //{
        //    Vector3 ScenePosition = new Vector3(MainCamera.transform.position.x, 8.56f, MainCamera.transform.position.z);
        //    MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, ScenePosition, Time.deltaTime * goToGameSpeed);

        //    if (Vector3.Distance(MainCamera.transform.position, ScenePosition) <= 0.05f)
        //    {
        //        MainCamera.transform.position = ScenePosition;

        //        InGameOverlay.SetActive(true); // Set InGameOverlay to true
        //        inGameAnimator.SetTrigger("CountDown");
        //        BeepAudio.Play();

        //        parallaxGroup.SetActive(false); // Set ParallaxGroup to false
        //        parallaxTrigger.SetActive(false); // Set ParallaxTrigger to false

        //        transitionToPlay = false;
        //    }
        //}
    }
}
