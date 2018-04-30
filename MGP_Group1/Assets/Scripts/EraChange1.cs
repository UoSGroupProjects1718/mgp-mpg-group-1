using UnityEngine;

public class EraChange1 : MonoBehaviour
{
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
			GetComponent<Animator>().SetFloat("Era", 0f);
        }
        else
        {
			GetComponent<Animator>().SetFloat("Era", 1f);
        }
    }
}
