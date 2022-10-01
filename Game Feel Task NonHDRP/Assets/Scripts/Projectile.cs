using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    public float lifeTime = 10f;
    public bool applyForce;
    public bool bigExplosion;
    public int explosionRadius = 30;
    public int force = 50;
    public GameObject explosionParticle;
    public Transform explosionPoint;
    private MMF_Player feedbacks;
    public MMF_Player Feedbacks { set { feedbacks = value; } }
    private Pooler pool;
    private ParticleSystem exhaustParticle;
    private MeshRenderer meshRenderer;
    private AudioSource audioSource;
    private TrailRenderer trail;
    public bool spawning;
    public bool spawnParticles = true;
    [SerializeField]
    private float cutoff = 6;
    private float minCutOff;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        exhaustParticle = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponentInChildren<AudioSource>();
        trail = GetComponentInChildren<TrailRenderer>();
    }

    private void Start()
    {
        pool = transform.parent.GetComponent<Pooler>();
        minCutOff = -cutoff;
        meshRenderer.sharedMaterial.SetFloat("_CutoffHeight", cutoff);

    }

    private void Update()
    {
        if (spawning)
        {
            minCutOff = Mathf.Lerp(minCutOff, cutoff, Time.deltaTime);
            meshRenderer.sharedMaterial.SetFloat("_CutoffHeight", minCutOff);
        }
    }

    //Resets cutoff value for the next spawn
    public void ResetCutoff()
    {
        minCutOff = -cutoff;
    }

    //Launches the projectile
    public void Launch(int speed)
    {
        if (spawnParticles)
        {
            trail.enabled = true;
            exhaustParticle.Play();
        }
        else
        {
            trail.enabled = false;
        }

        rb.AddForce(Vector3.back * speed, ForceMode.Acceleration);
        audioSource?.Play();
    }

    //Returns the projectile to the pool 
    public void KillProjectile()
    {
        audioSource.Stop();
        rb.velocity = Vector3.zero;
        pool.ReturnObject(this.gameObject);
    }

    public void ThusterSound(bool value)
    {
        audioSource.mute = !value;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Spawn explosion particles
        if (spawnParticles)
        {
            GameObject explosion = Instantiate(explosionParticle);
            explosion.transform.position = explosionPoint.position;
        }

        //Play feedbacks if there are any
        feedbacks?.PlayFeedbacks();

        //Make one type of explosion or another depending on settings
        if (other.CompareTag("Target") && applyForce && !bigExplosion)
        {
            other.gameObject.GetComponent<TargetReset>().ResetPosition();
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * force, ForceMode.Impulse);
        }
        else if (other.CompareTag("Target") && bigExplosion)
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                hit.gameObject.GetComponent<TargetReset>()?.ResetPosition();
                if (rb != null)
                    rb.AddExplosionForce(force * 15, explosionPos + transform.up * -4, explosionRadius, 8f);
            }
        }
        KillProjectile();
    }
}
