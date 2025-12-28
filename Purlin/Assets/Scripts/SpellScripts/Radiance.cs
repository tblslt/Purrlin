//blinding light around the player, damaging enemies. if not hitting an enemy, drain mana
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;

public class Radiance : MonoBehaviour, Spell
{
    playerScript player;
    Coroutine activeCoroutine = null;
    public GameObject hitbox;
    public int baseDamage = 1;

    int manaCost = 1;
    float baseCastTime = .5f;

    void Awake()
    {
        player = GetComponentInParent<playerScript>();
    }

    void Spell.Cancel()
    {
        player.queuedSpell = null;
        if (activeCoroutine != null)
        {
            hitbox = null;
            //TODO: disable hitbox and animations
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }
    }
    void Spell.Cast()
    {
        player.queuedSpell = null;
        if (activeCoroutine != null)
        {
            hitbox = null;
            //TODO: disable hitbox and animations
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }
        else
        {
            activeCoroutine = StartCoroutine(Casting());
        }

    }
    IEnumerator Casting()
    {

        while (true)
        {

            //if (enemy in hitbox collider)
            {
                //TODO: damage all enemy touching
            }
            //else
            {
                if (manaCost > player.mana)
                {
                    //TODO: destory item in hitbox
                    hitbox = null;
                    activeCoroutine = null;
                    yield break;
                }
                yield return new WaitForSeconds(baseCastTime * player.durationMultiplier);
            }
        }
    }
}
