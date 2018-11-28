using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Events;


public class Player : MonoBehaviour
{
    public Rigidbody2D RigidB;
    public float Speed;

	// Use this for initialization
	void Start ()
	{
		_objects = new List<KeyValuePair<Vector2, Trigger>>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (canBeManipulated)
	    {
	        float right = Input.GetAxisRaw("Horizontal");
	        float up = Input.GetAxisRaw("Vertical");

	        RigidB.velocity = new Vector2(right * Speed, up * Speed);

	        if (Input.GetKeyDown("e"))
	            Interact();
        }
	}

    public void RegisterCollider(Vector2 position, Trigger ue)
    {
        _objects.Add(new KeyValuePair<Vector2, Trigger>(position, ue));
    }

    public void DeleteCollider(Trigger ue)
    {
        int index = _objects.FindIndex(obj => obj.Value == ue);
        if (index >= 0)
            _objects.RemoveAt(index);
    }

    public void Interact()
    {
        if (Interlocked.CompareExchange(ref interacting, 1, 0) == 0)
        {
            if (_objects.Count == 0)
                return;

            float min = float.MaxValue;
            int k = 0;
            for (int i = 0; i < _objects.Count; ++i)
            {
                float dist = Vector2.Distance(_objects[i].Key, RigidB.position);
                if (dist < min && _objects[i].Value.Inactive == 0)
                {
                    min = dist;
                    k = i;
                }
            }

            if (Interlocked.CompareExchange(ref _objects[k].Value.Inactive, 1, 0) == 0)
            {
                _objects[k].Value.UE.Invoke();
            }
            interacting = 0;
        }
    }

    public void Stop()
    {
        canBeManipulated = false;
    }

    public void Resume()
    {
        canBeManipulated = true;
    }

    private bool canBeManipulated = true;
    private int interacting = 0;
    private List<KeyValuePair<Vector2, Trigger>> _objects;
}
