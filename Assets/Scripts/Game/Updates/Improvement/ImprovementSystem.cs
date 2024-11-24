using System;
using System.Linq;
using UnityEngine;

public class ImprovementSystem : Sound
{
    [SerializeField] private Up[] upgradeObjects;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject player;

    private bool check=false;

    private void Update()
    {
        if (panel.activeSelf)
        {
            Pause.PauseGame();
        }
        if (panel.activeSelf && check != true)
        {
            PlaySound(0, volume);
            check = true;

            for (int i = 0; i < upgradeObjects.Length; i++)
            {
                buttons[i].SetActive(false);
            }
            var nums = RandomExceptList(upgradeObjects.Length);
            for(int i=0; i< 3; i++)
            {
                buttons[nums[i]].SetActive(true);
            }
        }
    }

    public static int[] RandomExceptList(int count)
    {
        var nums = Enumerable.Range(0, count).ToArray();
        System.Random random = new System.Random();

        for (int i = 0; i < nums.Length; i++)
        {
            int randomIndex = random.Next(nums.Length);
            int temp = nums[randomIndex];
            nums[randomIndex] = nums[i];
            nums[i] = temp;
        }
        return nums;
    }

    public void ButtonClick(string name)
    {
        upgradeObjects.FirstOrDefault(x => x.UpgradesName == name)?.gameObject.SetActive(true);
        Pause.ContinueGame();
        check = false;
        panel.SetActive(false); 
    }
}
