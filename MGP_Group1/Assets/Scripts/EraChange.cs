using UnityEngine;

public class EraChange : MonoBehaviour
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

    void Update()
    {
        if (Era)
        {
            GetComponent<SpriteRenderer>().sprite = OldEra;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = NewEra;
        }
    }
}
