using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private bool isGrowing;
    [SerializeField] private GameObject coin;
    private void Awake()
    {
        gameObject.transform.localScale = new Vector3(0.01f,0.01f,0.01f);
        isGrowing = true;
        int rand = Random.Range(0, 4);
        if (rand==2)
        {
            coin.SetActive(true);
        }
    }
    private void Update()
    {
        if (isGrowing && transform.localScale.x<1)
        {
            transform.localScale +=Vector3.one*Time.deltaTime;
        }
        else if (isGrowing)
        {
            transform.localScale = Vector3.one;
            isGrowing = false;
        }
    }
}
