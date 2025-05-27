using System;
using SkillSystem;
using UnityEditor;
using UnityEngine;

public partial class UtilityWindow
{
    private readonly string _effectDirectory = "Assets/08.SO/PowerUp/Effects";
    private PowerUpEffectListSO _effectTable;

    //private void InputSoundTable()
    //{
    //    EditorGUILayout.BeginHorizontal();
    //    {
    //        EditorGUILayout.LabelField("Sound Table", EditorStyles.boldLabel);
    //        _soundTable = (SoundTableSO) EditorGUILayout.ObjectField(_soundTable, typeof(SoundTableSO), false);
    //    }
    //    EditorGUILayout.EndHorizontal();
    //}
    private void DrawEffectItems()
    {
        //스탯증가, 스킬언락, 스킬업그레이드
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Stat inc"))
            {
                GenerateEffectAsset<StatIncUpEffectSO>($"{_effectDirectory}/StatInc");
            }

            if (GUILayout.Button("Skill Unlock"))
            {
                GenerateEffectAsset<UnLockSkillEffectSO>($"{_effectDirectory}/UnlockSkill");
            }

            if (GUILayout.Button("Skill Upgrade"))
            {
                GenerateEffectAsset<UpgradeSkillEffectSO>($"{_effectDirectory}/UpgradeSkill");
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(300f));
            {
                EditorGUILayout.LabelField("Effect list");
                EditorGUILayout.Space(3f);


                scrollPositions[UtilType.Effect] = EditorGUILayout.BeginScrollView(
                    scrollPositions[UtilType.Effect],
                    false, true, GUIStyle.none, GUI.skin.verticalScrollbar, GUIStyle.none);
                {
                    foreach (var so in _effectTable.list)
                    {
                        float labelWidth = 220f;
                        GUIStyle style = selectedItem[UtilType.Effect] == so
                            ? _selectStyle
                            : GUIStyle.none;

                        EditorGUILayout.BeginHorizontal(style, GUILayout.Height(40f));
                        {

                            EditorGUILayout.LabelField(
                                $"[{so.type}]", GUILayout.Width(60f), GUILayout.Height(40f));

                            EditorGUILayout.LabelField(
                                $"[{so.code}]", GUILayout.Width(labelWidth), GUILayout.Height(40f));

                            EditorGUILayout.BeginVertical();
                            {
                                EditorGUILayout.Space(10f);
                                GUI.color = Color.red;
                                if (GUILayout.Button("X", GUILayout.Width(20f)))
                                {
                                    _effectTable.list.Remove(so);
                                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(so));
                                    EditorUtility.SetDirty(_effectTable);
                                    AssetDatabase.SaveAssets();
                                }

                                GUI.color = Color.white;
                            }
                            EditorGUILayout.EndVertical();
                            //End of delete!
                        }
                        EditorGUILayout.EndHorizontal();

                        if (so == null)
                            break;

                        //마지막으로 그린 사각형 정보를 알아온다.
                        Rect lastRect = GUILayoutUtility.GetLastRect();

                        if (Event.current.type == EventType.MouseDown
                            && lastRect.Contains(Event.current.mousePosition))
                        {
                            inspectorScroll = Vector2.zero;
                            selectedItem[UtilType.Effect] = so;
                            Event.current.Use();
                        }
                    }
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();

            if (selectedItem[UtilType.Effect] != null)
            {
                inspectorScroll = EditorGUILayout.BeginScrollView(inspectorScroll);
                {
                    EditorGUILayout.Space(2f);
                    Editor.CreateCachedEditor(
                        selectedItem[UtilType.Effect], null, ref _cachedEditor);

                    _cachedEditor.OnInspectorGUI();
                }
                EditorGUILayout.EndScrollView();
            }
        }
        EditorGUILayout.EndHorizontal();

    }

    private T GenerateEffectAsset<T>(string path) where T : PowerUpEffectSO
    {
        Guid guid = Guid.NewGuid();
        T newData = ScriptableObject.CreateInstance<T>();
        newData.code = guid.ToString();
        AssetDatabase.CreateAsset(newData, $"{path}/Effect_{guid}.asset");
        _effectTable.list.Add(newData);
        EditorUtility.SetDirty(_effectTable);
        AssetDatabase.SaveAssets();

        return newData;
    }

   
}