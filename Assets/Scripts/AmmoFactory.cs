using System.Collections;
using UnityEngine;

public class AmmoFactory : MonoBehaviour
{
    [SerializeField] private GameObject ammo;
    private float ShootDelay = 1;

    private float speedModifier = 0;
    private float damageModifier = 0;
    private int penetrationModifier = 0;

    private Coroutine CreateAmmoCoroutine;

    private void Awake()
    {
        CreateAmmoCoroutine = StartCoroutine(CreateAmmo());
    }

    private IEnumerator CreateAmmo()
    {
        while (true)
        {
            GameObject newAmmo = Instantiate(ammo, transform.position,transform.parent.rotation, null);
            newAmmo.GetComponentInChildren<Ammo>().Initialize(speedModifier, damageModifier, penetrationModifier, transform.parent.rotation.eulerAngles.z);
            yield return new WaitForSeconds(ShootDelay);
        }
    }

    public void StopCoroutine()
    {
        StopCoroutine(CreateAmmoCoroutine);
    }

}
