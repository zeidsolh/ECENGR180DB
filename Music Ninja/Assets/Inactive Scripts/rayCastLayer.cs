using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public LayerMask targetLayer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Fire a ray from the camera's position in the direction of the mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hit an object in the "Target" layer
            if (Physics.Raycast(ray, out hit, 100f, targetLayer))
            {
                // Do something when the object is hit
                Debug.Log("Hit object in target layer");
            }
        }
    }
}
