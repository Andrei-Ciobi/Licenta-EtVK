using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Components
{
    public class ColorFieldComponent : BaseField<Color>
	{
		[UnityEngine.Scripting.Preserve]
		public new class UxmlFactory : UxmlFactory<ColorFieldComponent, UxmlTraits> { }

		[UnityEngine.Scripting.Preserve]
		public new class UxmlTraits : BaseFieldTraits<Color, UxmlColorAttributeDescription>
		{
		}

		public ColorPopupComponent ColorPopup { get; set; }
		public System.Action<ColorPopupComponent> onOpen;

		private ColorFieldInput colorFieldInput;
		private Color defaultColor;

		private const string stylesResource = "UiStyles/ColorFieldStyleSheet";
		private const string ussFieldName = "color-field";
		private const string ussFieldLabel = "color-field__label";

		// ------------------------------------------------------------------------------------------------------------

		public ColorFieldComponent()
			: this(null, new ColorFieldInput())
		{ }

		public ColorFieldComponent(string label)
			: this(label, new ColorFieldInput())
		{ }

		private ColorFieldComponent(string label, ColorFieldInput colorFieldInput)
			: base(label, colorFieldInput)
		{
			this.colorFieldInput = colorFieldInput;

			styleSheets.Add(Resources.Load<StyleSheet>(stylesResource));
			AddToClassList(ussFieldName);

			labelElement.AddToClassList(ussFieldLabel);

			colorFieldInput.RegisterCallback<ClickEvent>(OnClickEvent);
			RegisterCallback<GeometryChangedEvent>(OnGeometryChangedEvent);
		}

		public override void SetValueWithoutNotify(Color newValue)
		{
			base.SetValueWithoutNotify(newValue);
			colorFieldInput.SetColor(newValue);
		}

		public void SetDefaultColor(Color defaultColor)
		{
			
			this.defaultColor = defaultColor;
			SetValueWithoutNotify(defaultColor);
		}

		private void OnGeometryChangedEvent(GeometryChangedEvent ev)
		{
			UnregisterCallback<GeometryChangedEvent>(OnGeometryChangedEvent);
			colorFieldInput.SetColor(value);
		}

		private void OnClickEvent(ClickEvent ev)
		{
			ColorPopup?.Show(value, c => value = c, ResetColor, defaultColor);
			onOpen?.Invoke(ColorPopup);
		}

		private void ResetColor()
		{
			SetValueWithoutNotify(defaultColor);
		}
		// ============================================================================================================

		private class ColorFieldInput : VisualElement
		{
			public VisualElement rgbField;

			private const string ussFieldInput = "color-field__input";
			private const string ussFieldInputRGB = "color-field__input-rgb";

			public ColorFieldInput()
			{
				AddToClassList(ussFieldInput);

				rgbField = new VisualElement();
				rgbField.AddToClassList(ussFieldInputRGB);
				Add(rgbField);
			}

			public void SetColor(Color color)
			{
				rgbField.style.backgroundColor = new Color(color.r, color.g, color.b, 1f);
			}
		}

		// ============================================================================================================
	}
}