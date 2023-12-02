using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWheelScroll : MonoBehaviour {
    CameraMovement cameraMovement;

    [SerializeField]
    float speed = 50f;

    [SerializeField]
    bool invert = true;

    void Start() {
        cameraMovement = GetComponent<CameraMovement>();
    }

    void Update() {
        if (Input.mouseScrollDelta.x > 0) cameraMovement.MoveH(speed * Time.deltaTime * (invert ? -1 : 1));
        else if (Input.mouseScrollDelta.x < 0) cameraMovement.MoveH(-speed * Time.deltaTime * (invert ? -1 : 1));
    }
}
