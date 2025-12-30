//stop movement and heal after animation is played
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;

public class Heal : MonoBehaviour, Spell
{
    SpellCaster sc;
    bool cancelled = false;
    public Coroutine activeCoroutine { get; private set; }
    public int manaCost = 10;
    public float baseCastTime = 2;
    void Awake()
    {
        sc = GetComponent<SpellCaster>();
    }
    void Spell.Cancel()
    {
        if (activeCoroutine != null)
        {
            //TODO: stop animation
            StopCoroutine(activeCoroutine);
            sc.rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
            activeCoroutine = null;
        }
    }

    bool Spell.Cast()
    {
        activeCoroutine = StartCoroutine(Casting());
        return true;
    }
    IEnumerator Casting()
    {

        if (manaCost > sc.mana) { activeCoroutine = null; yield break; }
        sc.mana -= manaCost;
        float startTime = Time.time;

        //TODO: play heal animation
        sc.rb.constraints |= RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(baseCastTime * sc.durationMultiplier);
        sc.rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
        activeCoroutine = null;
        //TODO: set animation back
    }

}