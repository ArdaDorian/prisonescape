using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireEventSystem : MonoBehaviour
{
    [SerializeField] Transform bulletSpawner;
    Object arrow;

    private void Awake()
    {
        arrow = Resources.Load("arrow");
    }

    public void FireArrow()
    {
        var newArrow = (GameObject) Instantiate(arrow, bulletSpawner.position, bulletSpawner.rotation);
        newArrow.transform.localScale = -transform.localScale;
    }
}
