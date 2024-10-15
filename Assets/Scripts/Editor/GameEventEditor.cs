using NUnit.Framework;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Unity.VisualScripting;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor
{
    private SerializedProperty _effects;
    private ReorderableList _list;
    private Type[] _types;

    public override void OnInspectorGUI()
    {
        GameEvent gameEvent = target as GameEvent;

        _list.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }

    private void OnEnable()
    {
        _effects = serializedObject.FindProperty("Effects");

        _list = new ReorderableList(serializedObject, _effects, true, true, true, true);
        _list.drawElementCallback = DrawListItems;
        _list.drawHeaderCallback = DrawHeader;
        _list.onAddDropdownCallback = AddDropDown;

        _types = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                  from assemblyType in domainAssembly.GetTypes()
                  where assemblyType.IsSubclassOf(typeof(GameEffect))
                  select assemblyType).ToArray();
    }

    private void DrawListItems(Rect rect, int index, bool isActive, bool IsFocused)
    {
        GameEvent gameEvent = target as GameEvent;
        if (gameEvent == null) return;

        GameEffect feedback = gameEvent.Effects[index];
        if (feedback == null) return;

        if (index > 0)
        {
            var line = rect;
            line.height = 1;
            line.x += 45;
            line.width -= 45;
            EditorGUI.DrawRect(line, Color.black);
        }

        var header = rect;
        header.width = 10;
        EditorGUI.DrawRect(header, gameEvent.Effects[index].GetColor(gameEvent));

        SerializedProperty element = _effects.GetArrayElementAtIndex(index);
        SerializedProperty enabledProperty = element.FindPropertyRelative("Enabled");
        rect.x += 15;
        enabledProperty.boolValue = EditorGUI.Toggle(rect, GUIContent.none, enabledProperty.boolValue);

        rect.x += 30;
        EditorGUI.LabelField(rect, gameEvent.Effects[index].ToString());

        if (IsFocused == false && isActive == false) return;

        foreach (SerializedProperty child in GetChildren(element))
        {
            EditorGUILayout.PropertyField(child);
        }
    }

    private void DrawHeader(Rect rect)
    {
        //EditorGUI.DrawRect(rect, Color.red);
    }

    private void AddDropDown(Rect rect, ReorderableList list)
    {
        GenericMenu menu = new GenericMenu();
        foreach (var item in _types)
        {
            menu.AddItem(new GUIContent(item.Name), false, () =>
            {
                _effects.arraySize++;
                SerializedProperty newProp = _effects.GetArrayElementAtIndex(_effects.arraySize - 1);
                newProp.managedReferenceValue = Activator.CreateInstance(item);
                serializedObject.ApplyModifiedProperties();
            }
            );
        }

        menu.ShowAsContext();
    }

    private IEnumerable<SerializedProperty> GetChildren(SerializedProperty property)
    {
        SerializedProperty currentProperty = property.Copy();
        SerializedProperty nextProperty = property.Copy();
        nextProperty.Next(false);

        if (currentProperty.Next(true))
        {
            do
            {
                if (SerializedProperty.EqualContents(currentProperty, nextProperty)) break;
                yield return currentProperty;
            } while (currentProperty.Next(false));
        }
    }
}
