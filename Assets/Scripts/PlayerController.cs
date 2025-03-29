using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
//5using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public string enemyPath = "CopyPasteRoom/Random Room/Preset1/Enemies";

    public Animator animator;
    public Slider slider;
    public LineRenderer beamLine;
    public float beamRange = 100f;
    public float cooldownSpeed = 0.2f;
    public float beamFillSpeed = 0.5f;
    private bool overHeated;
    public bool beamEnabled = false;
    public Light beamLight;
    public float speed = 1.0f;
    public float slowdownFactor = 10;
    public float blinkDistance = 5;
    public float jumpForce;
    public GameObject shotPrefab;
    public GameObject bombPrefab;
    public GameObject blinkSFX;
    public Camera cam;
    public Transform gun;
    private bool isGrounded;
    private Rigidbody rb;
    private bool timeSlowed;
    private Coroutine slowTimeCoroutine;
    private bool blinkReady;
    private bool babyBombReady = true;
    private float numberOfLives;
    private bool jumpEnd = false;
    private bool isRewinding = false;
    public GameManager gameManager;

    //List to hold the recorded positions
    public List<Vector3> recordedPositions = new List<Vector3>();
    //List to hold the recorded rotations
    public List<Quaternion> recordedRotations = new List<Quaternion>();

    // Cooldowns
    private float slowdownDurationSeconds = 1f;
    private float slowdownCooldownSeconds = 10f;
    private float blinkCooldownSeconds = 0.5f;
    private float babyBombCooldownSeconds = 5f;

    // Audio stuff
    private GameObject audioManagers;

    private AudioClip timeSlowAudioClip;
    private GameObject slowTimeAudioManager;
    private AudioSource slowTimeAudioSource;

    private AudioClip blinkAudioClip;
    private GameObject blinkAudioManager;
    private AudioSource blinkAudioSource;

    private AudioClip rewindAudioClip;
    private GameObject rewindAudioManager;
    private AudioSource rewindAudioSource;

    private AudioClip pewAudioClip;
    private GameObject pewAudioManager;
    private AudioSource pewAudioSource;

    private GameObject PostProcessVolumeObject;
    private PostProcessVolume postProcessVolume;
    private PostProcessProfile timeSlowProfile;
    private PostProcessProfile rewindProfile;

    // pause menu
    private bool pauseMenuActive;
    private float gameTimeScale;
    private Image pauseMenuBackground;


    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        pauseMenuActive = false;
        pauseMenuBackground = GameObject.Find("PauseMenuBackground").GetComponent<Image>();

=======
        gameManager = FindAnyObjectByType<GameManager>();
