using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    Bounds m_bounds;

    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();
        if (null == renderer)
        {
            Debug.LogError("RepeatBackground with no renderer");
            Destroy(this);
        }
        m_bounds = renderer.bounds;
    }

    // Update is called once per frame
    void Update()
    {
        {   // TODO add new tiles to fill to the right
            Transform rightChild = transform.GetChild(transform.childCount-1);
            Renderer rightRender = rightChild.GetComponent<Renderer>();
            Bounds rightBounds = rightRender.bounds;
            Vector3 viewRight = Camera.main.WorldToViewportPoint(rightBounds.max);
            if (viewRight.x < 1.0f){
                // need a new tile
                GameObject newTile = Instantiate(rightChild.gameObject);
                Vector3 new_pos = rightChild.position;
                new_pos.x += m_bounds.size.x;
                newTile.transform.position = new_pos;
                newTile.transform.SetParent(transform);
            }
        }

        {   // TODO remove tiles on the left
            Transform leftChild = transform.GetChild(0);
            Renderer leftRender = leftChild.GetComponent<Renderer>();
            Bounds leftBounds = leftRender.bounds;
            Vector3 viewLeft = Camera.main.WorldToViewportPoint(leftBounds.max);
            if (viewLeft.x < 0.0f){
                Destroy(leftChild.gameObject);
            }

        }
    }
}
