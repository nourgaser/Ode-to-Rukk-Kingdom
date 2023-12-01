using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ensureSelectionInView : MonoBehaviour {
    private Selection selection;
    private Grid grid;
    private CameraMovement cameraMovement;

    private float MAX_X {
        get {
            return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        }
    }

    private float MIN_X {
        get {
            return Camera.main.ScreenToWorldPoint(Vector3.zero).x;
        }
    }

    private float CELL_HALF_WIDTH {
        get {
            return grid.cellSize.x / 2;
        }
    }



    void Start() {
        selection = FindObjectOfType<Selection>();
        grid = FindObjectOfType<Grid>();
        cameraMovement = FindObjectOfType<CameraMovement>();
    }

    void Update() {
        if (selection.SelectedWorldPos.x + CELL_HALF_WIDTH > MAX_X) {
            Move(false);
        } else if (selection.SelectedWorldPos.x - CELL_HALF_WIDTH < MIN_X) {
            Move(true);
        }
    }

    private void Move(bool left) {
        cameraMovement.MoveH(CELL_HALF_WIDTH * 3 * (left ? -1 : 1));
    }
}