>>>>>>> e2a76e4a12ec5dab45e4036e1ff198d2f0eb1995
        numberOfLives = 3;
        overHeated = false;
        blinkReady = true;
        timeSlowed = false;
        animator.applyRootMotion = false;
        rb = GetComponent<Rigidbody>();

        slider = GameObject.Find("Slider").GetComponent<Slider>();

        // Audio stuff
        audioManagers = GameObject.Find("AudioManagers");

        timeSlowAudioClip = Resources.Load<AudioClip>("Audio/ZaWarudo");
        slowTimeAudioManager = audioManagers.transform.Find("SlowTimeAudioManager").gameObject;
        slowTimeAudioSource = slowTimeAudioManager.GetComponent<AudioSource>();

        blinkAudioClip = Resources.Load<AudioClip>("Audio/Blink");
        blinkAudioManager = audioManagers.transform.Find("BlinkAudioManager").gameObject;
        blinkAudioSource = blinkAudioManager.GetComponent<AudioSource>();

        rewindAudioClip = Resources.Load<AudioClip>("Audio/rewind");
        rewindAudioManager = audioManagers.transform.Find("RewindAudioManager").gameObject;
        rewindAudioSource = rewindAudioManager.GetComponent<AudioSource>();

        pewAudioClip = Resources.Load<AudioClip>("Audio/Pew");
        pewAudioManager = audioManagers.transform.Find("PewAudioManager").gameObject;
        pewAudioSource = pewAudioManager.GetComponent<AudioSource>();

        PostProcessVolumeObject = GameObject.Find("PostProcessVolume");
        postProcessVolume = PostProcessVolumeObject.GetComponent<PostProcessVolume>();
        timeSlowProfile = Resources.Load<PostProcessProfile>("TimeSlowTint");
        rewindProfile = Resources.Load<PostProcessProfile>("RewindProfile");

        //Start recording positions
        StartCoroutine(RecordPositions());
    }

    // Update is called once per frame
    void Update()
    {
        slider.value -= cooldownSpeed * Time.deltaTime;
        if (slider.value <= 0.1f)
        {
            overHeated = false;
        }
        Vector3 moveDirection = Vector3.zero;
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        forward = Vector3.ProjectOnPlane(forward, Vector3.up).normalized;
        right = Vector3.ProjectOnPlane(right, Vector3.up).normalized;

        if (Input.GetKey(KeyCode.W) && !isRewinding)
        {
            animator.SetBool("movingForwards", true);
            moveDirection += forward;
        }

        if (Input.GetKeyUp(KeyCode.W) && !isRewinding)
        {
            animator.SetBool("movingForwards", false);
        }

        if (Input.GetKey(KeyCode.D) && !isRewinding)
        {
            animator.SetBool("movingRight", true);
            moveDirection += right;
        }

        if (Input.GetKeyUp(KeyCode.D) && !isRewinding)
        {
            animator.SetBool("movingRight", false);
        }

        if (Input.GetKey(KeyCode.A) && !isRewinding)
        {
            animator.SetBool("movingLeft", true);
            moveDirection -= right;
        }

        if (Input.GetKeyUp(KeyCode.A) && !isRewinding)
        {
            animator.SetBool("movingLeft", false);
        }

<<<<<<< HEAD
        if (Input.GetKeyDown(KeyCode.F) && !isRewinding && !pauseMenuActive)
        {
            BlinkAbility();
        }
        if (Input.GetKeyDown(KeyCode.C) && !isRewinding && !pauseMenuActive)
=======
        if (Input.GetKeyDown(KeyCode.F) && !isRewinding && gameManager.blink)
        {
            BlinkAbility();
        }
        if (Input.GetKeyDown(KeyCode.C) && !isRewinding && gameManager.slowTime)
>>>>>>> e2a76e4a12ec5dab45e4036e1ff198d2f0eb1995
        {
            SlowTimeAbility();
        }

        if (Input.GetKey(KeyCode.S) && !isRewinding)
        {
            animator.SetBool("movingBackwards", true);
            moveDirection -= forward;
        }

        if (Input.GetKeyUp(KeyCode.S) && !isRewinding)
        {
            animator.SetBool("movingBackwards", false);
        }

        // pause menu
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenuActive)
        {
            pauseMenuBackground.enabled = true;
            pauseMenuActive = true;
            gameTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuActive)
        {
            pauseMenuBackground.enabled = false;
            pauseMenuActive = false;
            Time.timeScale = gameTimeScale;
        }

        if (moveDirection != Vector3.zero)
        {
            transform.Translate(moveDirection * Time.deltaTime * speed, Space.World);
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isRewinding && !pauseMenuActive)
        {
            jumpEnd = false;
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            animator.SetBool("jumping", true);
        } else {
            if(Input.GetKeyDown(KeyCode.Space) && !isGrounded)
            {
                if(gameManager.doubleJump && jumpEnd == false){
                    jumpEnd = true;
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                    rb.AddForce((transform.up * jumpForce), ForceMode.Impulse);
                    isGrounded = false;
                    animator.SetBool("falling", false);
                    animator.SetBool("jumping", true);
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && !beamEnabled && !isRewinding && !pauseMenuActive)
        {
            if (!overHeated)
            {
                slider.value += 0.2f;
                if (slider.value >= 1f)
                {
                    overHeated = true;                    
                } 
                GameObject bullet = Instantiate(shotPrefab, gun.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));

                // set bullet's firer variable
                ShotCollision shotScript = bullet.GetComponent<ShotCollision>();
                shotScript.Firer = gameObject;

                RaycastHit[] hits = Physics.RaycastAll(cam.transform.position, cam.transform.forward);
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].distance > 3)
                    {
                        bullet.transform.LookAt(hits[i].point);
                        break;
                    }
                }

                pewAudioSource.PlayOneShot(pewAudioClip);

            }
        }

        if (Input.GetMouseButton(0) && beamEnabled && !isRewinding && !pauseMenuActive)
        {
            if (!overHeated)
            {
                slider.value += beamFillSpeed * Time.deltaTime;
                Debug.Log("Click");
                if (slider.value >= 1f)
                {
                    overHeated = true;
                }
                RaycastHit[] hits = Physics.RaycastAll(cam.transform.position, cam.transform.forward);
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].distance > 3)
                    {
                        beamLine.SetPosition(0, gun.position);
                        beamLine.SetPosition(1, hits[i].point); 
                        beamLight.transform.position = gun.position;
                        beamLight.transform.rotation = gun.rotation;
                        break;
                    }
                }
                beamLine.enabled = true;
                beamLight.enabled = true;
            }
        }


