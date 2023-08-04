using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    
    private Transform player;
    public float smooth;
    private float s;
    // Start is called before the first frame update
    void Start()
    {
        s = smooth;
        smooth = 0.5f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Visualize();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(player.position.x >= -10 && player.position.x < 200)
        {
            Vector3 following = new Vector3(player.position.x, player.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, following, smooth * Time.deltaTime);
        }
    }

    void Visualize()
    {
        StartCoroutine("Sceneview");
    }
    
    IEnumerator Sceneview()
    {
        yield return new WaitForSeconds(3f);
        smooth = s;
    }
}
