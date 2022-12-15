using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float moveSpeed;
    public float steerSpeed;
    public int Gap;

    public GameObject bodyPrefab;
    List<GameObject> bodyParts = new List<GameObject> ();
    List<Vector3> bodyPositions = new List<Vector3> ();

    private void Start()
    {
        Grow();
    }
    void Update()
    {
        transform.position -= transform.forward * moveSpeed * Time.deltaTime;

        float steerDirection = Input.GetAxis("Horizontal");

        transform.Rotate(Vector3.up * steerDirection * steerSpeed * Time.deltaTime);

        bodyPositions.Insert(0, transform.position);
        int id = 0;
        foreach (var part in bodyParts)
        {
            Vector3 point = bodyPositions[Mathf.Clamp(id++ * Gap, 0, bodyPositions.Count - 1)];
            Vector3 moveDirection = point - part.transform.position;
            part.transform.position += moveDirection * moveSpeed * Time.deltaTime;
            part.transform.LookAt(point);
        }
    }

    private void Grow()
    {
        GameObject body = Instantiate(bodyPrefab);
        bodyParts.Add(body);
    }    
}
