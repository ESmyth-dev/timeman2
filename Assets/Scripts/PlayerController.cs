using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
//5using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
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
    private bool blinkReady;

    // Audio stuff
    private GameObject audioManagers;

    private AudioClip timeSlowAudioClip;
    private GameObject slowTimeAudioManager;
    private AudioSource slowTimeAudioSource;

    private AudioClip blinkAudioClip;
    private GameObject blinkAudioManager;
    private AudioSource blinkAudioSource;

    private AudioClip pewAudioClip;
    private GameObject pewAudioManager;
    private AudioSource pewAudioSource;

    private GameObject PostProcessVolumeObject;
    private PostProcessVolume postProcessVolume;


    // Start is called before the first frame update
    void Start()
    {
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
        blinkAudioSource = slowTimeAudioManager.GetComponent<AudioSource>();

        pewAudioClip = Resources.Load<AudioClip>("Audio/Pew");
        pewAudioManager = audioManagers.transform.Find("PewAudioManager").gameObject;
        pewAudioSource = pewAudioManager.GetComponent<AudioSource>();

        PostProcessVolumeObject = GameObject.Find("PostProcessVolume");
        postProcessVolume = PostProcessVolumeObject.GetComponent<PostProcessVolume>();
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

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("movingForwards", true);
            moveDirection += forward;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("movingForwards", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("movingRight", true);
            moveDirection += right;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("movingRight", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("movingLeft", true);
            moveDirection -= right;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("movingLeft", false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            BlinkAbility();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SlowTimeAbility();
        }

        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("movingBackwards", true);
            moveDirection -= forward;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("movingBackwards", false);
        }

        if (moveDirection != Vector3.zero)
        {
            transform.Translate(moveDirection * Time.deltaTime * speed, Space.World);
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            animator.SetBool("jumping", true);
        }else{
            if(Input.GetKeyDown(KeyCode.Space) && !isGrounded)
            {
                if(GameManager.instance.doubleJump){
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                    rb.AddForce((transform.up * jumpForce), ForceMode.Impulse);
                    isGrounded = false;
                    animator.SetBool("falling", false);
                    animator.SetBool("jumping", true);
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && !beamEnabled)
        {
            if (!overHeated)
            {
                slider.value += 0.2f;
                if (slider.value >= 1f)
                {
                    overHeated = true;                    
                } 
                GameObject go = Instantiate(shotPrefab, gun.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));
                //go.transform.rotation = transform.rotation;
                RaycastHit[] hits = Physics.RaycastAll(cam.transform.position, cam.transform.forward);
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].distance > 3)
                    {
                        go.transform.LookAt(hits[i].point);
                        break;
                    }
                }

                pewAudioSource.PlayOneShot(pewAudioClip);

            }
        }

        if (Input.GetMouseButton(0) && beamEnabled)
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

        if (Input.GetMouseButtonUp(0) && beamEnabled)
        {
            beamLine.enabled = false;
            beamLight.enabled = false;
        }


        if (Input.GetMouseButtonDown(1))
        {
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

    public void JumpEnd()
    {
        animator.SetBool("jumping", false);
        animator.SetBool("onGround", false);
        animator.SetBool("falling", true);
    }

    void OnCollisionStay()
    {
        if (!animator.GetBool("jumping"))
        {
            isGrounded = true;
            animator.SetBool("onGround", true);

        }
    }

    IEnumerator SlowTime()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);
        postProcessVolume.enabled = false;
        Time.timeScale *= slowdownFactor;
        speed /= slowdownFactor;
        animator.speed /= slowdownFactor;
        timeSlowed = false;

        // wait another 5 seconds to use the slow time ability again
        yield return new WaitForSeconds(5);
        Image slowAbilityBackground = GameObject.Find("SlowInactive").GetComponent<Image>();
        slowAbilityBackground.enabled = false;

    }

    void SlowTimeAbility()
    {
        if (!timeSlowed)
        {
            postProcessVolume.enabled = true;
            Time.timeScale /= slowdownFactor;
            speed *= slowdownFactor;
            animator.speed *= slowdownFactor;
            timeSlowed = true;

            Image slowAbilityBackground = GameObject.Find("SlowInactive").GetComponent<Image>();
            slowAbilityBackground.enabled = true;

            slowTimeAudioSource.PlayOneShot(timeSlowAudioClip);

            StartCoroutine(SlowTime());
        }
        
    }

    IEnumerator blinkCooldown()
    {
        yield return new WaitForSeconds(0.5f);
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
            if (Physics.Raycast(transform.position + transform.up * 0.5f, blinkVector, out RaycastHit hit, blinkDistance, LayerMask.GetMask("Level")))
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

    public void Hit()
    {
        if(GameManager.instance.numberOfLives> 0)
        {
            GameManager.instance.LoseLife();
            DeathRewind.instance.Rewind();
        }
        else
        {
            GameManager.instance.GameOver();
        }
    }
}

