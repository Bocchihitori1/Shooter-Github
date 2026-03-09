using TMPro;
using UnityEngine;

public class Contador : MonoBehaviour

{
    private HandWeapon handWeapon;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI inventoryAmmoText;
    void Start()
    {

        handWeapon = FindAnyObjectByType<HandWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        Updatetext();
    }
    private void Updatetext()
    {
        ammoText.text = handWeapon.currentWeapon.actualAmmo.ToString() + " /";
        inventoryAmmoText.text = handWeapon.currentWeapon.inventoryAmmo.ToString();
    }
}
