using UnityEngine;

public class RandomColorChanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float timer = 0f;
    private float changeInterval = 1f;

    // Colores configurables desde el inspector
    public Color color1 = Color.red;
    public Color color2 = Color.green;
    public Color color3 = Color.blue;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            ChangeColor();
            timer = 0f;
        }
    }

    void ChangeColor()
    {
        // Elige aleatoriamente uno de los tres colores
        int colorChoice = Random.Range(0, 3);

        switch (colorChoice)
        {
            case 0:
                spriteRenderer.color = color1;
                break;
            case 1:
                spriteRenderer.color = color2;
                break;
            case 2:
                spriteRenderer.color = color3;
                break;
        }
    }
}
