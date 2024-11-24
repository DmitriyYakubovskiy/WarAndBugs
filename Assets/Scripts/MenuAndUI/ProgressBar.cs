using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MenuAndUI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private GameObject barObject;
        [SerializeField] private RectTransform healthLine;
        private float maxPoints;
        private float fill = 1;

        public float MaxPoints { get => maxPoints; set => maxPoints = value; }

        private void Start()
        {
            fill = 1f;
        }

        private void FixedUpdate()
        {
            if (fill >= 1f)
            {
                barObject.SetActive(false);
            }
            if (fill < 1f)
            {
                barObject.SetActive(true);
            }

            if (healthLine.GetComponent<Image>().fillAmount != fill)
            {
                healthLine.GetComponent<Image>().fillAmount = fill;
            }
        }

        public void ChangePoints(float points)
        {
            fill = points / maxPoints;
        }

        public void DeleteHealthBar()
        {
            Destroy(barObject);
        }
    }
}
