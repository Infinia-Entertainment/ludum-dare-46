using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.Serialization;
using WeaponSystem;

namespace WeaponSystem.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        private IWeapon currentWeapon;

        //Keep in mind that the controller is responsible **ONLY** for controlling the weapons,
        //It's job isn't to create or assign weapons, the code you see here in comments is an example of how it works

        // this is how you do this shit
        // WeaponExample weaponExample = new WeaponExample(); 

        private void Awake()
        {
            //For testing and debugging, don't remove

            //var currentWeaponComponents = currentWeapon.GetType().Assembly.GetTypes().
            //Where(type => type.GetInterface(typeof(IWeaponComponent).Name) != null);

            // Here you initialize the weapons and assign the currentWeapon as whatever weapon

            //weaponExample.Initialize();

            //currentWeapon = weapon as IWeapon;
        }

        private void Update()
        {
            foreach (WeaponAction weaponAction in currentWeapon.WeaponActions)
            {
                //GetKeyDown may cause a problem in the future since some actions may need GetKey or GetKeyUp instead
                if (Input.GetKeyDown(weaponAction.actionKey))
                {
                    weaponAction.actionEvent();
                }
            }
        }
    }
}