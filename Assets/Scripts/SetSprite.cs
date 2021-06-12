using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSprite : MonoBehaviour
{

    public Animator _animator;
    public RuntimeAnimatorController anim1;
    public RuntimeAnimatorController anim2;
    public RuntimeAnimatorController anim3;
    public RuntimeAnimatorController anim4;
    private RuntimeAnimatorController _anim;

    // Start is called before the first frame update
    void Start()
    {
        setRandomSprite();
        this.GetComponent<Animator>().runtimeAnimatorController = _anim as RuntimeAnimatorController;
    }


    void setRandomSprite()
    {
        float modifier = Random.Range(0, 4);
        if (modifier < 1)
        {
            _anim = anim1;
        } else if (modifier < 2)
        {
            _anim = anim2;
        } else if (modifier < 3)
        {
            _anim = anim3;
        } else
        {
            _anim = anim4;
        }
    }
}
