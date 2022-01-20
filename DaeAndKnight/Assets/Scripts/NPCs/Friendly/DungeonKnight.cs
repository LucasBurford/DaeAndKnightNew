using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonKnight : MonoBehaviour
{
    public Dialogue dialogue1;

    public int conversation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<PlayerAttack>().isInDialogue = true;

            conversation++;

            switch (conversation)
            {
                case 1:
                    {
                        dialogue1.StartCoroutine(dialogue1.Type());
                    }
                    break;
            }

        }
    }
}
