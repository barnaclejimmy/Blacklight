using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class FieldOfView : MonoBehaviour {

    // Initialise mesh renderer and filter
    MeshFilter mf;
    MeshRenderer mr;

    // Arrays to specify mesh vertices and triangles
    private Vector3[] vertices;
    private int[] triangles;

    private Mesh mesh;

    // Radius of the cone
    public float radius = 4f;

    // Number of points on circle's edge (only some will be used)
    int numPoints = 1024;

    // Will hold reference to the player character object (so we can follow its
    // movement)
    public Transform player;

    // References the cursor
    public Transform cursor;

    // Start is called before the first frame update
    void Start() {
        // Get references to the mesh filter and renderer components
        mf = GetComponent<MeshFilter>();
        mr = GetComponent<MeshRenderer>();

        // Create new mesh and attach it to the mesh filter
        mesh = new Mesh();
        mf.mesh = mesh;

        // Set cone colour
        mr.material.color = new Color(0.1f, 1, 0.1f, 0.1f);

        // Initialise array for vertices and triangles (plus one extra
        // vertex is for the point of the cone)
        vertices = new Vector3[numPoints + 1];
        triangles = new int[numPoints * 3];
    }

    // Update is called once per frame
    void Update() {

        // Finds angle between two points on cone
        float delta = 2f * Mathf.PI / (float)(numPoints - 1);
        // Starts with 0 angle
        //float alpha = 1.1f;

        // Get the Screen positions of the player
        Vector2 positionOnScreen = transform.position;

        // Get the Screen position of the mouse
        //Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Get the Screen position of the mouse
        Vector2 mouseOnScreen = cursor.position;

        // Get the angle between the points
        float alpha = Mathf.Atan2(positionOnScreen.y - mouseOnScreen.y, positionOnScreen.x - mouseOnScreen.x) + 2.74f;

        // Center vertex at (0, 0)
        vertices[0].x = 0;
        vertices[0].y = 0;
        vertices[0].z = transform.position.z;

        // Specifies that raycasting will only interact with the given layer
        int layerMask = 1 << 8;

        // Position other vertices around the cone evenly
        for (int i = numPoints/4; i <= (numPoints/4 + numPoints/8); i++) {
            // Find x position from coordinates
            float x = radius * Mathf.Cos(alpha);
            // Rind y position from coordinates
            float y = radius * Mathf.Sin(alpha);

            // Create a ray
            Vector2 ray = new Vector2(x, y);
            ray.x *= transform.lossyScale.x;
            ray.y *= transform.lossyScale.y;

            // Cast the ray 
            RaycastHit2D hit = Physics2D.Raycast(transform.position, ray, ray.magnitude, layerMask);
            // Check if ray has hit something, if yes, check how far from the ray's origin point
            // and adjust the distance of where the mesh point is going to be located.
            if (hit.collider != null) {
                // Only stop the ray if it is a wall (ignore other things, e.g. ghosts)
                if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("OpenDoor")) {
                    float distance = hit.distance;
                    x = distance * Mathf.Cos(alpha) / transform.lossyScale.x;
                    y = distance * Mathf.Sin(alpha) / transform.lossyScale.y;
                }
            }

            // Set the vertex values
            vertices[i].x = x;
            vertices[i].y = y;
            vertices[i].z = transform.position.z;

            // Specify the triangle going from the centre to
            // the i vertex and the previous vertex on the cone
            triangles[(i - 1) * 3] = 0;
            if (i == 1) {
                // If current vertex is 1, then previous vertex is the
                // last vertex (to close the cone)
                triangles[(i - 1) * 3 + 1] = numPoints;
            } else {
                triangles[(i - 1) * 3 + 1] = i - 1;
            }
            triangles[(i - 1) * 3 + 2] = i;

            // Increase the angle to get the next positon around the cone
            alpha += delta;
        }
        // Set the new vertices and triangles in the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Always move to the position of the player on the x and y axis
        // Force z position to 0
        transform.position = new Vector3(player.position.x, player.position.y, 0);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;

        // Compute the angle between two triangles in the cone
        float delta = 2f * Mathf.PI / (float)(numPoints - 1);
        // Stat with angle of 0
        float alpha = 1.1f;

        // Other vertices positioned evenly around the cone
        for (int i = numPoints / 4; i <= (numPoints / 4 + numPoints / 8); i++) {

            // Find position x from coordinates
            float x = radius * Mathf.Cos(alpha);
            // Find position y from coordinates
            float y = radius * Mathf.Sin(alpha);

            // Create a ray
            Vector3 ray = new Vector3(x, y, transform.position.z);

            ray.x *= transform.lossyScale.x;
            ray.y *= transform.lossyScale.y;

            Gizmos.DrawRay(transform.position, ray);

            // Increase the angle to reach the next vertex on the cone
            alpha += delta;
        }
    }
}