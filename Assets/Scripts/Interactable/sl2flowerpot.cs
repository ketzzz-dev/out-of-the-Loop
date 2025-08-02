using UnityEngine;

public class sl2flowerpot : MonoBehaviour
{

    private SpriteRenderer childSprite;
    public PotInteraction potInteraction;
    void Awake()
    {
        childSprite = GetComponentInChildren<SpriteRenderer>(true);
    }

    void Update()
    {

        if (potInteraction.hasFlower)
        {
            // Enable the GameObject of the sprite.
            childSprite.gameObject.SetActive(true);

            // This line ensures the sprite itself is enabled for rendering.
            childSprite.enabled = true;
        }
    }

}
