using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    Camera GameCamera;
    public float CameraDamp = 0.3f;
    public float Shake;
    public float ShakeFreq;
    Vector3 startPosition;
    


    void Start()
    {
        GameCamera = GetComponent<Camera>();
        startPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch = Input.GetTouch(0);
            GameCamera.orthographicSize += touch.deltaPosition.y / 5;
            GameCamera.orthographicSize = Mathf.Clamp(GameCamera.orthographicSize, 7, 12);
            return;
        }

        GameCamera.orthographicSize -= Input.GetAxis("Vertical") * CameraDamp;
        GameCamera.orthographicSize = Mathf.Clamp(GameCamera.orthographicSize, 7, 12);
        

    }

    public IEnumerator CameraShake()
    {
        float shakeAmp = Shake;
        
        while(shakeAmp > 0)
        {
            Vector3 temp = transform.position;
            temp.y = startPosition.y + shakeAmp * Mathf.Sin(ShakeFreq * Time.time);
            transform.position = temp;
            shakeAmp -= Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
