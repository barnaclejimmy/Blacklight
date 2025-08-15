using UnityEngine;

public class Crate : MonoBehaviour
{
    private Crosshair crosshairScript;
    private GameObject player;
  
    private void Start()
    {
        crosshairScript = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Crosshair>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnMouseOver()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 1.5f)
        {
            crosshairScript.Grab();
            FixedJoint2D joint = null;
            if (Input.GetMouseButtonDown(1))
            {
                if (joint == null)
                {
                    joint = gameObject.AddComponent<FixedJoint2D>();
                    joint.connectedBody = player.GetComponent<Rigidbody2D>();
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                Destroy(transform.GetComponent<FixedJoint2D>());
            }
        }
        else
        {
            crosshairScript.UnGrab();
        }
    }

    private void OnMouseExit()
    {
        crosshairScript.UnGrab();
        Destroy(transform.GetComponent<FixedJoint2D>());
    }
}
