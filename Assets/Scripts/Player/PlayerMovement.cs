using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : Singleton<PlayerMovement>
{
    [Header("Player Values")]
    [SerializeField] private float headCheckDis;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float sprint = 1.4f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 8f;

    [Space]

    [Header("Player Data")]
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform headCheck;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private GameObject Gun;
    [SerializeField] private Slider staminaSlider;

    private CharacterController controller;
    private Animator anim;
    private bool headCheckClear = true;
    private bool canSprint = true;
    private bool sprinting = false;
    [HideInInspector] public float sprintValue = 500f;
    private bool isCrouching = false;
    private int sprintLength = 200;
    Vector3 velocity;
    bool isgrounded;
    private float nextTimeToAdd = 0;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        SprintSlideUpdate();
        if (PlayerPrefs.GetInt("PlayerProLevel") >= 4 || SceneManager.GetActiveScene().buildIndex >= 5) 
        { 
            Gun.SetActive(true);
            PlayerPrefs.SetInt("PlayerProLevel", 4);
        } else 
        {
            Gun.SetActive(false); 
        }

    }

    

    private void FixedUpdate()
    {
        isgrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isgrounded && velocity.y < 0) { velocity.y = -2; }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 forMove = transform.forward * z;
        Vector3 rightMove = transform.right * x;

        speed = isCrouching ? 3 : 5;

        controller.SimpleMove(Vector3.ClampMagnitude(forMove + rightMove, 1.0f) * speed * sprint);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        Jump();
        Crouch();
        Sprint();
    }


    void Jump()
    {
        if (PlayerPrefs.GetInt("PlayerProLevel") < 3) { return; }
        if (Input.GetKey(KeyCode.Space) && isgrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
    }

    void Sprint()
    {
        if (PlayerPrefs.GetInt("PlayerProLevel") < 2) { return; }
        staminaSlider.value = sprintValue;
        if (isCrouching) { return; }
        if (Time.time >= nextTimeToAdd)
        {
            nextTimeToAdd = Time.time + 1f / 50f;
            if (sprintValue >= -10 && !sprinting && sprintValue < sprintLength)
            {
                sprintValue++;

            }


        }
        if (sprintValue <= 0)
        {
            canSprint = false;
        }
        if (canSprint == false)
        {
            if (sprintValue >= 30)
            {
                canSprint = true;
            }

        }
        if (sprintValue > sprintLength)
        {
            sprintValue = sprintLength;
        }


        if (Input.GetKey(KeyCode.LeftShift) && canSprint)
        {
            sprint = 1.75f;
            sprintValue--;
            sprinting = true;
        }
        else
        {
            sprint = 1f;
            sprinting = false;

        }
    }

    void Crouch()
    {
        if (PlayerPrefs.GetInt("PlayerProLevel") < 3) { return; }
        RaycastHit hit;
        if (Physics.Raycast(headCheck.transform.position, headCheck.transform.up, out hit, 2))
        {
            if (hit.distance <= 1)
            {
                headCheckClear = false; 
            }
        }
        else
        {
            headCheckClear = true;
        }

        if (Input.GetKey(KeyCode.LeftControl) && isgrounded)
        {
            anim.SetBool("isCrouching", true);
            isCrouching = true;
        }
        else if (!Input.GetKey(KeyCode.LeftControl) && headCheckClear)
        {
            anim.SetBool("isCrouching", false);
            isCrouching = false;
        }
        
        
    }
    public void SprintSlideUpdate()
    {
        if (PlayerPrefs.GetInt("PlayerProLevel") >= 2 || SceneManager.GetActiveScene().buildIndex >= 3)
        {
            staminaSlider.gameObject.SetActive(true);
            staminaSlider.maxValue = 200;
        }
        else
        {
            staminaSlider.gameObject.SetActive(false);
        }
    }
}
