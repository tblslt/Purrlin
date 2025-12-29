using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

//prevents incomming damage and staggers enemies that hit. if a successful block, refund mana
public class Counter : MonoBehaviour, Spell
{
    SpellCaster sc;
    bool cancelled = false;
    Coroutine activeCoroutine = null;
    public int manaCost = 6;
    public float CastDuration = 1;
    void Awake()
    {
        sc = GetComponentInParent<SpellCaster>();
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
    void Spell.Cast()
    {
        activeCoroutine = StartCoroutine(Casting());
    }
    IEnumerator Casting()
    {

        if (manaCost > sc.mana) { activeCoroutine = null; yield break; }
        sc.mana -= manaCost;
        float startTime = Time.time;

        sc.rb.constraints |= RigidbodyConstraints2D.FreezePosition;
        //TODO: play counter animation
        while (Time.time < startTime + CastDuration * sc.durationMultiplier)
        {
            yield return null;
        }

        sc.rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
        //TODO: set animation back
    }
}