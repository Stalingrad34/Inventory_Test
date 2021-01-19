using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeItem : Item
{
    protected override void AttachToInventory(Inventory section)
    {
        var rigidBody = GetComponent<Rigidbody>();
        var target = section.GetItemPosition(Model);
        if (!target)
            return;
        
        rigidBody.isKinematic = true;
        StartCoroutine(Coroutine());
        
        IEnumerator Coroutine()
        {
            while (Vector3.Distance(transform.position, target.transform.position) > 0.1f)
            {
                GetComponent<Rigidbody>().position = Vector3.MoveTowards(transform.position, target.transform.position, 0.1f);
                yield return new WaitForEndOfFrame();
            }

            rigidBody.isKinematic = false;
            target.AttachItem(this);
        }
        
        isAttached = true;
        onItemAttached.Invoke();
    }

    public override void DetachToInventory()
    {
        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.isKinematic = true;
        StartCoroutine(Coroutine());
        
        IEnumerator Coroutine()
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20));
            while (Vector3.Distance(transform.position, mousePosition) > 0.1f)
            {
                rigidBody.position = Vector3.MoveTowards(transform.position, mousePosition, 0.1f);
                yield return new WaitForEndOfFrame();
            }

            rigidBody.isKinematic = false;
            isAttached = false;
            onItemDetached.Invoke();
        }
        
        
    }
}
