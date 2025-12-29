//blinding light around the player, damaging enemies. if not hitting an enemy, drain mana
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;

public class Radiance : MonoBehaviour, Spell
{
    SpellCaster sc;
    public Coroutine activeCoroutine { get; private set; }
    public GameObject hitbox;
    public int baseDamage = 1;

    int manaCost = 1;
    float baseCastTime = .5f;

    void Awake()
    {
        sc = GetComponentInParent<SpellCaster>();
    }

    void Spell.Cancel()
    {
        if (activeCoroutine != null)
        {
            hitbox.SetActive(false);
            //TODO: disable hitbox and animations
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }
    }
    bool Spell.Cast()
    {
        if (activeCoroutine != null)
        {
            hitbox.SetActive(false);
            //TODO: disable hitbox and animations
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }
        else
        {
            activeCoroutine = StartCoroutine(Casting());
        }
        return false;

    }
    IEnumerator Casting()
    {
        if (manaCost > sc.mana)
        {
            //TODO: destory item in hitbox
            hitbox.SetActive(false);
            activeCoroutine = null;
            yield break;
        }
        hitbox.SetActive(true);
        while (true)
        {

            //if (enemy in hitbox collider)
            //{
                //TODO: damage all enemy touching
            //}
            //else
            //{
                if (manaCost > sc.mana)
                {
                    //TODO: destory item in hitbox
                    hitbox.SetActive(false);
                    activeCoroutine = null;
                    yield break;
                }
                yield return new WaitForSeconds(baseCastTime * sc.durationMultiplier);
            //}
        }
    }
}
