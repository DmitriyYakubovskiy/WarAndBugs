using UnityEngine;

public class ImprovementSystem : Sound
{
    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] private GameObject[] allGuns;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject player;

    private int[] indexes=new int[3];
    private bool check=false;

    private void Update()
    {
        if (panel.activeSelf)
        {
            Pause.PauseGame();
        }
        if (panel.activeSelf && check != true)
        {
            PlaySound(0,2);
            check = true;
            System.Random random = new System.Random();
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }
            //if (player.GetComponent<Player>().Level % 10 == 0)
            //{
            //    indexes[0] = random.Next(0, availableGuns.Count);
            //    indexes[1] = random.Next(0, availableGuns.Count);
            //    while (indexes[1] == indexes[0])
            //    {
            //        indexes[1] = (indexes[1] + 1) % availableGuns.Count;
            //    }
            //    indexes[2] = random.Next(0, availableGuns.Count);
            //    while (indexes[2] == indexes[1] || indexes[2] == indexes[0])
            //    {
            //        indexes[2] = (indexes[2] + 1) % availableGuns.Count;
            //    }
            //    for (int i = 0; i < indexes.Length; i++)
            //    {
            //        availableButtonsGuns[indexes[i]].SetActive(true);
            //        availableButtonsGuns[indexes[i]].GetComponent<RectTransform>().localPosition = new Vector3(0, 130 - 130 * i, 0);
            //    }
            //}
            //else
            //{
                indexes[0] = random.Next(0, gameObjects.Length);
                indexes[1] = random.Next(0, gameObjects.Length);
                while (indexes[1] == indexes[0])
                {
                    indexes[1] = (indexes[1] + 1) % gameObjects.Length;
                }
                indexes[2] = random.Next(0, gameObjects.Length);
                while (indexes[2] == indexes[1] || indexes[2] == indexes[0])
                {
                    indexes[2] = (indexes[2] + 1) % gameObjects.Length;
                }
                for (int i = 0; i < indexes.Length; i++)
                {
                    buttons[indexes[i]].SetActive(true);
                    buttons[indexes[i]].GetComponent<RectTransform>().localPosition = new Vector3(0, 130 - 130 * i, 0);
                }
            //}
        }
    }

    public void ButtonClick(int index)
    {
        //if (player.GetComponent<Player>().Level % 10 == 0)
        //{
        //}
        //else
        //{
            gameObjects[index].gameObject.SetActive(true);
        //}
        check = false;
        Pause.ContinueGame();           
        panel.SetActive(false); 
    }
}
