using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection;
using DenizYanar.External.Sense_Engine.Scripts.Core;

namespace DenizYanar.SenseEngine
{

	
	[CustomEditor(typeof(SenseEnginePlayer))]
	public class SenseEnginePlayerEditor : Editor
	{
		public class SenseTypeAndNamePair
		{
			public Type SenseType;
			public string SenseName;

			public SenseTypeAndNamePair(Type senseType, string senseName)
			{
				SenseType = senseType;
				SenseName = senseName;
			}
		}

		protected SerializedProperty _senses;
		protected List<SenseTypeAndNamePair> _senseTypeAndNamePairs = new List<SenseTypeAndNamePair>();
		protected Dictionary<Sense, Editor> _senseEditors;
		protected string[] _senseNamesToDisplay;

		private void OnEnable()
		{
			_senses = serializedObject.FindProperty("SenseList");

			// Create editors
			_senseEditors = new Dictionary<Sense, Editor>();
			for (int i = 0; i < _senses.arraySize; i++)
			{
				CreateEditor(_senses.GetArrayElementAtIndex(i).objectReferenceValue as Sense);
			}


			// Find subclass of sense's by attributes.
			List<Type> types = new List<Type>();

			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

			foreach(Assembly assembly in assemblies)
			{
				Type[] ts = assembly.GetTypes();
				foreach (Type type in ts)
				{
					if(type.IsSubclassOf(typeof(Sense)))
					{
						types.Add(type);
					}				
				}
			}

			
			// Create display list from types
			List<string> typeNames = new List<string>();
			for (int i = 0; i < types.Count; i++)
			{
				string senseName = SenseEnginePathAttribute.GetSensePath(types[i]);
				Type senseType = types[i];
				SenseTypeAndNamePair newSenseNameAndTypePair = new SenseTypeAndNamePair(senseType, senseName);


				_senseTypeAndNamePairs.Add(newSenseNameAndTypePair);
			}

			// List A to Z 
			_senseTypeAndNamePairs = _senseTypeAndNamePairs.OrderBy(t => t.SenseName).ToList();


			// Create display names list from Type And Pairs List


			// First add +Sense label to display first element of popup
			typeNames.Add("+ Add Sense");

			
			for (int i = 0; i < _senseTypeAndNamePairs.Count; i++)
				typeNames.Add(_senseTypeAndNamePairs[i].SenseName);
			
			// Convert list to array
			_senseNamesToDisplay = typeNames.ToArray();



		}


		public override void OnInspectorGUI()
		{


			serializedObject.Update();
			
			for (int i = 0; i < _senses.arraySize; i++)
			{

				SerializedProperty property = _senses.GetArrayElementAtIndex(i);

				Sense sense = property.objectReferenceValue as Sense;
				sense.hideFlags = HideFlags.HideInInspector;


				// Draw header

				int id = i;
				bool isExpanded = property.isExpanded;
				string label = sense.Label;

				SenseEngineEditorStyle.DrawHeader(ref isExpanded, id, label, sense.Color, (int senseId) => RemoveSense(senseId));

				// If header is expanded show it's content

				property.isExpanded = isExpanded;
				if (isExpanded)
				{
					Editor editor = _senseEditors[sense];
					CreateCachedEditor(sense, sense.GetType(), ref editor);

					editor.OnInspectorGUI();

					EditorGUI.EndDisabledGroup();



					EditorGUILayout.Space();
					EditorGUILayout.Space();
				}
			}


			// +Sense popup
			int senseIndex = EditorGUILayout.Popup(0, _senseNamesToDisplay) - 1;

			// Control selection clicked
			if (senseIndex >= 0)
				// Add selection
				AddSense(_senseTypeAndNamePairs[senseIndex].SenseType);

			serializedObject.ApplyModifiedProperties();
		}


		
		private void CreateEditor(Sense sense)
		{
			if (sense == null)
				return;

			if (_senseEditors.ContainsKey(sense))
				return;

			Editor editor = null;
			CreateCachedEditor(sense, null, ref editor);
			_senseEditors.Add(sense, editor);		
		}


		private Sense AddSense(Type type)
		{

			GameObject gameObject = (target as SenseEnginePlayer).gameObject;

			Sense newFeedback = Undo.AddComponent(gameObject, type) as Sense;
			newFeedback.hideFlags = HideFlags.HideInInspector;

			CreateEditor(newFeedback);

			_senses.arraySize++;
			_senses.GetArrayElementAtIndex(_senses.arraySize - 1).objectReferenceValue = newFeedback;

			return newFeedback;
		}

		private void RemoveSense(int id)
		{
			SerializedProperty property = _senses.GetArrayElementAtIndex(id);
			Sense sense = property.objectReferenceValue as Sense;

			(target as SenseEnginePlayer).SenseList.Remove(sense);

			_senseEditors.Remove(sense);
			Undo.DestroyObjectImmediate(sense);
		}
	}

}
