using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveCoin : MonoBehaviour
{
    public Item item;
    public int unit = 1;

    void Start()
    {
        
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit))
        {
            //Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
            //Debug.Log(hit.transform.name);
            if (hit.distance <= 5)
            {
                if (hit.transform.gameObject == this.gameObject)
                {
                    item.unit += unit;
                    Destroy(this.gameObject);
                }
            }
        }

    }
}


