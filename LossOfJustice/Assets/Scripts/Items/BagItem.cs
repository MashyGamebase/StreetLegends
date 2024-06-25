using UnityEngine;

public class BagItem : ItemCollectible
{
    public float rotationSpeed = 10f;
    public float floatSpeed = 0.5f;
    public float amplitude = 1.0f;
    public Transform visual;


    private void Update()
    {
        Visual();
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    void Visual()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.Self);
        visual.position = new Vector3(visual.position.x, amplitude * (Mathf.Sin(Time.time * floatSpeed)), visual.position.z);
    }
}