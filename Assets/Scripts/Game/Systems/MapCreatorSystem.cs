using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreatorSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    [SerializeField] private int[] counts;
    [SerializeField] private float rangeTop;
    [SerializeField] private float rangeRight;
    [SerializeField] private float rangeBottom;
    [SerializeField] private float rangeLeft;

    private void Start()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            for (int j = 0; j < counts[i]; j++)
            {
                var @object = Instantiate(objects[i]);
                @object.transform.position = GetRandomPosition();
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        int i = 1000;
        while (i >= 0)
        {
            var position = new Vector3(Random.Range(rangeLeft, rangeRight), Random.Range(rangeBottom, rangeTop));

            if (!Physics2D.OverlapCircle(position, 0.1f))
            {
                return position;
            }
            i--;
        }
        return Vector3.zero - new Vector3(1, 1);
    }
}
