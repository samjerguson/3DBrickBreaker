using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform target; // the point to orbit around
    public float distance; // distance from the point
    public float height; // height above the point
    public float speed; // speed of rotation around the point

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y + height, target.position.z);
        transform.position = targetPosition - transform.forward * distance;
        transform.RotateAround(targetPosition, Vector3.up, speed * Time.deltaTime);
        transform.LookAt(targetPosition);
    }
}
