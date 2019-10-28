using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

	public Text winText;

    public Text score;

	public Text lives;

	private int livesValue = 3;

	private int scoreValue = 0;

	public AudioSource musicSource;

	public AudioClip musicClipOne;

	public AudioClip musicClipTwo;

	private bool facingRight = true;
    
	Animator anim;

    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();

		rd2d = GetComponent<Rigidbody2D>();
        lives.text = livesValue.ToString();

		winText.text = " ";

		anim = GetComponent<Animator>();

    }

    
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

		if (Input.GetKey("escape"))
        {
	       Application.Quit();
        }

		if (livesValue ==0)
		{
	    Destroy(gameObject);
		}

		if (facingRight == false && hozMovement > 0)
		{
		 Flip();
		}
		else if (facingRight == true && hozMovement < 0)
		 {
		 Flip();
		}

		 
    }



	void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

   void Update()
   {
    if (Input.GetKeyDown(KeyCode.W))
		{
		anim.SetInteger("State", 2);
		
		}
		if (Input.GetKeyUp(KeyCode.W))
		{
		anim.SetInteger("State", 0);
		
		}if (Input.GetKeyDown(KeyCode.D))
		{
		anim.SetInteger("State", 1);
		
		}if (Input.GetKeyUp(KeyCode.D))
		{
		anim.SetInteger("State", 0);
		
		}if (Input.GetKeyDown(KeyCode.A))
		{
		anim.SetInteger("State", 1);
		
		}if (Input.GetKeyUp(KeyCode.A))
		{
		anim.SetInteger("State", 0);
		
		}
   
   }
   
 
   





    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            
			scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);

			if (scoreValue == 8)
			{
			winText.text = "You Win Game by Mark";
			musicSource.clip = musicClipTwo;
			musicSource.Play();
			}

			if (scoreValue == 4)
			{
			transform.position = new Vector2(100.0f, 100.0f);
			livesValue = 3;
			lives.text =livesValue.ToString();

			
			}

        }

		else if (collision.collider.tag == "Enemy")
		{
			livesValue -= 1;
			lives.text= livesValue.ToString();
			Destroy(collision.collider.gameObject);
			
			if (livesValue == 0)
			{
			winText.text = "You lose";
			}
		
		}
			
    }
	
	
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
		
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
				
            }
        }
    }
}