using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    private Vector3 targetPosition;

    private SpriteRenderer backgroundSpriteRenderer;

    private float speed = 50f;

    private float CameraWidth { get { return Camera.main.orthographicSize * Screen.width / Screen.height; } }

    private float MinX { get { return backgroundSpriteRenderer.bounds.min.x + CameraWidth; } }

    private float MaxX { get { return backgroundSpriteRenderer.bounds.max.x - CameraWidth; } }

    void Start() {
        backgroundSpriteRenderer = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        targetPosition = new Vector3(MinX, transform.position.y, transform.position.z);
    }

    /// <summary>
    /// Move horizontally. Won't move if the new position is out of bounds.
    /// </summary>
    public void MoveH(float distance) {
        Debug.Log("Moving " + distance);
        targetPosition = new Vector3(Mathf.Clamp(transform.position.x + distance, MinX, MaxX), transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update() {
        if (transform.position != targetPosition) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }
}
