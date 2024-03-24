using UnityEngine;


public class ExitManager : MonoBehaviour
{
    Animator animator;

    private GameObject[] doors;
    public GameManager gameManager;
    private void Awake()
    {
        doors = GameObject.FindGameObjectsWithTag("Door");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PNJ"))
        {
            animator = other.GetComponent<Animator>();
            if (animator.GetBool("hasEnterPub") == false)
            {
                foreach (GameObject door in doors)
                {
                    DoorLeft doorScript = door.GetComponent<DoorLeft>();
                    doorScript.howManyPnjUseDoors -= 1;
                    if (doorScript.howManyPnjUseDoors == 0)
                    {
                        doorScript.CloseDoor();
                    }
                }
                if (gameObject.CompareTag("Entry"))
                    animator.SetBool("hasEnterPub", true);
                    gameManager.AugmenterArgent(8);
            }
        GoBackInPub(other);
        }
    }

    public void GoBackInPub(Collider other)
    {
        animator = other.GetComponent<Animator>();
        int actions = animator.GetInteger("actions");
        if (-1 < actions && actions < 3)
        {
            animator.SetBool("shouldGoIn", true);
        }
    }
}
