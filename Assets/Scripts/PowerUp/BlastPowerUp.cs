using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BlastPowerUp: PowerUpUi, IPowerUp
{
    public float fireRate = 0f;
    public void OnClickPowerUp()
    {
        ResetLoading();
        StartCoroutine(ClaimPowerUp());
    }

    public IEnumerator ClaimPowerUp()
    {
        button.interactable = false;
        claimed = true;
        PlayerController.Instance.bulletType = BulletType.Bomb;
        PlayerController.Instance.ReloadBullet();
        Instantiate(PowerUpManager.Instance.powerUpEffect, transform);
        SoundManager.Instance.PlaySound("Powerup");
        PlayerController.Instance.fireRate = fireRate;

        yield return new WaitForSeconds(timeLimit);

        claimed = false;
        PlayerController.Instance.bulletType = BulletType.Bullet;
        PlayerController.Instance.fireRate = PlayerController.Instance.defaultFireRate;
        DecreseCount();
        button.interactable = true;
        PlayerController.Instance.ReloadBullet();
    }
    public void ResetPowerUp()
    {
        StopAllCoroutines();
        if (claimed)
            DecreseCount();
        claimed = false;
        PlayerController.Instance.bulletType = BulletType.Bullet;
        PlayerController.Instance.fireRate = PlayerController.Instance.defaultFireRate;
        button.interactable = true;
        //PlayerController.Instance.ReloadBullet();
    }
}
