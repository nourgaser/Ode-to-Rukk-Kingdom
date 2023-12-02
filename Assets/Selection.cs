using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour {

    [SerializeField]
    public Vector3Int selected = new Vector3Int(0, 0, 0);

    public Vector3 SelectedWorldPos {
        get {
            return grid.CellToWorld(selected);
        }
    }

    [SerializeField]
    private bool mouseEnabled = true;

    [SerializeField]
    private bool loopOnX = true;
    [SerializeField]
    private bool loopOnY = false;

    public static readonly int MAX_X = 10;
    public static readonly int MIN_X = -11;
    public static readonly int MAX_Y = 1;
    public static readonly int MIN_Y = -3;

    private Grid grid;

    // Start is called before the first frame update
    void Start() {
        grid = FindObjectOfType<Grid>();
    }

    void Update() {
        MouseControls();
        KeyBoardControls();
        HandleLooping();
        Clamp();
    }

    private void HandleLooping() {
        if (loopOnX) {
            if (selected.x > MAX_X) selected.x = MIN_X;
            else if (selected.x < MIN_X) selected.x = MAX_X;
        }

        if (loopOnY) {
            if (selected.y > MAX_Y) selected.y = MIN_Y;
            else if (selected.y < MIN_Y) selected.y = MAX_Y;
        }
    }

    private void Clamp() {
        selected.x = Mathf.Clamp(selected.x, MIN_X, MAX_X);
        selected.y = Mathf.Clamp(selected.y, MIN_Y, MAX_Y);
    }

    private void KeyBoardControls() {
        bool changed = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow);

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            selected.y++;
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            selected.y--;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            selected.x--;
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            selected.x++;
        }

        if (changed) mouseEnabled = false;
    }

    Vector3 lastMousePos = Vector3.zero;
    private void MouseControls() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (lastMousePos != mousePos) mouseEnabled = true;
        lastMousePos = mousePos;

        if (!mouseEnabled || !IsMouseInGrid()) return;
        Vector3Int cellPos = grid.WorldToCell(mousePos);
        selected = cellPos;
    }

    private bool IsMouseInGrid() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = grid.WorldToCell(mousePos);
        return cellPos.x >= MIN_X && cellPos.x <= MAX_X && cellPos.y >= MIN_Y && cellPos.y <= MAX_Y;
    }
}
