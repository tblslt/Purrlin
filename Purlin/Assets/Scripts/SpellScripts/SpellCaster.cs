using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    int spellQueue = -1;
    int activeSpell = -1;
    Spell[] spellOptions;
    public Rigidbody2D rb;

    public int maxMana = 10;
    public int mana = 10;
    public int manaRegen = 2;

    public int maxHealth = 9;
    public int health = 9;
    //stat modifiers
    public float durationMultiplier = 1f;
    public float damageMultiplier = 1f;


    void Awake()
    {
        spellOptions = GetComponents<Spell>();
    }

    private void Update()
    {
        if(activeSpell != -1)
        {
            print(activeSpell + "," + spellQueue + "," + spellOptions[activeSpell].activeCoroutine == null);
            if(spellOptions[activeSpell].activeCoroutine == null)
            {
                activeSpell = -1;
            }
        }
        if(activeSpell == -1 && spellQueue != -1)
        {
            CastSpell(spellQueue);
            spellQueue = -1;
        }
    }

    public void QueueSpell(int i)
    {
        spellQueue = i;
    }

    void CastSpell(int i)
    {
        Debug.Log("attempt cast" + i);
        
            if (spellOptions[i].Cast())
            {
                activeSpell = i;
            }
        
    }
    void CancelSpell(int i)
    {
        if (spellOptions.Length! > i + 1)
        {
            spellOptions[i].Cancel();
        }
    }
    void CancelAllSpell()
    {
        foreach (Spell spell in spellOptions)
        {
            spell.Cancel();
        }
    }
}