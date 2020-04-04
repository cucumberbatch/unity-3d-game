using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
	public GameObject guns;
	public ScannerRayInterface rayInterface;
	public Transform weaponHandler;

	public int akAmmo = 30;
	public int lazerAmmo = 30;
	public int plasmaAmmo = 30;

	public int akAmmoLimit = 120;
	public int lazerAmmoLimit = 90;
	public int plasmaAmmoLimit = 150;


	void Start()
	{
		rayInterface = GetComponent<ScannerRayInterface>();
	}

	void Update()
	{
		GameObject that = rayInterface.hitObjectTransform.gameObject;

		if (Input.GetButtonDown("Fire1") && that)
		{
			if (that.tag.Equals("Weapon"))
				OnWeaponTake(that);
			else if (that.tag.Equals("WeaponMagazine"))
				OnAmmoTake(that);
		}
	}

	public void OnWeaponTake(GameObject weapon)
	{
		if (guns != null)
		{
			guns.SetActive(false);
		}

		guns = weapon;
		guns.transform.position = weaponHandler.position;
		guns.transform.rotation = weaponHandler.rotation;
		guns.transform.SetParent(weaponHandler);
		guns.GetComponent<Rigidbody>().useGravity = false;
		guns.GetComponent<MeshCollider>().enabled = false;
	}

	public void OnAmmoTake(GameObject ammo)
	{
		AmmoMagazineContainer magazine = ammo.GetComponent<WeaponMagazine>().magazine;
		switch (magazine.ammoType)
		{
			case AmmoMagazineContainer.AmmoType.AK:
				if (akAmmo == akAmmoLimit)
				{
					return;
				}
				else if (akAmmo + magazine.currentAmount > akAmmoLimit)
				{
					akAmmo = akAmmoLimit;
				}
				else
				{
					akAmmo += magazine.currentAmount;
				}

				break;

			case AmmoMagazineContainer.AmmoType.LASER:
				if (lazerAmmo == lazerAmmoLimit)
				{
					return;
				}
				else if (lazerAmmo + magazine.currentAmount > lazerAmmoLimit)
				{
					lazerAmmo = lazerAmmoLimit;
				}
				else
				{
					lazerAmmo += magazine.currentAmount;
				}

				break;

			case AmmoMagazineContainer.AmmoType.PLASMA:
				if (plasmaAmmo == plasmaAmmoLimit)
				{
					return;
				}
				else if (plasmaAmmo + magazine.currentAmount > plasmaAmmoLimit)
				{
					plasmaAmmo = plasmaAmmoLimit;
				}
				else
				{
					plasmaAmmo += magazine.currentAmount;
				}

				break;
		}

		ammo.SetActive(false);
	}
}