using System;
using System.Runtime.CompilerServices;
using UnityEngine;

internal sealed class MathUtility {

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualToAnyOfTwo(int value, int one, int two) {
        return value == one || value == two;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInsideExclusive(float value, float min, float max) {
        return min < value && value < max;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInsideInclusiveExclusive(int value, int minInclusive, int maxExclusive) {
        return minInclusive <= value && value < maxExclusive;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOutSideExclusive(float value, float min, float max) {
        return value < min || max < value;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CirculerAddition(int value, int addend, int min, int max) {
        int raw = value + addend;
        if(raw > max) return min;
        if(raw < min) return max;
        return raw;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CirculerSubtraction(int value, int addend, int min, int max) {
        int raw = value - addend;
        if(raw > max) return min;
        if(raw < min) return max;
        return raw;
    }
}