using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public void Shot(Transform target, Action onHit)
    {
        transform.DOMove(target.position, 0.5f).OnComplete(() =>
        {
            onHit?.Invoke();
            Destroy(this.gameObject);
        }).OnUpdate(() =>
        {
            Vector3 diff = target.position - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }).SetEase(Ease.Linear);
    }
}