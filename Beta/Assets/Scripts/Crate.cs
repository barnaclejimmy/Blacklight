using UnityEngine;

public class Crate : MonoBehaviour
{
    private Crosshair crosshairScript;
    private GameObject player;
    private FixedJoint2D joint;
  
    private void Start()
    {
        crosshairScript = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Crosshair>();
        player = GameObject.FindGameObjectWithTag("Player");
        joint = GetComponent<FixedJoint2D>();
        joint.enabled = false;
    }

    private void OnMouseOver()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 1.5f)
        {
            crosshairScript.Grab();
            if (Input.GetMouseButtonDown(1))
            {
                joint.enabled = true;
            }
            if (Input.GetMouseButtonUp(1))
            {
                joint.enabled = false;
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
        joint.enabled = false;
    }
}
