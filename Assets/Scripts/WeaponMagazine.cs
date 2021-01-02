using System;
using UnityEngine;

public class WeaponMagazine : MonoBehaviour
{
    [SerializeField] public AmmoMagazineContainer magazine;

    private void Start()
    {
        magazine = new AmmoMagazineContainer();
    }

}

[Serializable]
public class AmmoMagazineContainer
{
    public enum AmmoType
    {
        AK,
        LASER,
        PLASMA
    }
    
    public AmmoType ammoType;
    public int currentAmount = 30;
}
