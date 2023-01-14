using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.ProBuilder;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 18f;
    public int health = 100;
    public GameObject flashLight;
    public bool lightOn = true;
    public bool hasKey = false;
    public GameObject key;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI objText;
    public TMP_ColorGradient fineHealth;
    public TMP_ColorGradient badHealth;
    public GameObject damageScreen;
    public Animator anim;
    public Light torch;

    public GameObject equipText;
    public bool nearKey=false;

    Vector3 velocity;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDis = .4f;
    public float jumpHt = 3f;
    public LayerMask groundMask;
    bool isGrounded;

    // Start is called before the first frame update
    private void Awake()
    {
        damageScreen.SetActive(false);
        equipText.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDis, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHt * -2f * gravity); 
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        healthText.text = "HEALTH: "+health.ToString();
        if (health <= 60)
        {
            healthText.colorGradientPreset = fineHealth;
        }
        if (health <= 45)
        {
            healthText.colorGradientPreset = badHealth;
        }
        if (health <= 0)
        {
            StartCoroutine(Restart());
        }
        if (Input.GetKeyDown(KeyCode.T) && lightOn)
        {
            flashLight.SetActive(false);
            lightOn= false;
        }
        else if(Input.GetKeyDown(KeyCode.T) && !lightOn)
        {
            flashLight.SetActive(true);
            lightOn = true;
        }
        if (lightOn)
        {
            if (Input.GetKeyDown(KeyCode.I) && lightOn)
            {
                torch.spotAngle += 45;
            }
            else if (!lightOn && Input.GetKeyDown(KeyCode.I) || torch.spotAngle > 90)
            {
                torch.spotAngle = 45;
            }
        }
        if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.I))
        {
            AudioManager.PlayAudio("light");
        }
        if (nearKey == true)
        {
            AudioManager.PlayAudio("key");
            hasKey = true;
            key.SetActive(false);
            objText.text = "Get to the PORTAL to ESCAPE!";
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            AudioManager.PlayAudio("pain");
            StartCoroutine(TookHit());
        }
        if (other.gameObject.CompareTag("Key"))
        {
            equipText.SetActive(true) ;
            nearKey = true;
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            if (hasKey == true)
            {
                Debug.Log("Survied!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                Debug.Log("You'll need the key!");
            }
        }
        if (other.gameObject.CompareTag("Finale"))
        {
            if (hasKey == true)
            {
                Debug.Log("Survied!");
                SceneManager.LoadScene(0);
            }
            else
            {
                Debug.Log("You'll need the key!");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            equipText.SetActive(false);
        }
    }
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(.5f);
        flashLight.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator TookHit() 
    {
        health -= 10;
        damageScreen.SetActive(true);
        anim.SetBool("isDamaged", true);
        yield return new WaitForSeconds(3f);
        damageScreen.SetActive(false);
        anim.SetBool("isDamaged", false);
    }
}
