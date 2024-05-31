using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GunController : MonoBehaviour
{

    [SerializeField]
    private Gun currentGun;
    private float currentFireRate;
        private bool isReload = false;
    public bool isFineSightMode = false;
    

    public Score Scoreup;

    [SerializeField]
    private Vector3 originPos;

    private AudioSource audioSource;

    public RaycastHit hitinfo;

    [SerializeField]
    private Camera theCam;

    [SerializeField]
    private GameObject hit_effect_prefab;

    public Score ScoreupGameObject;

    public TargetHit targetHit;

    // Material mat;

    void Start()
    {
        originPos = Vector3.zero;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GunFireRateCalc();
        TryFire();
        TryReload();
        // TryFineSight();

    }
   
    private void GunFireRateCalc()
    {
        if (currentFireRate > 0)
            currentFireRate -= Time.deltaTime;
    }

    private void TryFire()
    {
        if (Input.GetButton("Fire1") && currentFireRate <= 0 && !isReload)
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (!isReload)
        {
            if (currentGun.currentBulletCount > 0)
                Shoot();
            else
            {
           //     CancelFineSight();
                StartCoroutine(ReloadCoroutine());
            }


        }
    }

    private void Shoot()
    {
        currentGun.currentBulletCount--;
        currentFireRate = currentGun.fireRate; // ���� �ӵ� ����.
        PlaySE(currentGun.fire_Sound);
        currentGun.muzzleFlash.Play();
        //Hit();
        StopCoroutine(ReloadCoroutine());
       // StartCoroutine(RetroActionCoroutine());

    }
    /*
    public void Hit()
    {
        
       
        //Material hitmat = hitinfo.transform.GetComponent<MeshRenderer>().material;
        if (Physics.Raycast(theCam.transform.position, theCam.transform.forward, out hitinfo, currentGun.range)) 
        {
            GameObject clone = Instantiate(hit_effect_prefab, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
            Destroy(clone, 2f);

            bool matchtarget = hitinfo.transform.CompareTag("Real Target");
            bool falseTarget = hitinfo.transform.CompareTag("Target");
            bool wallHit = hitinfo.transform.CompareTag("Wall");
            
            for (int i = 0; i < targetHit.TargetsToHit.Length; i++)
            {
                targetHit.TargetsToHit[i].GetComponent<MeshRenderer>().material.color = Color.white;

            }

                //Debug.Log(hitinfo.transform.name);
                if (matchtarget)
                {

                    StartCoroutine(HitCorrect());
                    Debug.Log("Correct!");
                    
                    Material Tarmat = hitinfo.transform.GetComponent<MeshRenderer>().materials[0];
                    Tarmat.color = Color.green;
                    //mat.color = Color.green;
                    ScoreupGameObject.Addscore(1);


                }
                if (falseTarget)
                {
                    StartCoroutine(HitMiss());
                    Debug.Log("Incorrect!");
                    ScoreupGameObject.Miss(1);
                }
                else
                {
                    return;
                }

            
        }       
    }
    //This Part is related to Color Thing. and we don't need anymore those one.
    
    IEnumerator HitCorrect()
    {
        
        yield return new WaitForSecondsRealtime(0.3f);
        Material Tarmat = hitinfo.transform.GetComponent<MeshRenderer>().materials[0];
        Tarmat.color = Color.white;
        targetHit.SetTarget();
    }
    IEnumerator HitMiss()
    {

        Material Tarmat = hitinfo.transform.GetComponent<MeshRenderer>().materials[0];
        Tarmat.color = Color.red;

        yield return new WaitForSecondsRealtime(0.3f);


        Tarmat.color = Color.white;

    }

    */


    private void TryReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReload && currentGun.currentBulletCount < currentGun.reloadBulletCount)
        {
            //CancelFineSight();
            StartCoroutine(ReloadCoroutine());
        }
    }

    IEnumerator ReloadCoroutine()
    {
        if (currentGun.carryBulletCount > 0)
        {
            isReload = true;

            currentGun.anim.SetTrigger("Reload");


            currentGun.carryBulletCount += currentGun.currentBulletCount;
            currentGun.currentBulletCount = 0;

            yield return new WaitForSeconds(currentGun.reloadTime);

            if (currentGun.carryBulletCount >= currentGun.reloadBulletCount)
            {
                currentGun.currentBulletCount = currentGun.reloadBulletCount;
                currentGun.carryBulletCount -= currentGun.reloadBulletCount;
            }
            else
            {
                currentGun.currentBulletCount = currentGun.carryBulletCount;
                currentGun.carryBulletCount = 0;
            }


            isReload = false;
        }
        else
        {
            Debug.Log("Bullet all Used.");
        }

        

    }
    
    private void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }

    public Gun GetGun()
    {
        return currentGun;
    }
}


