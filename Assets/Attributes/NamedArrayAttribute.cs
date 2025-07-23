using System.Collections.Generic;
using UnityEngine;

public sealed class NamedArrayAttribute : PropertyAttribute {
    public readonly string[] NAMES = default;
    public NamedArrayAttribute(string[] names) {
        NAMES = names;
    }
}