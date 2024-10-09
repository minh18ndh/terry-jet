using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] float jumpForce;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.velocity = Vector2.up * jumpForce; //luc nhay fix cung
            GetComponent<ChuckSubInstance>().RunCode(string.Format(@"
                SndBuf jumpBuf => dac;
                me.dir() + ""engine.wav"" => jumpBuf.read;

                // Start at the beginning of the clip
                0 => jumpBuf.pos;

                // Set rate to a constant value (e.g., 1.0 for normal speed)
                1.0 => jumpBuf.rate;

                // Set gain to a constant value (e.g., 0.5 for medium volume)
                0.5 => jumpBuf.gain;

                // Pass time so that the file plays
                jumpBuf.length() / jumpBuf.rate() => now;
            "));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WTC")) // Ensure the WTC has the tag "WTC"
        {
            GetComponent<ChuckSubInstance>().RunCode(string.Format(@"
                SndBuf impactBuf => dac;
                me.dir() + ""explosion.wav"" => impactBuf.read;

                // Start at the beginning of the clip
                0 => impactBuf.pos;

                // Set rate to a constant value (e.g., 1.0 for normal speed)
                1.0 => impactBuf.rate;

                // Set gain to a constant value (e.g., 0.5 for medium volume)
                0.5 => impactBuf.gain;

                // Pass time so that the file plays
                impactBuf.length() / impactBuf.rate() => now;
            "));


            JetGameManager.instance.triggerExplosion();
        }

        else
        {
            GetComponent<ChuckSubInstance>().RunCode(string.Format(@"
                SndBuf impactBuf => dac;
                me.dir() + ""oo.wav"" => impactBuf.read;

                // Start at the beginning of the clip
                0 => impactBuf.pos;

                // Set rate to a constant value (e.g., 1.0 for normal speed)
                1.0 => impactBuf.rate;

                // Set gain to a constant value (e.g., 0.5 for medium volume)
                0.5 => impactBuf.gain;

                // Pass time so that the file plays
                impactBuf.length() / impactBuf.rate() => now;
            "));
            JetGameManager.instance.GameOver();
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "ScoreTrigger")
    //        JetGameManager.instance.UpdateScore();
    //}
}
