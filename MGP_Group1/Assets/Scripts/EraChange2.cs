using UnityEngine;

public class EraChange2 : MonoBehaviour
{
    public Sprite OldEra;
    public Sprite NewEra;
    private static bool Era = true; //True == OldEra, False == NewEra
    public static bool ChangeEra
    {
        get { return Era; }
        set { Era = value; }
    }
    private bool Changed = false;
    public bool hasChanged //Use this to prevent an object from triggering an era change multiple times
    {
        get { return Changed; }
        set { Changed = value; }
    }

    private void OnEnable()
    {
        Changed = false;
    }

    private float Mult = 1f;

    void Update()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (Era)
        {
            sprite.sprite = OldEra;
        }
        else
        {
            sprite.sprite = NewEra;
        }
        if (sprite.color.a <= 0f)
        {
            Mult = 1f;
        }
        else if (sprite.color.a >= 1f)
        {
            Mult = -1f;
        }
        sprite.color = new Color(255f, 255f, 255f, sprite.color.a + Mult * 0.02f);
    }
}
