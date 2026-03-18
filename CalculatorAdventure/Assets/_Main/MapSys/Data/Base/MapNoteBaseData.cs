using Sirenix.OdinInspector;
using System;
using UnityEngine;

public abstract class MapNoteBaseData : ScriptableObject
{
    [Title("ID")][SerializeField] private int id;
    [SerializeField] MapNoteType mapNoteType;

    public abstract void TriggerMapNote();
}
