using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tape_Pivot_Script : MonoBehaviour
{
    [SerializeField] GameObject hitBox1;
    [SerializeField] GameObject hitBox2;
    [SerializeField] GameObject tapePrefab;
    GameObject tape;

    // Start is called before the first frame update
    void Start()
    {
        tape = GameObject.Find("Tape");

        Instantiate(hitBox1, new Vector3(2, 0, 0), Quaternion.identity);
        Instantiate(hitBox2, new Vector3(0, 2, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (tape.GetComponent<Tape_Script>() == null)
        {
            // make previous tape no longer a child
            tape.transform.parent = null;

            // reset tape pivot transform
            transform.rotation = Quaternion.identity;
            transform.localScale = new Vector3(1, 1, 1);

            // create new tape
            tape = Instantiate(tapePrefab, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), Quaternion.identity);
            tape.transform.parent = transform;
        }
    }
}
