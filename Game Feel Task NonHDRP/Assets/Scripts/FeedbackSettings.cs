using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
using MoreMountains.FeedbacksForThirdParty;
using System.Linq;

public class FeedbackSettings : MonoBehaviour
{
    public Toggle soundToggle;
    public Toggle cameraToggle;
    public Toggle squashStretchToggle;
    public Toggle spawnToggle;
    public Toggle physicsToggle;
    public Toggle particlesToggle;
    public List<Rigidbody> targetRigidbodies = new List<Rigidbody>();
    private List<ProjectileSpawner> projectileSpawners;
    private bool allFeedbackState = true;

    private void Awake()
    {
        projectileSpawners = new List<ProjectileSpawner>();
        projectileSpawners = FindObjectsOfType<ProjectileSpawner>().ToList();
    }

    public void DisableSoundFeedback()
    {
        foreach (ProjectileSpawner spawner in projectileSpawners)
        {
            spawner.ThrusterIsOn = soundToggle.isOn;
        }
        MMF_Sound.FeedbackTypeAuthorized = soundToggle.isOn;
    }

    public void DisableCameraFeedback()
    {
        MMF_CameraShake.FeedbackTypeAuthorized = cameraToggle.isOn;
        MMF_Flash.FeedbackTypeAuthorized = cameraToggle.isOn;
        MMF_ChromaticAberration.FeedbackTypeAuthorized = cameraToggle.isOn;
        MMF_LensDistortion.FeedbackTypeAuthorized = cameraToggle.isOn;
        MMF_CameraFieldOfView.FeedbackTypeAuthorized = cameraToggle.isOn;
    }

    public void DisableSquashStretchFeedback()
    {
        MMF_SquashAndStretch.FeedbackTypeAuthorized = squashStretchToggle.isOn;
    }

    public void DisableSpawnEffect()
    {
        foreach (ProjectileSpawner spawner in projectileSpawners)
        {
            spawner.SpawnEffect = spawnToggle.isOn;
        }
    }

    public void DisableTargetPhysics()
    {
        foreach (Rigidbody rigidbody in targetRigidbodies)
        {
            rigidbody.isKinematic = !physicsToggle.isOn;
        }
    }

    public void DisableParticles()
    {
        foreach (ProjectileSpawner spawner in projectileSpawners)
        {
            spawner.PlayParticles = particlesToggle.isOn;
        }
    }

    public void SwitchAllToggles()
    {
        if (allFeedbackState)
        {
            soundToggle.isOn = false;
            cameraToggle.isOn = false;
            squashStretchToggle.isOn = false;
            spawnToggle.isOn = false;
            physicsToggle.isOn = false;
            particlesToggle.isOn = false;

            allFeedbackState = false;
        }
        else
        {
            soundToggle.isOn = true;
            cameraToggle.isOn = true;
            squashStretchToggle.isOn = true;
            spawnToggle.isOn = true;
            physicsToggle.isOn = true;
            particlesToggle.isOn = true;

            allFeedbackState = true;
        }

    }
}
