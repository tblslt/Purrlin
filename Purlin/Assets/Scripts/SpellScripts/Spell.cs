using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public interface Spell
{
    public Coroutine activeCoroutine { get;}
    public void Cancel();
    public bool Cast(); //returns true when a spell is cast and active, returns false if the cast spell does not prevent other spells
}