using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed, isTwinNote;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect, connector;

    public KeyCode keyToPress;

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {
                gameObject.SetActive(false);
                if (isTwinNote)
                {
                    connector.SetActive(false);
                }
    
                if(transform.position.x > -6.1 && transform.position.x < -5.9)
                {
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, new Vector3(-6, 0, 0), perfectEffect.transform.rotation);
                }
                else if(transform.position.x > -6.25 && transform.position.x < -5.75)
                {
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, new Vector3(-6, 0, 0), goodEffect.transform.rotation);
                }
                else
                {
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, new Vector3(-6, 0, 0), hitEffect.transform.rotation);
                }
            }
        }
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
                Instantiate(missEffect, new Vector3(-6, 0, 0), missEffect.transform.rotation);
            }
        }
    }
}
