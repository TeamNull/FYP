using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emitPoint : MonoBehaviour
{

    public int theRange = 15;
    public GameObject theArrow;
    Vector3 theEmitPoint = Vector3.zero;
    //Vector3 theDirection = Vector3.zero;
    //Vector3 theTargetPoint = Vector3.zero;
    //PlayerAttack playerAttack;
    //GameObject player;
    //RaycastHit theHit;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        theEmitPoint = transform.position;

    }

    void Update()
    {

    }

    public void AttackByShoot()
    {
        StartCoroutine(ArrowOut(0.2f));
    }

    public void AttackByTripleShoot()
    {
        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(ArrowOut(0.4f*i));
        }
    }

    public void AttackByJumpShoot()
    {
        StartCoroutine(ArrowOut(0.6f));
    }

    IEnumerator ArrowOut(float second)
    {
        yield return new WaitForSeconds(second);
        GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("ArcheryAttack");
        GameObject cloneArrow = Instantiate(theArrow, theEmitPoint, Quaternion.identity);
        cloneArrow.transform.position = theEmitPoint;
        cloneArrow.transform.rotation = transform.rotation;
    }
}
