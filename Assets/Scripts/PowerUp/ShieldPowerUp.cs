using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : PowerUpUi,IPowerUp
{
    public int durability = 5;
    public AreaShield areaShield;

    public void OnClickPowerUp()
    {
        ResetLoading();
        StartCoroutine(ClaimPowerUp());
    }
    public IEnumerator ClaimPowerUp()
    {
        button.interactable = false;
        claimed = true;
        AreaShield shield = Instantiate(areaShield, PlayerController.Instance.transform);
        shield.SetDurability(durability);
        SoundManager.Instance.PlaySound("Powerup");

        yield return new WaitForSeconds(timeLimit);

        Destroy(shield.gameObject);

        claimed = false;

        DecreseCount();
        button.interactable = true;
    }

    public void ResetPowerUp()
    {
        StopAllCoroutines();
        if (claimed)
            DecreseCount();
        claimed = false;
        foreach(AreaShield shield in FindObjectsOfType<AreaShield>())
        {
            Destroy(shield.gameObject);
        }
        button.interactable = true;
        //PlayerController.Instance.ReloadBullet();
    }

}
