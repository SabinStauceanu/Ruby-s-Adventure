using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyControler : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public AudioClip collectedClipes;


    public int maxHealth = 5;

    public float speed = 3.0f;

    public float totalTime;
    bool didItPlay = false;

    public float timeInvincible = 2.0f;

    bool isInvincible;

    float invincibleTimer;

    public AudioClip collectedClips;
   

    public GameObject projectilePrefab;


    public int health { get { return currentHealth; } }

    Animator animator;

    Vector2 lookDirection = new Vector2(1, 0);

    int currentHealth;

    AudioSource audioSrc;
    AudioSource audioSource;
    AudioSource audioSource3;
    
   
    //private AudioClip[] clips;
   
    bool soundEfects = false;
    


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;

        currentHealth = 1;

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource3 = GetComponent<AudioSource>();
        audioSrc = GetComponent<AudioSource>();
        



    }

    

    public void PlaySound1(AudioClip clip)
    {
        audioSrc.PlayOneShot(clip);
    }
    public void PlaySound(AudioClip clip)
    {
        
        audioSource.PlayOneShot(clip);
    }

    public void PlaySounds(AudioClip clip)
    {
        audioSource3.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        Vector2 position = rigidbody2d.position;
       


        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed=10.0f;
            
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5.0f;
        }
       

       
            
            position = position + move * speed * Time.deltaTime;

        rigidbody2d.MovePosition(position);


        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;

            if (invincibleTimer < 0)

                isInvincible = false;

        }

        void Launch()
        {
            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

            Projectile projectile = projectileObject.GetComponent<Projectile>();

            projectile.Launch(lookDirection, 300);

            animator.SetTrigger("Launch");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
            PlaySounds(collectedClipes);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }

    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;

            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        Debug.Log(currentHealth + "/" + maxHealth);


    }

}
