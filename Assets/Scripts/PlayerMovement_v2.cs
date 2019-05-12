using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_v2 : MonoBehaviour {

    [Header("PlayerStats")]
    public float playerHealth = 3;
    public float damage = 10;
    public float walkSpeed;
    public float dashSpeed;
    public float waitTime = 3;
    public bool facingRight = true;

    [Header("References")]
    Rigidbody2D rb;
    public GameObject[] heartPrefab;
    public GameObject dashEffect;
    public GameObject dustCloud;
    public GameObject deathParticle;
    public GameObject minusHealthPrefab;

    public AudioClip[] audioClipList;
    public AudioClip[] popHit;
    public AudioSource audioSource;
    public AudioSource hitAudio;
    public AudioSource popHitAudio;

    public Animator charaAnimator;

    public GameManager gameManager;
    public BossBehaviour bossBehaviour;
    public BlinkEffect blinkEffect;

    [Header("Jump")]
    public bool isGrounded = true;
    public float jumpForce;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    [Header("Dash AD")]
    public bool dashIsInCoolDown = false;
    public float dashCoolDown = 0;
    public float dashMaxCoolDown = 0.5f;
    //float leftTotal = 0;
    //float leftTimeDelay = 0;
    //float rightTotal = 0;
    //float rightTimeDelay = 0;

    //[Header("Dash Up")]
    ////public float upTotal = 0;
    ////public float upTimeDelay = 0;

    [Header("Invincible")]
    public GameObject invincibleMode;
    public bool isInInvincible = false;

    [Header("Shooting")]
    public GameObject playerProjectile;
    //public float timeBtwAttack;
    //public float startTimeBtwAttack = 0.3f;
    public float curCoolDown = 0f;
    public float maxCoolDOwn = 0.1f;
    public bool isCoolDown = false;
    public Transform spawnPosition;
    //public bool fairyBullet = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
        dashCoolDown = dashMaxCoolDown;
        curCoolDown = maxCoolDOwn;
        //dashTime = startdashTime;
    }

	void Update ()
    {
        #region Movement and Jump 
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = Vector2.up * jumpForce;
            Instantiate(dustCloud, transform.position + new Vector3(0, 0, -1), Quaternion.identity);
            audioSource.clip = audioClipList[0];
            audioSource.Play();
        }
        if (!dashIsInCoolDown && Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.Space))
        {
            dashIsInCoolDown = true;
            audioSource.PlayOneShot(audioClipList[1]);
            Instantiate(dashEffect, transform.position + new Vector3(0, 0, -1), Quaternion.identity);
            rb.velocity = Vector2.up * (dashSpeed * 2);
        }
        //if (!dashIsInCoolDown && Input.GetKeyDown(KeyCode.W))
        //{
        //    upTotal += 1;
        //}
        //if (upTotal == 1 && upTimeDelay < 1f)
        //{
        //    upTimeDelay += Time.deltaTime;
        //}
        //if (upTotal == 1 && upTimeDelay >= 1f)
        //{
        //    upTimeDelay = 0;
        //    upTotal = 0;
        //}
        //if (!dashIsInCoolDown && upTotal == 2 && upTimeDelay < 1f)
        //{
        //    dashIsInCoolDown = true;
        //    dashCoolDown = dashMaxCoolDown;
        //    audioSource.PlayOneShot(audioClipList[1]);
        //    Instantiate(dashEffect, transform.position + new Vector3(0, 0, -1), Quaternion.identity);
        //    rb.velocity = Vector2.up * (dashSpeed * 2);
        //    upTotal = 0;
        //}
        //if (upTotal == 2 && upTimeDelay >= 1f)
        //{
        //    upTimeDelay = 0;
        //    upTotal = 0;
        //}

        else if (!Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
        {
            charaAnimator.SetBool("Walk", true);
            facingRight = false;
            transform.position += Vector3.left * Time.deltaTime * walkSpeed;
            if (!dashIsInCoolDown && Input.GetKeyDown(KeyCode.Space))
            {
                dashIsInCoolDown = true;
                audioSource.PlayOneShot(audioClipList[1]);
                Instantiate(dashEffect, transform.position + new Vector3(0, 0, -1), Quaternion.identity);
                rb.velocity = new Vector3(-1, 0.5f, 0) * (dashSpeed * 1.5f);
                //leftTotal += 1;
            }


            //if (leftTotal == 1 && leftTimeDelay < 1f)
            //    leftTimeDelay += Time.deltaTime * 3f;

            //if (leftTotal == 1 && leftTimeDelay >= 1f)
            //{
            //    leftTimeDelay = 0;
            //    leftTotal = 0;
            //}
            //if (!dashIsInCoolDown && leftTotal == 2 && leftTimeDelay < 1f)
            //{
            //    dashIsInCoolDown = true;
            //    dashCoolDown = dashMaxCoolDown;
            //    audioSource.PlayOneShot(audioClipList[1]);
            //    Instantiate(dashEffect, transform.position + new Vector3(0, 0, -1), Quaternion.identity);
            //    rb.velocity = new Vector3(-1, 0.5f, 0) * (dashSpeed * 1.5f);
            //    leftTotal = 0;
            //}
            //if (leftTotal == 2 && leftTimeDelay >= 1f)
            //{
            //    leftTimeDelay = 0;
            //    leftTotal = 0;
            //}
        }
        else if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            charaAnimator.SetBool("Walk", true);
            facingRight = true;
            transform.position += Vector3.right * Time.deltaTime * walkSpeed;
            if (!dashIsInCoolDown && Input.GetKeyDown(KeyCode.Space))
            {
                dashIsInCoolDown = true;
                audioSource.PlayOneShot(audioClipList[1]);
                Instantiate(dashEffect, transform.position + new Vector3(0, 0, -1), Quaternion.identity);
                rb.velocity = new Vector3(1, 0.5f, 0) * (dashSpeed * 1.5f);
                //rightTotal += 1;
            }

            //if (rightTotal == 1 && rightTimeDelay < 1f)
            //    rightTimeDelay += Time.deltaTime * 3f;

            //if (rightTotal == 1 && rightTimeDelay >= 1f)
            //{
            //    rightTimeDelay = 0;
            //    rightTotal = 0;
            //}
            //if (!dashIsInCoolDown && rightTotal == 2 && rightTimeDelay < 1f)
            //{
            //    dashIsInCoolDown = true;
            //    dashCoolDown = dashMaxCoolDown;
            //    audioSource.PlayOneShot(audioClipList[1]);
            //    Instantiate(dashEffect, transform.position + new Vector3(0, 0, -1), Quaternion.identity);
            //    rb.velocity = new Vector3(1, 0.5f, 0) * (dashSpeed * 1.5f);
            //    rightTotal = 0;
            //}
            //if (rightTotal == 2 && rightTimeDelay >= 1f)
            //{
            //    rightTimeDelay = 0;
            //    rightTotal = 0;
            //}
        }
        else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            charaAnimator.SetBool("Walk", false);
        }

        if (dashIsInCoolDown)
        {
            dashCoolDown -= Time.deltaTime;
            if (dashCoolDown <= 0)
            {
                dashCoolDown = dashMaxCoolDown;
                dashIsInCoolDown = false;
            }
        }
        #endregion

        #region Health
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainScene")
        {
            if (playerHealth >= 3)
            {
                heartPrefab[2].SetActive(true);
                heartPrefab[1].SetActive(true);
                heartPrefab[0].SetActive(true);
            }
            else if (playerHealth >= 2)
            {
                heartPrefab[2].SetActive(false);
                heartPrefab[1].SetActive(true);
                heartPrefab[0].SetActive(true);
            }
            else if (playerHealth >= 1)
            {
                heartPrefab[2].SetActive(false);
                heartPrefab[1].SetActive(false);
                heartPrefab[0].SetActive(true);
            }
            else if (playerHealth <= 0) // Player will be dead when zero health
            {
                heartPrefab[2].SetActive(false);
                heartPrefab[1].SetActive(false);
                heartPrefab[0].SetActive(false);
                gameManager.DefeatAudio.Play();
                Instantiate(deathParticle, transform.position, Quaternion.identity);
                Instantiate(deathParticle, transform.position, Quaternion.identity);
                gameManager.BossRageSlider.gameObject.SetActive(false);
                gameManager.InGameOverlay.GetComponent<Animator>().SetTrigger("FadeOut"); // Play FadeOut InGameOverlay
                gameManager.DefeatOverlay.GetComponent<Animator>().SetTrigger("Defeat");
                bossBehaviour.UpdateScore();
                Destroy(gameObject);
            }
        }
        #endregion

        #region Projectile
        if (isCoolDown)
        {
            curCoolDown -= Time.deltaTime;
            if (curCoolDown <= 0)
            {
                isCoolDown = false;
            }
        }

        if (!isCoolDown && Input.GetKey(KeyCode.Mouse0))
        {
            isCoolDown = true;
            curCoolDown = maxCoolDOwn;
            if (facingRight)
            {
                popHitAudio.PlayOneShot(popHit[Random.Range(0, popHit.Length)]);
                GameObject bullet = Instantiate(playerProjectile, spawnPosition.position, spawnPosition.rotation);
                bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * 1, bullet.transform.localScale.y, bullet.transform.localScale.z);
                bullet.GetComponent<pProjectile>().SetDirection(spawnPosition.forward, this);

                //if (fairyBullet == true)
                //{
                //    GameObject bullet2 = Instantiate(playerProjectile, transform.GetChild(10).transform.position, spawnPosition.rotation);
                //    bullet2.transform.localScale = new Vector3(bullet.transform.localScale.x * 1, bullet.transform.localScale.y, bullet.transform.localScale.z);
                //    bullet2.GetComponent<pProjectile>().SetDirection(spawnPosition.forward, this);
                //}
                //else
                //{
                //    fairyBullet = false;
                //}
            }
            else if (!facingRight)
            {
                popHitAudio.PlayOneShot(popHit[Random.Range(0, popHit.Length)]);
                GameObject bullet = Instantiate(playerProjectile, spawnPosition.position, spawnPosition.rotation);
                bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * -1, bullet.transform.localScale.y, bullet.transform.localScale.z);
                bullet.GetComponent<pProjectile>().SetDirection(spawnPosition.forward, this);

                //if ((fairyBullet == true))
                //{
                //    GameObject bullet2 = Instantiate(playerProjectile, transform.GetChild(10).transform.position, spawnPosition.rotation);
                //    bullet2.transform.localScale = new Vector3(bullet.transform.localScale.x * -1, bullet.transform.localScale.y, bullet.transform.localScale.z);
                //    bullet2.GetComponent<pProjectile>().SetDirection(spawnPosition.forward, this);
                //}
                //else
                //{
                //    fairyBullet = false;
                //}
            }
        }
        #endregion

        #region Invicible
        if (isInInvincible) // Invincible Statement
        {
            blinkEffect.startBlinking = true;
            invincibleMode.SetActive(true);
            waitTime -= Time.deltaTime;

            if (waitTime <= 0)
            {
                blinkEffect.startBlinking = false;
                isInInvincible = false;
                invincibleMode.SetActive(false);
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                waitTime = 3;
            }
        }
        #endregion
    }

    #region Look Left or Right
    void LateUpdate()
    {
        Vector3 localScale = transform.localScale;

        if (((facingRight) && (localScale.x > 0)) || ((!facingRight) && (localScale.x < 0)))
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;
    }
    #endregion

    public void SpawnHitEffect()
    {
        Instantiate(deathParticle, transform.position + new Vector3(1.5f, 0.5f, 0), Quaternion.identity);
        Instantiate(minusHealthPrefab, transform.position + new Vector3(1.5f, 0.5f, 0), Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            invincibleMode.SetActive(true);
        }
    }
}
