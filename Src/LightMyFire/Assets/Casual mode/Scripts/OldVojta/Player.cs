using System.Collections;
using System.Collections.Generic;
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
		_objects = new List<KeyValuePair<Vector2, UnityEvent>>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    float right = Input.GetAxisRaw("Horizontal");
	    float up = Input.GetAxisRaw("Vertical");

	    RigidB.velocity = new Vector2(right * Speed, up * Speed);

	    if (Input.GetKey("e"))
            Interact();
	}

    public void RegisterCollider(Vector2 position, UnityEvent ue)
    {
        _objects.Add(new KeyValuePair<Vector2, UnityEvent>(position, ue));
    }

    public void DeleteCollider(UnityEvent ue)
    {
        _objects.RemoveAt(_objects.FindIndex(obj => obj.Value == ue));
    }

    public void Interact()
    {
        if (_objects.Count == 0)
            return;

        float min = float.MaxValue;
        int k = 0;
        for (int i = 0; i < _objects.Count; ++i)
        {
            float dist = Vector2.Distance(_objects[i].Key, RigidB.position);
            if (dist < min)
            {
                dist = min;
                k = i;
            }
        }

        _objects[k].Value.Invoke();
        _objects.RemoveAt(k);
    }

    private List<KeyValuePair<Vector2, UnityEvent>> _objects;
}
