using UnityEngine;

public class Safe : MonoBehaviour
{
    [SerializeField] GameObject pic;

    private void Start()
    {
        pic.SetActive(false);
    }  
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            pic.SetActive(true);
    }
}
