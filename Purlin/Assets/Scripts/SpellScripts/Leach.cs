//roots grow in the direction the player is facing that damages enemies and heals if hit
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
public class Leach : MonoBehaviour, Spell
{
    SpellCaster sc;
    Coroutine activeCoroutine = null;
    public GameObject hitbox;
    public int baseDamage = 1;

    int manaCost = 3;
    float baseCastTime = .5f;
    int heal = 1;
    void Awake()
    {
        sc = GetComponentInParent<SpellCaster>();
    }
    void Spell.Cancel()
    {
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
        if (activeCoroutine == null)
        {
            activeCoroutine = StartCoroutine(Casting());
        }
    }

    IEnumerator Casting()
    {

        if (manaCost > sc.mana) { activeCoroutine = null; yield break; }
        sc.mana -= manaCost;
        float startTime = Time.time;

        //TODO: play animation
        //if (enemy in hitbox collider)
        {
            //TODO: damage all enemy touching
            if (sc.health < sc.maxHealth)
            {
                sc.health += 1;
            }
        }
        while (Time.time < startTime + baseCastTime * sc.durationMultiplier)
        {

            yield return null;
        }
        //TODO: set animation back
    }
}
