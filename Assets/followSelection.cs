using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followSelection : MonoBehaviour {

    [SerializeField]
    private float speed = 1f;

    private Grid grid;
    private Selection selection;

    private Vector3 target;

    private void Start() {
        grid = FindObjectOfType<Grid>();
        selection = FindObjectOfType<Selection>();
        target = transform.position;
    }

    void Update() {
        Vector3 pos = grid.CellToWorld(selection.selected);
        target = new Vector3(pos.x + grid.cellSize.x / 2, pos.y + grid.cellSize.y / 2, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
