using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour {


    public int col;
    public int row;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetCoordinates(int col, int row)
    {
        this.col = col;
        this.row = row;
    }

    public void PrintInfo()
    {
        Debug.Log(string.Format("HexTile ({0},{1})", col, row));
    }
}
