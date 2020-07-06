using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBumpSoundScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
