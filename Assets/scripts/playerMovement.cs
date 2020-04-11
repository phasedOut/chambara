﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement: MonoBehaviour
{
    public void endAnimation(string message)
    {
      switch (message)
      {
        case "attack_ended":
          attack_range.SetActive(false);
          break;
        default:
          isAnimating = false;
          break;
      }
    }

    Animator anim;
    Rigidbody rb;
    private bool isAnimating = false;
    float speed;
    float lerpSpeed;

    private Vector3 startPos;
    private Vector3 endPos;

    private bool keyHit = false;
    private float fraction = 0; 

    GameObject attack_range;

    // Start is called before the first frame update
    void Start()
    {
      anim = GetComponent<Animator>();
      rb = GetComponent<Rigidbody>();
      speed = 10.0f;
      lerpSpeed = 5f;
      //set attack_range to not active
      attack_range = gameObject.transform.Find("attack_range").gameObject;
      attack_range.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            if (!isAnimating)
            {
                anim.SetBool("isRunning", true);
                rb.velocity = transform.forward * speed;
            }
        }
        else
        {
            anim.SetBool("isRunning", false);
            //rb.velocity = transform.forward * 0;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, -30, 0) * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime * speed);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!isAnimating) 
            {
                anim.SetTrigger("swing");
                isAnimating = true;
                attack_range.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!isAnimating)
            {
                rb.velocity = transform.forward * 0;
                isAnimating = true;
                anim.SetTrigger("dodge");
                startPos = transform.position;
                endPos = transform.position + (transform.forward * -2f);
                keyHit = true;
            }
        }
        if (keyHit == true)
        {
            if (fraction < 1)
            {
                fraction += Time.deltaTime * lerpSpeed;
                transform.position = Vector3.Lerp(startPos, endPos, fraction);
            }
            else if (fraction > 1)
            {
                keyHit = false;
                fraction = 0;
            }

        }
    }

}
