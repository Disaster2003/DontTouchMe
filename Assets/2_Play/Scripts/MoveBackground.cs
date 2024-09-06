using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // ”wŒi‚ğ¶‚ÉˆÚ“®
        transform.position += new Vector3(5 * -Time.deltaTime, 0, 0);

        // ‰æ‘œ‚Ì“¯‚¶•”•ª‚É–ß‚µA‚¸‚Á‚Æ‘±‚¢‚Ä‚¢‚é‚æ‚¤‚ÉöŠo
        if (transform.position.x <= -17.25f)
            transform.position = Vector3.zero;
    }
}
