using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_script : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text winText;
    private int scoreValue = 0;

    public Text livesText;
    private int lives;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        lives = 3; 
        SetLivestext ();
        
        musicSource.clip = musicClipOne;
        musicSource.loop = true;
        musicSource.Play();
        
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin_tag")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 4)
            {
                lives = 3; 
                SetLivestext ();
                transform.position = new Vector2(40.3f, 1.55f);
            }
            if (scoreValue >= 8) 
            {
                winText.text = "You Win! Game Created By Taylor Torres";
                musicSource.Stop();
                musicSource.loop = false;
                musicSource.clip = musicClipTwo;
                musicSource.Play();
                //Destroy(this);
            }
           
        } else if (collision.collider.tag == "enemy_tag")
        {
            Destroy(collision.collider.gameObject);
            lives -= 1;
            SetLivestext ();
        }


    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground_tag")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

   
    void SetLivestext () 
    {
        livesText.text = "Lives: " + lives.ToString ();
        if (lives <= 0)
        {
            winText.text = "You Lose! Game by Taylor Torres";

            Destroy(this);
        } 
    }
    

}