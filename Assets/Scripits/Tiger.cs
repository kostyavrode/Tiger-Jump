using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tiger : MonoBehaviour
{
    public static Action onNewPlatformReached;
    private Rigidbody rb;
    private Animator animator;
    private float jumpPressedTime;
    [SerializeField] private LayerMask layerMask;
    private bool isGroundChecked;
    private float distanceCheck=1;
    [SerializeField] private Material whiteMaterial;
    [SerializeField] private Material secMaterial;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Buy1"))
        {
            GetComponentInChildren<SkinnedMeshRenderer>().material = whiteMaterial;
        }
        if (PlayerPrefs.HasKey("Buy2"))
        {
            GetComponentInChildren<SkinnedMeshRenderer>().material = secMaterial;
        }
            rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            jumpPressedTime += Time.deltaTime;
            if (jumpPressedTime>2)
            {
                jumpPressedTime = 2;
            }
            UIManager.instance.ShowForce(jumpPressedTime/2);
            animator.SetBool("isPreparing", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (jumpPressedTime>0.2f)
            {
                Jump();
                jumpPressedTime = 0;
                UIManager.instance.ShowForce(jumpPressedTime / 2);
                animator.SetTrigger("Jump");
            }
            else
            {
                jumpPressedTime = 0;
                UIManager.instance.ShowForce(jumpPressedTime / 2);
                animator.SetBool("isPreparing", false);
            }
        }
        if (rb.velocity.y>0 && IsPlatformReached() && !isGroundChecked)
        {
            isGroundChecked = true;
            onNewPlatformReached?.Invoke();
            animator.SetBool("isPreparing", false);
        }
    }
    private void Jump()
    {
        rb.AddForce(Vector3.forward * jumpPressedTime * 250);
        rb.AddForce(Vector3.up * jumpPressedTime * 250);
        StartCoroutine(WaitForGroundCheck());
    }
    private void Dead()
    {
        GameManager.instance.EndGame();
    }
    private bool IsPlatformReached()
    {
        Ray ray = new Ray(transform.position,-Vector3.up);
        Debug.DrawRay(ray.origin, ray.direction * distanceCheck);
        return (Physics.Raycast(ray, distanceCheck, layerMask));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="lava")
        {
            Dead();
        }
    }
    private IEnumerator WaitForGroundCheck()
    {
        yield return new WaitForSeconds(1);
        isGroundChecked = false;
        animator.SetBool("isPreparing", false);
    }
}
