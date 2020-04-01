using UnityEngine;
using UnityEngine.UI;

public class ScannerRayInterface : MonoBehaviour
{
    public Camera camera;
    public Text textDisplay;
    public float visibleDistance = 3;
    public Transform hitObjectTransform;
    
    private RaycastHit hit;

    void Update() {
        if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, visibleDistance)) {
            // Get the transform reference to the object we hit
            hitObjectTransform = hit.transform;
            if (hitObjectTransform.tag.Equals("WeaponMagazine"))
            {
                WeaponMagazine magazine = hitObjectTransform.gameObject.GetComponent<WeaponMagazine>();
                textDisplay.text = magazine.magazine.ammoType + " ammo: " + magazine.magazine.currentAmount + 
                                   "\nTake: 'MouseButton1'";
            } else {
                textDisplay.text = hitObjectTransform.name;
            }
        } else {
            textDisplay.text = "";
        }
    }

}
