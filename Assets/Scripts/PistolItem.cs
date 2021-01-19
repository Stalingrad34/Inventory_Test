using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolItem : Item
{
    private Vector3 offset = new Vector3(-4.0f, 0, 0); //Error rate pivot on 3D model Colt
    
    public override void OnMouseDrag()
    {
        if (isAttached)
            return;
        
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20));
        transform.position = mousePosition - offset; 
        transform.rotation = Quaternion.Euler(new Vector3(90, -90, 0));
    }
    
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
            var firstDestinationPoint = target.transform.position + (Vector3.up * 2) - offset;
            while (Vector3.Distance(transform.position, firstDestinationPoint) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, firstDestinationPoint, 0.1f);
                yield return new WaitForEndOfFrame();
            }
            
            var secondDestinationPoint = target.transform.position - offset;
            while (Vector3.Distance(transform.position, secondDestinationPoint) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, secondDestinationPoint, 0.1f);
                
                yield return new WaitForEndOfFrame();
            }
            target.AttachItem(this, false);
        }
        
        isAttached = true;
            onItemAttached.Invoke();
    }

    public override void DetachToInventory()
    {
        var rigidBody = GetComponent<Rigidbody>();
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
