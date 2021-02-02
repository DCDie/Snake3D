using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{

    public List<Transform> Tails;
    [Range(0, 3)]
    public float Dist;
    public GameObject Bone;

    [Range(0,4)]
    public float Speed; 

    private Transform _transform;

    public float thrust = 1.0f;
    public Rigidbody rb;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(0, 0, thrust, ForceMode.Impulse);
    }

    private void Update()
    {
        MoveSnake(_transform.position + _transform.forward * Speed);

        float ang = Input.GetAxis("Horizontal");
        _transform.Rotate(0, ang, 0);
    }

    private void MoveSnake(Vector3 newPosition)
    {
        float dist = Dist * Dist;
        Vector3 previousPosition = _transform.position;

        foreach (var bone in Tails)
        {
            if ((bone.position - previousPosition).sqrMagnitude > dist)
            {
                var temp = bone.position;
                bone.position = previousPosition;
                previousPosition = temp;
            }
            else
            {
                break;
            }
        }

        _transform.position = newPosition;

    }

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Food")
        {
            Destroy(coll.gameObject);
            var bone = Instantiate(Bone);
            Tails.Add(bone.transform);
            Speed = Speed + 0.001f;
        }
    }
}
