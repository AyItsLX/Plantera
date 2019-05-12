using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour {

    public enum BossPhase
    {
        STATICMODE,
        PHASE1,
        PHASE2,
        PHASE3,
        PHASE4,
    };

    public enum BossState
    {
        STATICIDLE,
        IDLE,
        SIDESWING,
        BACKSWING,
        REVERSED,
        BOTHSWING,
        SHOOTPEA,
    };

    [Header("Boss Phase")]
    public BossPhase PhaseState;

    [Header("Boss Behaviour State")]
    public BossState BehaviourState;

    [Header("Boss Stats")]
    public float bossHealth = 100;
    public float bossCurRage = 0;
    public float rageMultiplier = 2f;
    public float attackSpeedMultiplier = 0.5f;
    public float peaSpeedMultipler = 1f;

    public bool isBossRaging = false;

    [Header("Boss Attribute")]
    public float bossHitEffectSpeed = 2f; // Default is 2

    [Header("Limits")]
    public float bossMaxRage = 100;
    public float timeBetweenAttack = 5;

    [Header("References")]
    public bool isInMenu = true;

    #region References
    public Animator BodyAnimator;
    public Image DarkRS;
    public Image DarkLS;
    public Image DarkLBS;
    public Image DarkRBS;
    public Image LightRS;
    public Image LightLS;
    public Image LightLBS;
    public Image LightRBS;
    public Animator R_SideSwing;
    public Animator L_SideSwing;
    public Animator R_BackSwing;
    public Animator L_BackSwing;
    public GameObject R_ShooterPivot;
    public Animator R_PeaShooter;
    public GameObject L_ShooterPivot;
    public Animator L_PeaShooter;
    public SpriteRenderer BossBody;

    public AudioSource R_BackAudio;
    public AudioSource L_BackAudio;
    public AudioSource R_SideAudio;
    public AudioSource L_SideAudio;
    public AudioClip deathAudio;

    public GameObject Player;
    public GameManager gameManager;
    public Score scoreScript;
    public RageIncrement rageIncrement;
    #endregion

    void Start ()
    {
        LookForNextState(PhaseState, BehaviourState);
    }

    void Update()
    {
        #region Phase Change
        if (bossHealth <= 1500 && bossHealth > 1000)
        {
            if (!rageIncrement.cornerDetected)
            {
                rageMultiplier = 4f; // Increase to rage faster
            }
            attackSpeedMultiplier = 0.8f; // Increase to go faster
            peaSpeedMultipler = 0.9f; // Decrease to go faster
            PhaseState = BossPhase.PHASE2;
            BodyAnimator.SetInteger("Phase", 1);
        }
        else if (bossHealth <= 1000 && bossHealth > 500)
        {
            if (!rageIncrement.cornerDetected)
            {
                rageMultiplier = 6f; // Increase to rage faster
            }
            attackSpeedMultiplier = 1.1f; // Increase to go faster
            peaSpeedMultipler = 0.7f; // Decrease to go faster
            PhaseState = BossPhase.PHASE3;
            BodyAnimator.SetInteger("Phase", 2);
        }
        else if(bossHealth <= 500 && bossHealth > 0)
        {
            if (!rageIncrement.cornerDetected)
            {
                rageMultiplier = 8f; // Increase to rage faster
            }
            attackSpeedMultiplier = 1.4f; // Increase to go faster
            peaSpeedMultipler = 0.5f; // Decrease to go faster
            PhaseState = BossPhase.PHASE4;
            BodyAnimator.SetInteger("Phase", 3);
        }
        else if(bossHealth <= 0)
        {
            // Play Boss Death Animation
            StartCoroutine(BossDying());
        }
        #endregion

        #region Rage Mode
        if (bossCurRage >= bossMaxRage)
        {
            BehaviourState = BossState.IDLE;
            isBossRaging = true;
            StopAllCoroutines();
        }
        else if (!isInMenu && !isBossRaging)
        {
            bossCurRage += Time.deltaTime * rageMultiplier;
        }

        if (isBossRaging)
        {
            bossCurRage -= Time.deltaTime * 20;

            StartCoroutine(RageModeOn());

            if (bossCurRage <= 0)
            {
                isBossRaging = false;
                BehaviourState = BossState.IDLE;
                PhaseState = BossPhase.PHASE1;
                LookForNextState(PhaseState, BehaviourState); // Return Phase's Idle
            }
        }
        #endregion
    }

    #region Ready Mode

    #region Switches
    void LookForNextState(BossPhase _PhaseState, BossState _BehaviourState)
    {
        switch (_PhaseState)
        {
            case BossPhase.STATICMODE:
                switch (_BehaviourState)
                {
                    case BossState.STATICIDLE:
                        StartCoroutine(StaticIdle());
                        break;
                }
                break;
            case BossPhase.PHASE1:
                switch (_BehaviourState)
                {
                    case BossState.IDLE:
                        StartCoroutine(Idling());
                        break;
                    case BossState.SIDESWING:
                        StartCoroutine(SideSwing());
                        break;
                    case BossState.BACKSWING:
                        StartCoroutine(BackSwing());
                        break;
                    case BossState.SHOOTPEA:
                        StartCoroutine(ShootPea());
                        break;
                }
                break;
            case BossPhase.PHASE2:
                switch (_BehaviourState)
                {
                    case BossState.IDLE:
                        StartCoroutine(Idling());
                        break;
                    case BossState.SIDESWING:
                        StartCoroutine(SideSwing());
                        break;
                    case BossState.BACKSWING:
                        StartCoroutine(BackSwing());
                        break;
                    case BossState.REVERSED:
                        StartCoroutine(SideAndBackReversed());
                        break;
                    case BossState.SHOOTPEA:
                        StartCoroutine(ShootPea());
                        break;
                }
                break;
            case BossPhase.PHASE3:
                switch (_BehaviourState)
                {
                    case BossState.IDLE:
                        StartCoroutine(Idling());
                        break;
                    case BossState.SIDESWING:
                        StartCoroutine(SideSwing());
                        break;
                    case BossState.BACKSWING:
                        StartCoroutine(BackSwing());
                        break;
                    case BossState.REVERSED:
                        StartCoroutine(SideAndBackReversed());
                        break;
                    case BossState.BOTHSWING:
                        StartCoroutine(SideOrBackSwingBoth());
                        break;
                    case BossState.SHOOTPEA:
                        StartCoroutine(ShootPea());
                        break;
                }
                break;
            case BossPhase.PHASE4:
                switch (_BehaviourState)
                {
                    case BossState.IDLE:
                        StartCoroutine(Idling());
                        break;
                    case BossState.SIDESWING:
                        StartCoroutine(SideSwing());
                        break;
                    case BossState.BACKSWING:
                        StartCoroutine(BackSwing());
                        break;
                    case BossState.REVERSED:
                        StartCoroutine(SideAndBackReversed());
                        break;
                    case BossState.BOTHSWING:
                        StartCoroutine(SideOrBackSwingBoth());
                        break;
                    case BossState.SHOOTPEA:
                        StartCoroutine(ShootPea());
                        break;
                }
                break;
        }
    }
    #endregion

    #region Actions
    IEnumerator Idling()
    {
        //Phase1_Ani.SetBool("Idle", true); // Play idle animation and be passive
        yield return new WaitForSeconds(timeBetweenAttack); // Run a timer to 5 to channel a random attack
        if (PhaseState == BossPhase.PHASE1)
        {
            int[] randomize = { 2, 3, 6 };
            int state = randomize[Random.Range(0, 3)];
            //Debug.Log("Phase1Idle : " + state);
            BehaviourState = (BossState)state;
            LookForNextState(PhaseState, BehaviourState);
        }
        else if (PhaseState == BossPhase.PHASE2)
        {
            int[] randomize = { 2, 3, 4, 6 };
            int state = randomize[Random.Range(0, 4)];
            //Debug.Log("Phase2Idle : " + state);
            BehaviourState = (BossState)state;
            LookForNextState(PhaseState, BehaviourState);
        }
        else if (PhaseState == BossPhase.PHASE3)
        {
            int[] randomize = { 2, 3, 4, 5, 6 };
            int state = randomize[Random.Range(0, 5)];
            //Debug.Log("Phase3Idle : " + state);
            BehaviourState = (BossState)state;
            LookForNextState(PhaseState, BehaviourState);
        }
        else if (PhaseState == BossPhase.PHASE4)
        {
            int[] randomize = { 2, 3, 4, 5, 6 };
            int state = randomize[Random.Range(0, 5)];
            //Debug.Log("Phase4Idle : " + state);
            BehaviourState = (BossState)state;
            LookForNextState(PhaseState, BehaviourState);
        }
    }

    IEnumerator SideSwing() // Start SideSwing from left or right depending on player position
    {
        if (Player.transform.position.x < 0)
        {
            DarkLS.gameObject.SetActive(true); 
            LightLS.gameObject.SetActive(true);
            LightLS.fillAmount = 0;
            while (LightLS.fillAmount < 1)
            {
                LightLS.fillAmount += Time.deltaTime * attackSpeedMultiplier;
                yield return null;
            }
            LightLS.gameObject.SetActive(false);
            DarkLS.gameObject.SetActive(false);
            L_SideAudio.Play();
            L_SideSwing.SetTrigger("L_SS"); // Play left SideSwing animation
        }
        else if (Player.transform.position.x > 0)
        {
            DarkRS.gameObject.SetActive(true); 
            LightRS.gameObject.SetActive(true);
            LightRS.fillAmount = 0;
            while (LightRS.fillAmount < 1)
            {
                LightRS.fillAmount += Time.deltaTime * attackSpeedMultiplier;
                yield return null;
            }
            LightRS.gameObject.SetActive(false);
            DarkRS.gameObject.SetActive(false);
            R_SideAudio.Play();
            R_SideSwing.SetTrigger("R_SS"); // Play right SideSwing animation
        }
        yield return new WaitForSeconds(timeBetweenAttack);
        LookForNextState(PhaseState, BossState.IDLE); // Return Phase's Idle
    }

    IEnumerator BackSwing() // Start BackSwing from left or right depending on player position
    {
        if (Player.transform.position.x < 0)
        {
            DarkLBS.gameObject.SetActive(true);
            LightLBS.gameObject.SetActive(true);
            LightLBS.fillAmount = 0;
            while (LightLBS.fillAmount < 1)
            {
                LightLBS.fillAmount += Time.deltaTime * attackSpeedMultiplier;
                yield return null;
            }
            LightLBS.gameObject.SetActive(false);
            DarkLBS.gameObject.SetActive(false);
            L_BackAudio.Play();
            L_BackSwing.SetTrigger("L_BS"); // Play left BackSwing animation
        }
        else if (Player.transform.position.x > 0)
        {
            DarkRBS.gameObject.SetActive(true);
            LightRBS.gameObject.SetActive(true);
            LightRBS.fillAmount = 0;
            while (LightRBS .fillAmount < 1)
            {
                LightRBS.fillAmount += Time.deltaTime * attackSpeedMultiplier;
                yield return null;
            }
            LightRBS.gameObject.SetActive(false);
            DarkRBS.gameObject.SetActive(false);
            R_BackAudio.Play();
            R_BackSwing.SetTrigger("R_BS"); // Play right BackSwing animation
        }
        yield return new WaitForSeconds(timeBetweenAttack);
        LookForNextState(PhaseState, BossState.IDLE); // Return Phase's Idle
    }

    IEnumerator SideOrBackSwingBoth()
    {
        int randomizer = Random.Range(0, 2);

        if (randomizer == 0) // Both BackSwing play at the same time
        {
            DarkLBS.gameObject.SetActive(true);
            LightLBS.gameObject.SetActive(true);
            DarkRBS.gameObject.SetActive(true);
            LightRBS.gameObject.SetActive(true);
            LightLBS.fillAmount = 0; 
            LightRBS.fillAmount = 0;
            while (LightLBS.fillAmount < 1 && LightRBS.fillAmount < 1)
            {
                LightRBS.fillAmount += Time.deltaTime * attackSpeedMultiplier;
                LightLBS.fillAmount += Time.deltaTime * attackSpeedMultiplier;
                yield return null;
            }
            DarkLBS.gameObject.SetActive(false);
            LightLBS.gameObject.SetActive(false);
            DarkRBS.gameObject.SetActive(false);
            LightRBS.gameObject.SetActive(false);
            L_BackAudio.Play();
            R_BackAudio.Play();
            L_BackSwing.SetTrigger("L_BS");
            R_BackSwing.SetTrigger("R_BS");
        }
        else if (randomizer == 1) // Both SideSwing play at the same time
        {
            DarkLS.gameObject.SetActive(true);
            LightLS.gameObject.SetActive(true);
            DarkRS.gameObject.SetActive(true);
            LightRS.gameObject.SetActive(true);
            LightLS.fillAmount = 0;
            LightRS.fillAmount = 0;
            while (LightRS.fillAmount < 1 && LightLS.fillAmount < 1)
            {
                LightLS.fillAmount += Time.deltaTime * attackSpeedMultiplier;
                LightRS.fillAmount += Time.deltaTime * attackSpeedMultiplier;
                yield return null;
            }
            LightRS.gameObject.SetActive(false);
            DarkRS.gameObject.SetActive(false);
            LightLS.gameObject.SetActive(false);
            DarkLS.gameObject.SetActive(false);
            L_SideAudio.Play();
            R_SideAudio.Play();
            L_SideSwing.SetTrigger("L_SS");
            R_SideSwing.SetTrigger("R_SS");
        }
        yield return new WaitForSeconds(timeBetweenAttack);
        LookForNextState(PhaseState, BossState.IDLE); // Return Phase's Idle
    }

    IEnumerator SideAndBackReversed()
    {
        if (Player.transform.position.x < 0)
        {
            DarkLBS.gameObject.SetActive(true);
            LightLBS.gameObject.SetActive(true);
            DarkRS.gameObject.SetActive(true);
            LightRS.gameObject.SetActive(true);
            LightLBS.fillAmount = 0;
            LightRS.fillAmount = 0;
            while (LightRS.fillAmount < 1 && LightLBS.fillAmount < 1)
            {
                LightLBS.fillAmount += Time.deltaTime * attackSpeedMultiplier;
                LightRS.fillAmount += Time.deltaTime * attackSpeedMultiplier;
                yield return null;
            }
            DarkLBS.gameObject.SetActive(false);
            LightLBS.gameObject.SetActive(false);
            DarkRS.gameObject.SetActive(false);
            LightRS.gameObject.SetActive(false);
            L_BackAudio.Play();
            R_SideAudio.Play();
            L_BackSwing.SetTrigger("L_BS");
            R_SideSwing.SetTrigger("R_SS");
        }
        else if (Player.transform.position.x > 0)
        {
            DarkRBS.gameObject.SetActive(true);
            LightRBS.gameObject.SetActive(true);
            DarkLS.gameObject.SetActive(true);
            LightLS.gameObject.SetActive(true);
            LightRBS.fillAmount = 0;
            LightLS.fillAmount = 0;
            while (LightRBS.fillAmount < 1 && LightLS.fillAmount < 1)
            {
                LightLS.fillAmount += Time.deltaTime * attackSpeedMultiplier;
                LightRBS.fillAmount += Time.deltaTime * attackSpeedMultiplier;
                yield return null;
            }
            DarkRBS.gameObject.SetActive(false);
            LightRBS.gameObject.SetActive(false);
            DarkLS.gameObject.SetActive(false);
            LightLS.gameObject.SetActive(false);
            R_BackAudio.Play();
            L_SideAudio.Play();
            R_BackSwing.SetTrigger("R_BS");
            L_SideSwing.SetTrigger("L_SS");
        }
        yield return new WaitForSeconds(timeBetweenAttack);
        LookForNextState(PhaseState, BossState.IDLE); // Return Phase's Idle
    }

    IEnumerator ShootPea() // Start ShootPea random from left or right
    {
        int randomizer = Random.Range(0, 2);

        if (randomizer == 0)
        {
            L_PeaShooter.SetTrigger("L_PS"); // Play left ShootPea animation
            yield return new WaitForSeconds(peaSpeedMultipler);
            R_PeaShooter.SetTrigger("R_PS"); // Play right ShootPea animation
            yield return new WaitForSeconds(peaSpeedMultipler);
        }
        else if (randomizer == 1)
        {
            R_PeaShooter.SetTrigger("R_PS"); // Play right ShootPea animation
            yield return new WaitForSeconds(peaSpeedMultipler);
            L_PeaShooter.SetTrigger("L_PS"); // Play left ShootPea animation
            yield return new WaitForSeconds(peaSpeedMultipler);
        }
        yield return new WaitForSeconds(timeBetweenAttack);
        LookForNextState(PhaseState, BossState.IDLE); // Return Phase's Idle
    }

    IEnumerator RageModeOn()
    {
        DarkLBS.gameObject.SetActive(false);
        LightLBS.gameObject.SetActive(false);
        DarkRS.gameObject.SetActive(false);
        LightRS.gameObject.SetActive(false);
        DarkRBS.gameObject.SetActive(false);
        LightRBS.gameObject.SetActive(false);
        DarkLS.gameObject.SetActive(false);
        LightLS.gameObject.SetActive(false);
        while (isBossRaging)
        {
            L_PeaShooter.SetTrigger("L_PS"); // Play left ShootPea animation
            R_PeaShooter.SetTrigger("R_PS"); // Play right ShootPea animation
            yield return new WaitForSeconds(0.5f);
        }
    }
    #endregion

    #endregion

    #region Static Mode
    IEnumerator StaticIdle()
    {
        while (isInMenu)
        {
            //Phase1_Ani.SetBool("Idle", true);
            //L_SideSwing.SetBool("Idle", true);
            //R_SideSwing.SetBool("Idle", true);
            //L_BackSwing.SetBool("Idle", true);
            //R_BackSwing.SetBool("Idle", true);
            yield return null;
        }

        if (!gameManager.disallowBossToAttack)
        {
            BehaviourState = BossState.IDLE;
            PhaseState = BossPhase.PHASE1;
            LookForNextState(PhaseState, BehaviourState);
        }
        yield return null;
    }
    #endregion

    IEnumerator BossDying()
    {
        BodyAnimator.SetInteger("Phase", 4);
        yield return new WaitForSeconds(1);
        GetComponent<AudioSource>().PlayOneShot(deathAudio);
        GameManager.HasWon = true;
        Player.GetComponent<PlayerMovement_v2>().enabled = false;
        gameManager.BossRageSlider.gameObject.SetActive(false);
        gameManager.VictoryAudio.Play();
        gameManager.InGameOverlay.GetComponent<Animator>().SetTrigger("FadeOut"); // Play FadeOut InGameOverlay
        gameManager.VictoryOverlay.GetComponent<Animator>().SetTrigger("Victory");
        UpdateScore();
        Destroy(gameObject);
    }

    public void HitSound()
    {
        GetComponent<AudioSource>().pitch = Random.Range(1, 3);
        GetComponent<AudioSource>().Play();
    }

    public void OnBodyHit()
    {
        BossBody.color = new Color(1, 0, 0, 0); // Change Color & Alpha
        BossBody.gameObject.transform.parent.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1); // Upscale the object
        StartCoroutine(BodyHit());
    }

    IEnumerator BodyHit() // Punch, Fade & Damage effect
    {
        float fadeTime = 0;
        float scaleTime = 1.2f;

        while (BossBody.color.a <= 1 || BossBody.gameObject.transform.parent.gameObject.transform.localScale.x > 1.02f)
        {
            fadeTime += Time.deltaTime * bossHitEffectSpeed;
            BossBody.color = new Color(1, fadeTime, fadeTime, fadeTime);

            if (BossBody.gameObject.transform.parent.gameObject.transform.localScale.x > 1.02f)
            {
                scaleTime -= Time.deltaTime * bossHitEffectSpeed * 0.5f;
                BossBody.gameObject.transform.parent.gameObject.transform.localScale = new Vector3(scaleTime, scaleTime, 1);
            }

            yield return null;
        }

        if (BossBody.gameObject.transform.parent.gameObject.transform.localScale.x < 1)
        {
            BossBody.gameObject.transform.parent.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void UpdateScore()
    {
        gameManager.runTimer = false;
        scoreScript.curBossHealth = bossHealth;
        scoreScript.healthLeft = (int)Player.GetComponent<PlayerMovement_v2>().playerHealth;
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
