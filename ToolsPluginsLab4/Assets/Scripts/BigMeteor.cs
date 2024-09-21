using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMeteor : MonoBehaviour
{
    private int hitCount = 0;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 0.5f);

        if (transform.position.y < -11f)
        {
            GameManager.instance.ZoomIn();
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
                shake.ShakeCamera(5f, .5f);
            }
            else if (GameManager.instance.activeCam == GameManager.instance.vCam2)
            {
                CameraShake shake = GameManager.instance.vCam2.GetComponent<CameraShake>();
                shake.ShakeCamera(5f, .5f);
            }
            Destroy(whatIHit.gameObject);
        }
        else if (whatIHit.tag == "Laser")
        {
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

            hitCount++;
            if (hitCount >= 5)
            {
                GameManager.instance.ZoomIn();
                if (GameManager.instance.activeCam == GameManager.instance.vCam)
                {
                    CameraShake shake = GameManager.instance.vCam.GetComponent<CameraShake>();
                    shake.ShakeCamera(5f, .5f);
                }
                else if (GameManager.instance.activeCam == GameManager.instance.vCam2)
                {
                    CameraShake shake = GameManager.instance.vCam2.GetComponent<CameraShake>();
                    shake.ShakeCamera(5f, .5f);
                }
                ParticleManager.instance.Explode(gameObject.transform);
                Destroy(this.gameObject);
            }

            Destroy(whatIHit.gameObject);
        }
    }
}
