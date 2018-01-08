using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour {

    public HexTile tilePrefab; // TODO make generic "Tile"
    public int cols, rows;

    // Define constants:
    const int SCALE = 10;
    const float SQRT3_OVER2 = 0.866025404f;
    const float INNER_RADIUS =  2f * SQRT3_OVER2;
    const float OUTER_RADIUS = 1.5f;
    Vector3 oddOffset = Vector3.right * SQRT3_OVER2;
    
    // Use this for initialization
    void Start () {
        for (int r = 0; r < rows; r++)
        {
            bool odd = r % 2 == 0;
            for (int c = 0; c < cols; c++)
            {
                float noise = Mathf.PerlinNoise((float) c / cols, (float) r / rows);
                int h = Mathf.FloorToInt(noise * SCALE);
                Vector3 pos = new Vector3(c * INNER_RADIUS, r * OUTER_RADIUS, h);
                if (odd) { pos += oddOffset; }

                HexTile tile = Instantiate(tilePrefab, pos, Quaternion.identity);
                Transform baseplate = tile.transform.Find("Base"); // be able to get directly from HexTile obj?
                
                baseplate.localScale = new Vector3(1, 1, h);
                tile.transform.parent = transform;
                tile.SetCoordinates(c, r);

                tile.transform.Find("Ground").transform.position = new Vector3(1000f, 1000f, 1000f);
            }
        }

        // TODO Determine if we want to export blender files with Y and up and down. Then this won't be needed.
        transform.rotation = Quaternion.Euler(Vector3.right * -90f);
        transform.localScale = Vector3.one * SCALE;
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                HexTile tile = hit.transform.GetComponent<HexTile>();
                if(tile != null)
                {
                    tile.PrintInfo();
                }
            }
        }
	}
}
