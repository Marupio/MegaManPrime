using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[RequireComponent(typeof(Rigidbody2D))]
public class FickleBlock : MonoBehaviour
{
    private Rigidbody2D self;

    public float on;
    public float off;
    public float repeat;

    public AnimatedTile appearingAnimation;
    public AnimatedTile disappearingAnimation;

    void Awake()
    {
        self = GetComponent<Rigidbody2D>();
        appearingAnimation
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
