using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
	public GameObject guns;
	public ScannerRayInterface rayInterface;
	public Transform weaponHandler;

	void Start()
    {
	    rayInterface = GetComponent<ScannerRayInterface>();
    }

    void Update()
    {
	    GameObject that = rayInterface.hitObjectTransform.gameObject;

	    if (Input.GetButtonDown("Fire1") && that)
	    {
		    OnAmmoTake(that);
		    OnWeaponTake(that);
	    }
    }

    public void OnWeaponTake(GameObject weapon)
    {
	    
	    // if (weapon.tag.Equals("Weapon"))
	    // {
	    if (guns != null)
	    {
		    guns.SetActive(false);
	    }
	    
	    // GameObject[] updatedGuns = new GameObject[guns.Length+1];
	    // guns.CopyTo(updatedGuns, 0);
	    guns = weapon;
	    guns.transform.position = weaponHandler.position;
	    guns.transform.rotation = weaponHandler.rotation;
	    guns.transform.SetParent(weaponHandler);
	    guns.GetComponent<Rigidbody>().useGravity = false;
	    guns.GetComponent<MeshCollider>().enabled = false; 
		// }
	    
    }

    public void OnAmmoTake(GameObject ammo)
    {
	    if (ammo.tag.Equals("WeaponMagazine"))
	    {
		    
	    }
    }
}