<<<<<<< HEAD
        if (Input.GetMouseButtonDown(1) && babyBombReady && !isRewinding && !pauseMenuActive)
=======
        if (Input.GetMouseButtonDown(1) && babyBombReady && !isRewinding && gameManager.timeGrenade)
>>>>>>> e2a76e4a12ec5dab45e4036e1ff198d2f0eb1995
        {
            babyBombReady = false;
            Image bombBackground = GameObject.Find("BombInactive").GetComponent<Image>();
            bombBackground.enabled = true;
            StartCoroutine(babyBombCooldown());

            GameObject bomb = Instantiate(bombPrefab, gun.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));
            RaycastHit[] hits = Physics.RaycastAll(cam.transform.position, cam.transform.forward);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].distance > 3)
                {
                    bomb.transform.LookAt(hits[i].point);
                    break;
                }
            }
        }
    }

    IEnumerator babyBombCooldown()
    {
        yield return new WaitForSeconds(babyBombCooldownSeconds);
        babyBombReady = true;
        Image bombBackground = GameObject.Find("BombInactive").GetComponent<Image>();
        bombBackground.enabled = false;
    }

    public void JumpEnd()
    {
        animator.SetBool("jumping", false);
        animator.SetBool("onGround", false);
        animator.SetBool("falling", true);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor")){
            if (!animator.GetBool("jumping"))
            {
                isGrounded = true;
                animator.SetBool("onGround", true);

            }
        }
    }

    IEnumerator SlowTime()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(slowdownDurationSeconds);

        // Unslow
        postProcessVolume.enabled = false;
        Time.timeScale *= slowdownFactor;
        //Time.fixedDeltaTime *= slowdownFactor;
        speed /= slowdownFactor;
        animator.speed /= slowdownFactor;
        
        // wait another 5 seconds to use the slow time ability again
        yield return new WaitForSeconds(slowdownCooldownSeconds);
        timeSlowed = false;
        Image slowAbilityBackground = GameObject.Find("SlowInactive").GetComponent<Image>();
        slowAbilityBackground.enabled = false;

    }

    void SlowTimeAbility()
    {
        Debug.Log("slowslow");
        if (!timeSlowed)
        {
            postProcessVolume.profile = timeSlowProfile;
            postProcessVolume.enabled = true;
            Time.timeScale /= slowdownFactor;
            //Time.fixedDeltaTime /= slowdownFactor;
            speed *= slowdownFactor;
            animator.speed *= slowdownFactor;
            timeSlowed = true;

            Image slowAbilityBackground = GameObject.Find("SlowInactive").GetComponent<Image>();
            slowAbilityBackground.enabled = true;

            slowTimeAudioSource.PlayOneShot(timeSlowAudioClip);

            slowTimeCoroutine = StartCoroutine(SlowTime());
        }
        
    }

    IEnumerator blinkCooldown()
    {
        yield return new WaitForSeconds(blinkCooldownSeconds);
        Image blinkBackground = GameObject.Find("BlinkInactive").GetComponent<Image>();
        blinkBackground.enabled = false;
        blinkReady = true;
    }

    void BlinkAbility()
    {
       
        Vector3 blinkVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            blinkVector += transform.forward;

        }
        if (Input.GetKey(KeyCode.S))
        {
            blinkVector -= transform.forward;

        }
        if (Input.GetKey(KeyCode.D))
        {
            blinkVector += transform.right;

        }
        if (Input.GetKey(KeyCode.A))
        {
            blinkVector -= transform.right;

        }

        if (blinkReady && blinkVector != Vector3.zero)
        {
            if (Physics.Raycast(transform.position + transform.up * 0.5f, blinkVector, out RaycastHit hit, blinkDistance, LayerMask.GetMask(new string[] {"LevelLineOfSight", "Level"})))
            {
                Debug.Log("Obstacle detected! shorter teleport");
                transform.position += blinkVector * (hit.distance - 1.5f);
            }
            else
            {
                transform.position += blinkVector * blinkDistance;
            }

            if (blinkVector != Vector3.zero)
            {
                Quaternion effectRotation = Quaternion.LookRotation(blinkVector, Vector3.up);

                Vector3 effectVector = transform.position;  // position on player after 
                effectVector.y += 1f;

                Instantiate(blinkSFX, effectVector, effectRotation);
            }
            Image blinkBackground = GameObject.Find("BlinkInactive").GetComponent<Image>();
            blinkBackground.enabled = true;
            blinkReady = false;
            blinkAudioSource.PlayOneShot(blinkAudioClip);
            StartCoroutine(blinkCooldown());
        }
        

    }
    //This records the players position and rotation, then pauses for 1 sec, and then runs again
    private IEnumerator RecordPositions(){
        Debug.Log("Recording positions started");
        while(true)
        {
            Debug.Log("Recording positions");
            //Triggers if positions list is full, and removes the oldest one (also rotation)
            if(recordedPositions.Count > 5){
                recordedPositions.RemoveAt(0);
                recordedRotations.RemoveAt(0);
            }

            //Adds new positions/rotations to their lists
            recordedPositions.Add(transform.position);
            recordedRotations.Add(transform.rotation);

            //Waits 1 sec
            yield return new WaitForSeconds(1f);
        }
    }

    //This will be called when the player dies, and just sets the player to the position/rotation from 5 secs ago
    public void Rewind()
{
        if (recordedPositions.Count <= 0)
        {
            Debug.LogError("No recorded positions to rewind to. How did you mess this up?");
            return;
        }

        isRewinding = true;

        // Cancel slowtime if rewinding
        if (timeSlowed && slowTimeCoroutine != null)
        {
            StopCoroutine(slowTimeCoroutine);

            // Unslow
            timeSlowed = false;
            postProcessVolume.enabled = false;
            Time.timeScale *= slowdownFactor;
            //Time.fixedDeltaTime *= slowdownFactor;
            speed /= slowdownFactor;
            animator.speed /= slowdownFactor;
        }

        rewindAudioSource.PlayOneShot(rewindAudioClip);
        postProcessVolume.profile = rewindProfile;
        postProcessVolume.enabled = true;
        SetEnemyBehaviour(false);      

        StopCoroutine(RecordPositions()); // Stop any existing coroutines
        StartCoroutine(SmoothRewind());
    }

    private IEnumerator SmoothRewind()
    {
        Debug.Log("Rewinding...");

        // Iterate backward through recorded positions
        for (int i = recordedPositions.Count - 1; i >= 0; i--)
        {
            Vector3 startPos = transform.position;
            Quaternion startRot = transform.rotation;
            Vector3 targetPos = recordedPositions[i];
            Quaternion targetRot = recordedRotations[i];

            float duration = 0.6f; // Adjust rewind speed
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);
                transform.rotation = Quaternion.Lerp(startRot, targetRot, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null; // Wait for next frame
            }

            // Ensure final snap to exact position
            transform.position = targetPos;
            transform.rotation = targetRot;
        }

        // Clear recorded positions after rewind
        recordedPositions.Clear();
        recordedRotations.Clear();

        postProcessVolume.enabled = false;
        StartCoroutine(EnableEnemyBehaviourAfterDelay());
        isRewinding = false;

        Debug.Log("Rewind complete.");
        StartCoroutine(RecordPositions()); // Restart recording positions
    }

    private IEnumerator EnableEnemyBehaviourAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        SetEnemyBehaviour(true);
    }


    public void Hit()
    {
        //sends a log message to terminal 
        Debug.Log("Player has been hit");

        if(numberOfLives> 0)
        {
            if(GameManager.instance.deathBubble){
                // Instantiate the death bubble prefab at the player's position
                GameObject deathBubble = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            }
            numberOfLives--;

            if (!isRewinding)
            {
                Rewind();
            }
        }
        else
        {
            GameManager.instance.GameOver();
        }
    }

    private void SetEnemyBehaviour(bool value)
    {
        Transform enemiesParent = GameObject.Find(enemyPath)?.transform;

        if (enemiesParent == null)
        {
            Debug.LogError("Could not find the Enemies folder at path: " + enemyPath);
            return;
        }

        List<GameObject> enemyObjects = GetChildren(enemiesParent);

        foreach (GameObject enemy in enemyObjects)
        {
            EnemyBehaviour behaviour = enemy.GetComponent<EnemyBehaviour>();
            if (behaviour != null)
            {
                behaviour.BehaviourEnabled = value;
            }

            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
            if (agent != null && value == false)
            {
                agent.SetDestination(enemy.transform.position);
            }
            else if (agent != null && value == true)
            {
                behaviour.ChooseNewDestination();
            }
        }
    }

    private List<GameObject> GetChildren(Transform parent)
    {
        List<GameObject> result = new List<GameObject>();

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            result.Add(child.gameObject);
        }

        return result;
    }
}

