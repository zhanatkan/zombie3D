using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

public class BonusIconSystem : IEcsRunSystem
{
    private EcsFilter<ShieldIcon> shieldIcons;
    private EcsFilter<PowerIcon> powerIcons;
    private EcsFilter<SpeedIcon> speedIcons;

    public void Run()
    {
        foreach (var i in shieldIcons)
        {
            ref var shieldIcon = ref shieldIcons.Get1(i);
            shieldIcon.timer -= Time.deltaTime;

            if (shieldIcon.timer <= 0)
            {
                shieldIcon.iconObject.SetActive(false);
                shieldIcons.GetEntity(i).Destroy();
            }
            else
            {
                var image = shieldIcon.iconObject.GetComponent<Image>();
                if (image != null)
                {
                    image.fillAmount = shieldIcon.timer / 5f;
                }
            }
        }
        foreach (var i in powerIcons)
        {
            ref var powerIcon = ref powerIcons.Get1(i);
            powerIcon.timer -= Time.deltaTime;

            if (powerIcon.timer <= 0)
            {
                powerIcon.iconObject.SetActive(false);
                powerIcons.GetEntity(i).Destroy();
            }
            else
            {
                var image = powerIcon.iconObject.GetComponent<Image>();
                if (image != null)
                {
                    image.fillAmount = powerIcon.timer / 5f;
                }
            }
        }
        foreach (var i in speedIcons)
        {
            ref var speedIcon = ref speedIcons.Get1(i);
            speedIcon.timer -= Time.deltaTime;

            if (speedIcon.timer <= 0)
            {
                speedIcon.iconObject.SetActive(false);
                speedIcons.GetEntity(i).Destroy();
            }
            else
            {
                var image = speedIcon.iconObject.GetComponent<Image>();
                if (image != null)
                {
                    image.fillAmount = speedIcon.timer / 5f;
                }
            }
        }
    }
}