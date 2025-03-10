using Assets.Scripts.MenuAndUI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DashController : Sound 
{
    [SerializeField] private GameObject DashImageObject;
    [SerializeField] private GameObject Separator;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private float dashPower = 2;
    [SerializeField] private float startTime = 10f;
    private Player player;
    private SpriteRenderer DashImage;
    private float time = 0;
    private int maxCount;
    private int currentCount;
    private float startTimeImage = 0.2f;
    private float timeImage = 0;
    private List<float> dashTimes = new List<float>();

    private void Start()
    {
        if (SaveSystem.Data.ShopItemNames["Dash"] != 0)
        {
            this.enabled = true;
            maxCount = SaveSystem.Data.ShopItemNames["Dash"];
            currentCount = maxCount;
            if (maxCount == 1)
            {
                Separator.SetActive(false);
            }
        }
        else this.enabled = false;

        for (int i = 0; i < maxCount; i++)
        {
            dashTimes.Add(startTime);
        }

        time = startTime * maxCount;
        progressBar.MaxPoints = startTime * maxCount;
        DashImage = DashImageObject.GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentCount>0) Dash();

        RechargeDashTime();
        if (time < startTime*maxCount)
        {
            time += Time.deltaTime;
            progressBar.ChangePoints(time);
        }
        RechargeImageTime();
    }

    private void Dash()
    {
        if (player.MoveVector.x == 0 && player.MoveVector.y == 0) return;
        PlaySound(0, volume);
        SetActiveImage();

        time -= startTime;
        for (int i = 0; i < maxCount; i++)
        {
            if (dashTimes[i] >= startTime)
            {
                dashTimes[i] = 0;
                currentCount--;
                break;
            }
        }

        DashImage.flipX = player.MoveVector.x>0?false:true;
        transform.Translate(player.MoveVector * dashPower);
    }

    private void RechargeDashTime()
    {
        var filteredTimes = dashTimes.Where(x => x < startTime).ToList();

        if (filteredTimes.Count == 0) return ;

        var maxTime = filteredTimes.Max();
        for (int i = 0; i < maxCount; i++)
        {
            if (maxTime > dashTimes[i]) continue;

            if (dashTimes[i] < startTime)
            {
                dashTimes[i] += Time.deltaTime;
                if (dashTimes[i] >= startTime && currentCount < maxCount)
                {
                    currentCount++;
                }
                break;
            }
        }
    }

    private void RechargeImageTime()
    {
        if (timeImage > 0 && DashImageObject.activeSelf) timeImage -= Time.deltaTime;
        else DashImageObject.SetActive(false);
    }

    private void SetActiveImage()
    {
        timeImage = startTimeImage;
        DashImageObject.SetActive(true);
    }
}
