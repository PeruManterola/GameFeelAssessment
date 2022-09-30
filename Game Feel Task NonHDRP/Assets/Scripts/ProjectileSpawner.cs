using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.UI;

public class ProjectileSpawner : MonoBehaviour
{
    public int rocketSpeed = 5;
    [HideInInspector]
    public int rocketForce;
    [HideInInspector]
    public int rocketExplosionRadius;
    [HideInInspector]
    public bool applyForce;
    [HideInInspector]
    public bool bigExplosion;

    public MMF_Player feedbacks;
    private Pooler rocketPool;
    public Projectile data;

    private bool spawnAnimation = true;
    public bool SpawnEffect { get { return spawnAnimation; } set { spawnAnimation = value; } }
    private bool thrusterSound = true;
    public bool ThrusterIsOn { get { return thrusterSound; } set { thrusterSound = value; } }
    private bool playParticles = true;
    public bool PlayParticles { get { return playParticles; } set { playParticles = value; } }

    private void Start()
    {
        rocketPool = GetComponent<Pooler>();
        rocketForce = data.force;
        rocketExplosionRadius = data.explosionRadius;
        applyForce = data.applyForce;
        bigExplosion = data.bigExplosion;
    }

    public void SpawnRocket(Button button)
    {
        StartCoroutine(DisableButtonIE(button));
        GameObject rocket = rocketPool.GetObject();
        if (spawnAnimation)
        {
            StartCoroutine(RocketSpawnIE(rocket));
        }
        else
        {
            Projectile projectile = rocket.GetComponent<Projectile>();
            rocket.transform.position = transform.position;
            rocket.transform.rotation = transform.rotation;
            rocket.SetActive(true);
            SetupRocket(projectile);
            projectile.ThusterSound(thrusterSound);
            projectile.Launch(rocketSpeed);
        }
    }

    private IEnumerator DisableButtonIE(Button button)
    {
        button.interactable = false;
        yield return new WaitForSeconds(4.5f);
        button.interactable = true;
    }

    public IEnumerator RocketSpawnIE(GameObject g)
    {
        Projectile projectile = g.GetComponent<Projectile>();
        g.transform.position = transform.position;
        g.transform.rotation = transform.rotation;
        g.SetActive(true);
        projectile.spawning = true;
        yield return new WaitForSeconds(3f);
        projectile.spawning = false;
        SetupRocket(projectile);
        projectile.ThusterSound(thrusterSound);
        projectile.ResetCutoff();

        projectile.Launch(rocketSpeed);
    }

    private void SetupRocket(Projectile projectile)
    {
        projectile.spawnParticles = playParticles;
        projectile.Feedbacks = feedbacks;
        projectile.force = rocketForce;
        projectile.explosionRadius = rocketExplosionRadius;
        projectile.applyForce = applyForce;
        projectile.bigExplosion = bigExplosion;
    }
}
