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
    bool mouseEnabled = true;
    Vector3 lastMousePos = Vector3.zero;


    [SerializeField]
    bool loopOnX = true;
    [SerializeField]
    bool loopOnY = false;

    public static readonly int MAX_X = 10;
    public static readonly int MIN_X = -11;
    public static readonly int MAX_Y = 1;
    public static readonly int MIN_Y = -3;

    Grid grid;

    Dictionary<KeyCode, float> keyDownTime = new Dictionary<KeyCode, float>();
    KeyCode[] keys = new KeyCode[] { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };

    Dictionary<KeyCode, Coroutine> keyDownCoroutines = new Dictionary<KeyCode, Coroutine>();

    void Start() {
        grid = FindObjectOfType<Grid>();
    }

    void Update() {
        KeyboardControls();
        MouseControls();
        Loop();
        Clamp();
    }

    void Loop() {
        if (loopOnX) {
            if (selected.x > MAX_X) selected.x = MIN_X;
            else if (selected.x < MIN_X) selected.x = MAX_X;
        }

        if (loopOnY) {
            if (selected.y > MAX_Y) selected.y = MIN_Y;
            else if (selected.y < MIN_Y) selected.y = MAX_Y;
        }
    }

    void Clamp() {
        selected.x = Mathf.Clamp(selected.x, MIN_X, MAX_X);
        selected.y = Mathf.Clamp(selected.y, MIN_Y, MAX_Y);
    }

    void KeyboardControls() {
        foreach (KeyCode key in keys) {
            if (Input.GetKeyUp(key)) {
                keyDownTime.Remove(key);
                StopCoroutine(keyDownCoroutines[key]);
                keyDownCoroutines.Remove(key);
            } else if (Input.GetKeyDown(key)) {
                DisableMouse();
                keyDownTime.Add(key, 0);
                keyDownCoroutines.Add(key, StartCoroutine(HandleAcceleratedKeyboardMovement(key)));
            } else if (Input.GetKey(key)) {
                keyDownTime[key] += Time.deltaTime;
            }
        }
    }

    IEnumerator HandleAcceleratedKeyboardMovement(KeyCode code) {
        if (code == KeyCode.UpArrow) {
            selected.y++;
        } else if (code == KeyCode.DownArrow) {
            selected.y--;
        } else if (code == KeyCode.LeftArrow) {
            selected.x--;
        } else if (code == KeyCode.RightArrow) {
            selected.x++;
        } else yield break;

        float waitTime = Mathf.Max(0.55f - keyDownTime[code] * 0.3f, 0.1f);
        Debug.Log($"Waiting {waitTime}s, keyDownTime: {keyDownTime[code]}s, code: {code}");
        yield return new WaitForSeconds(waitTime);
        keyDownCoroutines[code] = StartCoroutine(HandleAcceleratedKeyboardMovement(code));
    }

    void MouseControls() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos != lastMousePos && (
        Input.GetAxis("Mouse X") != 0 ||
        Input.GetAxis("Mouse Y") != 0
        )) EnableMouse();
        lastMousePos = mousePos;

        if (!mouseEnabled || !IsMouseInGrid()) return;
        Vector3Int cellPos = grid.WorldToCell(mousePos);
        selected = cellPos;
    }

    void DisableMouse() {
        mouseEnabled = false;
        Cursor.visible = false;
    }

    void EnableMouse() {
        mouseEnabled = true;
        Cursor.visible = true;
    }

    bool IsMouseInGrid() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = grid.WorldToCell(mousePos);
        return cellPos.x >= MIN_X && cellPos.x <= MAX_X && cellPos.y >= MIN_Y && cellPos.y <= MAX_Y;
    }
}