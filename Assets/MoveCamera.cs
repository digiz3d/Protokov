using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private Transform playerCameraAnchor;

    void Update()
    {
        transform.position = playerCameraAnchor.transform.position;
    }
}
