using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPan : MonoBehaviour {
    private CameraMovement cameraMovement;
    void Start() {
        cameraMovement = GetComponent<CameraMovement>();
    }

    void Update() {
        if (Input.GetMouseButton(0)) {
            cameraMovement.MoveH(Input.GetAxis("Mouse X") * -100f * Time.deltaTime);
        }
    }
}
