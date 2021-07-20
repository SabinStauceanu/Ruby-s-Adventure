using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;

    
    float timer;
    float direction = 1;
    bool broken = true;
    Animator animator;
    public AudioClip collectedClip2;


    AudioSource audioSrc2;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction *= -1 ;
            timer = changeTime;
        }

        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed *direction;

            animator.SetFloat("Move X", 0);

            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;

            animator.SetFloat("Move X", direction);

            animator.SetFloat("Move Y", 0);
        }

        rigidbody2D.MovePosition(position);
    }
    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            e.Fix();
            PlaySound2(collectedClip2);
        }

        

        RubyControler player = other.gameObject.GetComponent<RubyControler>();

        if (player != null)
        {
            player.ChangeHealth(-3);
        }

        Destroy(gameObject);
    }
    public void PlaySound2(AudioClip clip)
    {
        audioSrc2.PlayOneShot(clip);
    }

}

