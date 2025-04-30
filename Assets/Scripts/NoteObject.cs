using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed, noteHit, noteBlock;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect, tapEffect;

    public KeyCode keyToPress;

    public AudioSource hitSfx, blockSfx;

    void Update()
    {
        // --- KEYBOARD SUPPORT ---
        if (canBePressed && Input.GetKeyDown(keyToPress))
        {
            if (gameObject.CompareTag("Note"))
            {
                NoteTap();
            }
            else if (gameObject.CompareTag("Block"))
            {
                NoteBlock();
            }
        }

        // --- MOBILE TOUCH SUPPORT ---
        if (canBePressed && Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                Vector3 wp = Camera.main.ScreenToWorldPoint(touch.position);
                Vector2 touchPos = new Vector2(wp.x, wp.y);

                Collider2D hit = Physics2D.OverlapPoint(touchPos);
                if (hit && hit.gameObject == this.gameObject)
                {
                    if (gameObject.CompareTag("Note") && touch.phase == TouchPhase.Began)
                    {
                        NoteTap();
                    }
                    else if (gameObject.CompareTag("Block") && (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved))
                    {
                        NoteBlock();
                    }
                }
            }
        }
    }


    public void NoteTap()
    {
        Instantiate(tapEffect, new Vector3(-4, transform.position.y, 0), tapEffect.transform.rotation);
        noteHit = true;
        gameObject.SetActive(false);
        hitSfx.Play();

        if(transform.position.x > -4.25 && transform.position.x < -3.75)
        {
            GameManager.instance.PerfectHit();
            Instantiate(perfectEffect, new Vector3(-6.1f, 2.2f, 0), perfectEffect.transform.rotation);
        }
        else if(transform.position.x > -4.5 && transform.position.x < -3.5)
        {
            GameManager.instance.GoodHit();
            Instantiate(goodEffect, new Vector3(-6.1f, 2.2f, 0), goodEffect.transform.rotation);
        }
        else
        {
            GameManager.instance.NormalHit();
            Instantiate(hitEffect, new Vector3(-6.1f, 2.2f, 0), hitEffect.transform.rotation);
        }

        noteHit = false;
    }

    void NoteBlock()
    {
        Instantiate(tapEffect, new Vector3(-4, transform.position.y, 0), tapEffect.transform.rotation);
        noteBlock = true;
        gameObject.SetActive(false);
        blockSfx.Play();

        GameManager.instance.PerfectHit();
        Instantiate(perfectEffect, new Vector3(-6.1f, 2.2f, 0), perfectEffect.transform.rotation);
        
        noteBlock = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = false;

            if (gameObject.activeSelf)
            {
                GameManager.instance.NoteMiss();
                Instantiate(missEffect, new Vector3(-6.1f, 2.2f, 0), missEffect.transform.rotation);
            }
        }
    }
}
