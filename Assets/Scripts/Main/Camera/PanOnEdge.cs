using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanOnEdge : MonoBehaviour {

    private Vector2 NormalizedMousePosition { get { return new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height); } }

    [SerializeField]
    private float threshold = 0.1f;

    private float RightSpeed { get { return NormalizedMousePosition.x - (1f - threshold); } }
    private float LeftSpeed { get { return threshold - NormalizedMousePosition.x; } }

    private CameraMovement cameraMovement;

    [SerializeField]
    private float baseSpeed = 100f;
    void Start() {
        cameraMovement = GetComponent<CameraMovement>();
    }

    void Update() {
        if (Input.GetMouseButton(0)) return;
        if (RightSpeed > 0) cameraMovement.MoveH(RightSpeed * baseSpeed * Time.deltaTime);
        else if (LeftSpeed > 0) cameraMovement.MoveH(LeftSpeed * -baseSpeed * Time.deltaTime);
    }
}
