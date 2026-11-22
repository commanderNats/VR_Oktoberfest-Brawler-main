using UnityEngine;
public class Rotation : MonoBehaviour
{
    [SerializeField] float velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, velocity, 0) * Time.deltaTime);
    }
}
