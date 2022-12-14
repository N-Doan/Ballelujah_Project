using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Reflection;
using System;

public class SSAOConfigurator
{
    private readonly object _ssaoSettings;
    private readonly FieldInfo _fRadius;
    private readonly FieldInfo _fDirectStrength;

    public SSAOConfigurator()
    {
        static ScriptableRendererFeature findRenderFeature(Type type)
        {
            FieldInfo field = reflectField(typeof(ScriptableRenderer), "m_RendererFeatures");
            ScriptableRenderer renderer = UniversalRenderPipeline.asset.scriptableRenderer;
            var list = (List<ScriptableRendererFeature>)field.GetValue(renderer);
            foreach (ScriptableRendererFeature feature in list)
                if (feature.GetType() == type)
                    return feature;
            throw new Exception($"Could not find instance of {type.AssemblyQualifiedName} in the renderer features list");
        }

        static FieldInfo reflectField(Type type, string name) =>
            type.GetField(name, BindingFlags.Instance | BindingFlags.NonPublic) ??
            throw new Exception($"Could not reflect field [{type.AssemblyQualifiedName}].{name}");

        Type tSsaoFeature = Type.GetType("UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusion, Unity.RenderPipelines.Universal.Runtime", true);
        FieldInfo fSettings = reflectField(tSsaoFeature, "m_Settings");
        ScriptableRendererFeature ssaoFeature = findRenderFeature(tSsaoFeature);
        _ssaoSettings = fSettings.GetValue(ssaoFeature) ?? throw new Exception("ssaoFeature.m_Settings was null");
        _fRadius = reflectField(_ssaoSettings.GetType(), "Radius");
        _fDirectStrength = reflectField(_ssaoSettings.GetType(), "DirectLightingStrength");
    }

    public float radius
    {
        get => (float)_fRadius.GetValue(_ssaoSettings);
        set => _fRadius.SetValue(_ssaoSettings, value);
    }

    public float directStrength
    {
        get => (float)_fDirectStrength.GetValue(_ssaoSettings);
        set => _fDirectStrength.SetValue(_ssaoSettings, value);
    }
}