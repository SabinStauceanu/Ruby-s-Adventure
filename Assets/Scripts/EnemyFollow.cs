using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;

    private Animator anim;

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < 7)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            anim.SetBool("isFollowing", true);
            if(Time.deltaTime==90)
            {
                anim.SetBool("Boom", true);

            }
           
            
        }
        void OnCollisionEnter2D(Collision2D other)
        {
            RubyControler player = other.gameObject.GetComponent<RubyControler>();

            if (player != null)
            {
                player.ChangeHealth(-3);
            }
        }


    }
}
