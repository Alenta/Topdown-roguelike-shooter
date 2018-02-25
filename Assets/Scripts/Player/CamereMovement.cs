using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamereMovement : MonoBehaviour {

    public GameObject player;
    private Camera cam;
    public Vector3 offset;
    public float speed = 2.5f;
    public float mouseLookEffect = 2.5f;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update () {

        if(player == null)
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            if(playerController != null)
            {
                player = playerController.gameObject;
            }
            else
            {
                return;
            }
        }

        Vector3 mouseLookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        mouseLookDirection = mouseLookDirection.normalized;

        Vector3 pos = transform.position;
        Vector3 target = player.transform.position + (mouseLookDirection * mouseLookEffect);
        pos = Vector3.Lerp(pos, target, speed * Time.deltaTime);
        pos.z = transform.position.z;
        Vector3 roundPos = new Vector3(RoundToNearestPixel(pos.x, cam), RoundToNearestPixel(pos.y, cam), pos.z);
        transform.position = roundPos;
        

	}
    public static float RoundToNearestPixel(float unityUnits, Camera viewingCamera)
    {
        float valueInPixels = (Screen.height / (viewingCamera.orthographicSize * 2)) * unityUnits;
        valueInPixels = Mathf.Round(valueInPixels);
        float adjustedUnityUnits = valueInPixels / (Screen.height / (viewingCamera.orthographicSize * 2));
        return adjustedUnityUnits;
    }
}
