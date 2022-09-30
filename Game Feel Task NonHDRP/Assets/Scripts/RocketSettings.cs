using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class RocketSettings : MonoBehaviour
{
    public ProjectileSpawner spawner;
    public Slider speedSlider;
    public Slider forceSlider;
    public Slider radiusSlider;
    public Toggle forceToggle;
    public Toggle explosionToggle;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        Setup();
    }

    public void Setup()
    {
        speedSlider.value = spawner.rocketSpeed;
        forceSlider.value = spawner.rocketForce;
        radiusSlider.value = spawner.rocketExplosionRadius;
        forceToggle.isOn = spawner.applyForce;
        explosionToggle.isOn = spawner.bigExplosion;
    }

    public void SetSettings()
    {

        spawner.rocketSpeed = (int)speedSlider.value;
        spawner.rocketForce = (int)forceSlider.value;
        spawner.rocketExplosionRadius = (int)radiusSlider.value;
        spawner.applyForce = forceToggle.isOn;
        spawner.bigExplosion = explosionToggle.isOn;
    }

    public void ShowSettings()
    {
        rectTransform.DOSizeDelta(new Vector2(328, 216), 0.6f);
        rectTransform.DOAnchorPosY(171, 0.6f);
    }

    public void CloseSettings()
    {
        rectTransform.DOSizeDelta(new Vector2(328, 0), 0.6f);
        rectTransform.DOAnchorPosY(62, 0.6f);
    }

}
