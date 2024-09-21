using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 2f);

        if (transform.position.y < -11f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if (whatIHit.tag == "Player")
        {
            GameManager.instance.gameOver = true;
            GameManager.instance.CancelInvoke();
            if (GameManager.instance.activeCam == GameManager.instance.vCam)
            {
                CameraShake shake = GameManager.instance.vCam.GetComponent<CameraShake>();
                shake.ShakeCamera(5f, .25f);
            }
            else if (GameManager.instance.activeCam == GameManager.instance.vCam2)
            {
                CameraShake shake = GameManager.instance.vCam2.GetComponent<CameraShake>();
                shake.ShakeCamera(5f, .25f);
            }
            ParticleManager.instance.Explode(gameObject.transform);
            Destroy(whatIHit.gameObject);
            Destroy(this.gameObject);
        } 
        else if (whatIHit.tag == "Laser")
        {
            GameManager.instance.meteorCount++;
            GameManager.instance.BigMeteor();
            if (GameManager.instance.activeCam == GameManager.instance.vCam)
            {
                CameraShake shake = GameManager.instance.vCam.GetComponent<CameraShake>();
                shake.ShakeCamera(5f, .25f);
            }
            else if (GameManager.instance.activeCam == GameManager.instance.vCam2)
            {
                CameraShake shake = GameManager.instance.vCam2.GetComponent<CameraShake>();
                shake.ShakeCamera(5f, .25f);
            }
            ParticleManager.instance.Explode(gameObject.transform);
            Destroy(whatIHit.gameObject);
            Destroy(this.gameObject);
        }
    }
}
