/******************************************************************************
 * Spine Runtimes License Agreement
 * Last updated May 1, 2019. Replaces all prior versions.
 *
 * Copyright (c) 2013-2019, Esoteric Software LLC
 *
 * Integration of the Spine Runtimes into software or otherwise creating
 * derivative works of the Spine Runtimes is permitted under the terms and
 * conditions of Section 2 of the Spine Editor License Agreement:
 * http://esotericsoftware.com/spine-editor-license
 *
 * Otherwise, it is permitted to integrate the Spine Runtimes into software
 * or otherwise create derivative works of the Spine Runtimes (collectively,
 * "Products"), provided that each user of the Products must obtain their own
 * Spine Editor license and redistribution of the Products in any form must
 * include this license and copyright notice.
 *
 * THIS SOFTWARE IS PROVIDED BY ESOTERIC SOFTWARE LLC "AS IS" AND ANY EXPRESS
 * OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN
 * NO EVENT SHALL ESOTERIC SOFTWARE LLC BE LIABLE FOR ANY DIRECT, INDIRECT,
 * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES, BUSINESS
 * INTERRUPTION, OR LOSS OF USE, DATA, OR PROFITS) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
 * EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/

using UnityEditor;
using UnityEngine;
using Spine;
using Spine.Unity;
using Spine.Unity.Playables;

//[CustomPropertyDrawer(typeof(SpineAnimationStateBehaviour))]
#if UNITY_EDITOR
public class SpineAnimationStateDrawer : PropertyDrawer {
	/*
	public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
		const int fieldCount = 8;
		return fieldCount * EditorGUIUtility.singleLineHeight;
	}

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
		SerializedProperty skeletonDataAssetProp = property.FindPropertyRelative("skeletonDataAsset");
		SerializedProperty animationNameProp = property.FindPropertyRelative("animationName");
		SerializedProperty loopProp = property.FindPropertyRelative("loop");
		SerializedProperty eventProp = property.FindPropertyRelative("eventThreshold");
		SerializedProperty attachmentProp = property.FindPropertyRelative("attachmentThreshold");
		SerializedProperty drawOrderProp = property.FindPropertyRelative("drawOrderThreshold");

		Rect singleFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
		EditorGUI.PropertyField(singleFieldRect, skeletonDataAssetProp);

		float lineHeightWithSpacing = EditorGUIUtility.singleLineHeight + 2f;

		singleFieldRect.y += lineHeightWithSpacing;
		EditorGUI.PropertyField(singleFieldRect, animationNameProp);

		singleFieldRect.y += lineHeightWithSpacing;
		EditorGUI.PropertyField(singleFieldRect, loopProp);

		singleFieldRect.y += lineHeightWithSpacing * 0.5f;

		singleFieldRect.y += lineHeightWithSpacing;
		EditorGUI.LabelField(singleFieldRect, "Mixing Settings", EditorStyles.boldLabel);

		singleFieldRect.y += lineHeightWithSpacing;
		EditorGUI.PropertyField(singleFieldRect, eventProp);

		singleFieldRect.y += lineHeightWithSpacing;
		EditorGUI.PropertyField(singleFieldRect, attachmentProp);

		singleFieldRect.y += lineHeightWithSpacing;
		EditorGUI.PropertyField(singleFieldRect, drawOrderProp);
	}
	*/
}
#endif