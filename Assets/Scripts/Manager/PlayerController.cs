using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0f;
    public float minBound = 0f;
    public float maxBound = 0f;
    public BulletType bulletType;
    public Transform bulletSpwanPos;
    public float defaultFireRate = 0f;
    public float fireRate = 0f;
    float nextFire = 0f;
    Animator animator;
    bool isDead = false;
    public int MagCapacity = 10;
    int BulletCount = 0;
    public float ReloadingTime = 2f;
    bool isReloading = false;
    public static PlayerController Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
             Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        fireRate = defaultFireRate;
        ReloadBullet();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }
        /*float h = Input.GetAxis("Horizontal")*/
        float h = Joystick.Instance.Horizontal;

        if (h > 0.1f)
        {
            animator.SetFloat("Horizontal", 1f);
        }
        else if (h < -0.1f)
        {
            animator.SetFloat("Horizontal", -1f);
        }
        else
        {
            animator.SetFloat("Horizontal", 0f);
        }
        transform.position += Vector3.right * h * speed;
        var tempPos = transform.position;
        if (transform.position.x<=minBound && h < 0)
        {
            h = 0;
            tempPos.x = minBound;
            transform.position = tempPos;
        }
        else if(transform.position.x >= maxBound && h > 0)
        {
            h = 0;
            tempPos.x = maxBound;
            transform.position = tempPos;
        }

    }
    void EnemyShoot()
    {
        Debug.DrawRay(bulletSpwanPos.position, Vector3.forward * 15);
        RaycastHit hit ;
        Physics.Raycast(bulletSpwanPos.position, Vector3.forward, out hit, Mathf.Infinity);
        if (hit.collider != null)
        {
            if(hit.collider.CompareTag("Enemy"))
            {
                if (EnemyMovement.Instance.canFire)
                {
                    StartCoroutine(EnemyMovement.Instance.Pause());
                    hit.collider.gameObject.GetComponent<EnemyController>().FireBullet();
                }

            }
        }
    }
    public bool IsDead()
    {
        return isDead;
    }
    void Update()
    {
        if (isDead)
        {
            return;
        }
        if (isReloading == false)
        {

            if (Input.touches.Length > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.tapCount > 1)
                    {
                        if (Time.time > nextFire)
                        {
                            BulletCount++;
                            GameManager.Instance.SetBulletCount(BulletCount, MagCapacity);
                            nextFire = Time.time + fireRate;
                            if (bulletType == BulletType.Bullet)
                            {
                                GameObject bullet = ObjectPoolController.Instance.SpwanFromPool("Bullet");
                                bullet.transform.position = bulletSpwanPos.position;
                            }
                            else
                            {
                                GameObject bullet = ObjectPoolController.Instance.SpwanFromPool("AreaBlast");
                                bullet.transform.position = bulletSpwanPos.position;
                            }
                            SoundManager.Instance.PlaySound("Fire");
                        }
                        break;
                    }
                }
            }
            if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
            {
                BulletCount++;
                GameManager.Instance.SetBulletCount(BulletCount, MagCapacity);
                nextFire = Time.time + fireRate;
                animator.SetTrigger("Fire");
                if (bulletType == BulletType.Bullet)
                {
                    GameObject bullet = ObjectPoolController.Instance.SpwanFromPool("Bullet");
                    bullet.transform.position = bulletSpwanPos.position;
                }
                else
                {
                    GameObject bullet = ObjectPoolController.Instance.SpwanFromPool("AreaBlast");
                    bullet.transform.position = bulletSpwanPos.position;
                }
                SoundManager.Instance.PlaySound("Fire");
            }
        }
        if (BulletCount >= MagCapacity)
        {
            ReloadBullet();
        }
        EnemyShoot();
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        animator.SetTrigger("Reload");
        BulletCount = 0;
        GameManager.Instance.ReloadBullet();
        yield return new WaitForSeconds(ReloadingTime);
        GameManager.Instance.SetBulletCount(BulletCount, MagCapacity);
        isReloading = false;
    }

    public void ReloadBullet()
    {
        StopCoroutine(Reload());

        StartCoroutine(Reload());
    }
    public void PlayerDead()
    {
        isDead = true;
        animator.SetTrigger("Dead");
    }
    public void PlayerRevive()
    {
        isDead = false;
        ReloadBullet();
        Health.Instance.HealthIncreased();
        animator.Play(Animator.StringToHash("Movement"));

    }
}
