using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Player1Scr Player1Script;
    private Player2Scr Player2Script;

    private Transform PLAYER1Transform;
    private Transform PLAYER2Transform;
    private Transform CameraHolderTransform;

    public Vector3 offset;
    private Vector3 velocity;

    private Camera cam;

    private float smoothTime = 0.3f;

    public float minZoom;
    public float maxZoom;
    public float zoomLimiter = 50f;

    // Start is called before the first frame update
    void Start()
    {
        Player1Script = GameObject.Find("Player1_Display").GetComponent<Player1Scr>();
        Player2Script = GameObject.Find("Player2_Display").GetComponent<Player2Scr>();
        CameraHolderTransform = GameObject.Find("Camera Holder").GetComponent<Transform>();
        PLAYER1Transform = GameObject.Find("Player1").GetComponent<Transform>();
        PLAYER2Transform = GameObject.Find("Player2").GetComponent<Transform>();
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        MoveCameraHolder();
        ZoomCamera();
        
    }

    void ZoomCamera()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, DistanceBetweenPlayers() / zoomLimiter);
        cam.fieldOfView = newZoom;
    }

    void MoveCameraHolder()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 cameraToPosition = centerPoint + offset;
        CameraHolderTransform.position = Vector3.SmoothDamp(CameraHolderTransform.position, cameraToPosition, ref velocity, smoothTime);
    }


    Vector3 GetCenterPoint()
    {
        var bounds = new Bounds(PLAYER1Transform.position, Vector3.zero);
        bounds.Encapsulate(PLAYER2Transform.position);
        return bounds.center;
    }

    float DistanceBetweenPlayers()
    {
        var bounds = new Bounds(PLAYER1Transform.position, Vector3.zero);
        bounds.Encapsulate(PLAYER2Transform.position);
        return bounds.size.x;
    }

    public IEnumerator Shake (float damage)
    {
        float intensity;
        float timeToShake;
        if(Player1Script.regularDamage == damage)
        {
            intensity = 0.25f;
            timeToShake = 0.25f;
        } else
        {
            intensity = 1f;
            timeToShake = 0.4f;
        }

        Vector3 originalPos = new Vector3(0, 0, 0);
        Quaternion originalRot = transform.localRotation;

        float timeElaspled = 0f;

        while(timeElaspled < timeToShake)
        {
            float xShake = Random.Range(-intensity, intensity);
            float yShake = Random.Range(-intensity, intensity);
            float xRot = Random.Range(-5f, 5f);
            float yRot = Random.Range(-5f, 5f);

            transform.localPosition = new Vector3(xShake, yShake, originalPos.z);
            //transform.localRotation = new Quaternion()            //fix the posShake first

            timeElaspled += Time.deltaTime;

            yield return null;

        }

        transform.localPosition = originalPos;
        transform.localRotation = originalRot;


    }


}
