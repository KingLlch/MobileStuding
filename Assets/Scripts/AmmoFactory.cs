using System.Collections;
using UnityEngine;

public class AmmoFactory : MonoBehaviour
{
    private static AmmoFactory _instance;

    [SerializeField] private GameObject ammo;

    [field: SerializeField] public float ShootDelay { get; private set; } = 1;
    [field: SerializeField] public float SpeedModifier { get; private set; } = 0;
    [field: SerializeField] public float DamageModifier { get; private set; } = 0;
    [field: SerializeField] public int PenetrationModifier { get; private set; } = 0;


    private Coroutine CreateAmmoCoroutine;

    public static AmmoFactory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AmmoFactory>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        CreateAmmoCoroutine = StartCoroutine(CreateAmmo());
    }

    private IEnumerator CreateAmmo()
    {
        while (true)
        {
            GameObject newAmmo = Instantiate(ammo, transform.position, transform.parent.rotation, null);
            newAmmo.GetComponentInChildren<Ammo>().Initialize(SpeedModifier, DamageModifier, PenetrationModifier, transform.parent.rotation.eulerAngles.z);
            yield return new WaitForSeconds(ShootDelay);
        }
    }

    public void StopCoroutine()
    {
        StopCoroutine(CreateAmmoCoroutine);
    }

    public void ChangeAmmoModifiers(float shootDelay ,float speed, float damage, int penetration)
    {
        ShootDelay -= shootDelay;
        SpeedModifier += speed;
        DamageModifier += damage;
        PenetrationModifier += penetration;
    }

}
