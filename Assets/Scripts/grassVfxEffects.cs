using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class grassVfxEffects : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem grassVFX;

    [SerializeField]
    private float startSpeed;

    public Controler1 controler;
    private float grassSpeed;
    void Update()
    {
        if (controler.speed < 0)
        {
            grassSpeed = controler.speed;
            grassSpeed *= -2;
            grassSpeed /= 4;
            grassVFX.startSpeed = grassSpeed;
        }
        else if(controler.speed >0)
            {
            startSpeed = controler.speed / 2;
            grassVFX.startSpeed = startSpeed;
        }
    }
}
