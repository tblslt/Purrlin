using UnityEngine;


public class CameraFocus
{
    public Transform cameraPosition;
    public Transform playerPosition;
    private Transform subjectPosition;
    public bool follow = true;


    private void Update()
    {
        //cameraPosition.position = new Vector3((cameraPosition.position.x - playerPosition.position.x)/2, (cameraPosition.position.y - playerPosition.position.y) / 2, 10);
    }


}


