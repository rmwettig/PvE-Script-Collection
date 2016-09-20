using System;
using UnityEngine;


/// <summary>
/// Allows an entity to switch between multiple weapons
/// </summary>
public class WeaponChanger : MonoBehaviour
{
    /// <summary>
    /// Choosable Weapons
    /// </summary>
    [SerializeField]
    private Weapon[] weapons = null;

    /// <summary>
    /// Index of the currently active weapon
    /// </summary>
    [SerializeField]
    private int currentWeaponIndex = 0;

    public void Awake()
    {
        //EventManager.Subscribe<OnWeaponChange>(UseNextWeapon);
        //Message<WeaponChanged>.Instance.add(WeaponChangeHandler); //todo: custom
        EventManager.Instance.Register<WeaponChanged>(WeaponChangeHandler);
    }

    private void WeaponChangeHandler(EventArgs weaponChange)
    {
        WeaponChanged weaponChangeEvent = weaponChange as WeaponChanged;
        if (weaponChangeEvent.Sender == gameObject)
            UseNextWeapon();
    }

    private void UseNextWeapon()
    {
        currentWeaponIndex++;
        if (currentWeaponIndex > weapons.Length)
            currentWeaponIndex = 0;
    }
}




