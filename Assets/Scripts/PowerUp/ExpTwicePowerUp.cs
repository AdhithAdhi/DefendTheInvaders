using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpTwicePowerUp:PowerUpUi, IPowerUp
{
    public int expValue = 2;

    public void OnClickPowerUp()
    {
        ResetLoading();
        StartCoroutine(ClaimPowerUp());
    }
    public IEnumerator ClaimPowerUp()
    {
        button.interactable = false;
        claimed = true;
        var temp = PlayerController.Instance.fireRate;
        Instantiate(PowerUpManager.Instance.powerUpEffect, transform);
        SoundManager.Instance.PlaySound("Powerup");
        GameManager.Instance.expForEnemy *= expValue;
        yield return new WaitForSeconds(timeLimit);

        claimed = false;
        GameManager.Instance.expForEnemy /= expValue;
        DecreseCount();
        button.interactable = true;
    }

    public void ResetPowerUp()
    {
        StopAllCoroutines();
        if (claimed)
            DecreseCount();
        GameManager.Instance.expForEnemy /= expValue;
        claimed = false;
        button.interactable = true;
    }
}
