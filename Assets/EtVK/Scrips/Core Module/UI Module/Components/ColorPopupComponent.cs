using EtVK.UI_Module.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Components
{
    public class ColorPopupComponent: BasePopupPanel
	{
		[UnityEngine.Scripting.Preserve]
		public new class UxmlFactory : UxmlFactory<ColorPopupComponent, UxmlTraits> { }

		public string Heading { get; set; } = "Pick Colour";
		public string ButtonLabel { get; set; } = "Apply";
		public string ButtonCloseLabel { get; set; } = "Default";
		public System.Action<Color> onColorChange;
		public bool isOpen;

		private float H, S, V, A;

		private System.Action<Color> onSubmit;
		private System.Action onCancel;

		private Button submitButton;
		private Button closeButton;
		
		private Slider2D gradientSlider;
		private UnityEngine.UIElements.Slider hueSlider;
		private VisualElement gradientSliderDragger;
		private VisualElement hueSliderDragger;

		private Texture2D gradientTexture;
		private Texture2D hueSliderTexture;

		private Color defaultColor;
		
		private const string popupStylesResource = "UiStyles/ColorPopupStyleSheet";
		private const string ussPopupClassName = "color-popup";
		private const string ussContentBack = ussPopupClassName + "__content-area";
		private const string ussButtonsBar = ussPopupClassName + "__buttons-bar";
		private const string ussSubmitButton = "button-popup";
		private const string ussGradientArea = ussPopupClassName + "__gradient-area";
		private const string ussGradientSlider = ussPopupClassName + "__gradient-slider";
		private const string ussHueSlider = ussPopupClassName + "__hue-slider";

		// ------------------------------------------------------------------------------------------------------------

		public ColorPopupComponent()
			: this(null, null)
		{ }

		public ColorPopupComponent(string heading, string buttonLabel)
		{
			if (heading != null) Heading = heading;
			if (buttonLabel != null) ButtonLabel = buttonLabel;

			styleSheets.Add(Resources.Load<StyleSheet>(popupStylesResource));

			// panel
			mainPanel.AddToClassList(ussPopupClassName);

			// content area
			var content = new VisualElement();
			content.AddToClassList(ussContentBack);
			mainPanel.Add(content);

			// gradient area
			var gradientArea = new VisualElement();
			gradientArea.AddToClassList(ussGradientArea);
			content.Add(gradientArea);

			// gradient block
			gradientSlider = new Slider2D();
			gradientSliderDragger = gradientSlider.Q("dragger");
			gradientSlider.AddToClassList(ussGradientSlider);
			gradientArea.Add(gradientSlider);

			// hue slider
			hueSlider = new UnityEngine.UIElements.Slider(null, 0f, 360f, SliderDirection.Vertical, 0f);
			hueSliderDragger = hueSlider.Q("unity-dragger");
			hueSlider.AddToClassList(ussHueSlider);
			gradientArea.Add(hueSlider);
			

			// button bar
			var buttons = new VisualElement();
			buttons.AddToClassList(ussButtonsBar);
			mainPanel.Add(buttons);

			submitButton = new Button() { text = ButtonLabel };
			submitButton.AddToClassList(ussSubmitButton);
			submitButton.clicked += OnSubmitButton;

			closeButton = new Button() {text = ButtonCloseLabel};
			closeButton.AddToClassList(ussSubmitButton);
			closeButton.clicked += OnChancelButton;
			
			buttons.Add(closeButton);
			buttons.Add(submitButton);

			hueSlider.RegisterValueChangedCallback(SetColorFromHueSlider);
			gradientSlider.RegisterValueChangedCallback(SetColorFromGradientSlider);
		}

		// public void Show(string heading, string buttonLabel, Color color, System.Action<Color> onSubmit)
		// {
		// 	if (heading != null) Heading = heading;
		// 	if (buttonLabel != null) ButtonLabel = buttonLabel;
		// 	Show(color, onSubmit);
		// }

		public void Show(Color color, System.Action<Color> onSubmit, System.Action onCancel, Color defaultColor = default)
		{
			Color.RGBToHSV(color, out H, out S, out V);
			A = color.a;
			
			this.onSubmit = onSubmit;
			this.onCancel = onCancel;
			this.defaultColor = defaultColor;
			isOpen = true;

			submitButton.text = ButtonLabel;
			
			CreateTextures();
			OnColorChanged(true, true);

			base.Show();
		}

		public override void Hide()
		{
			gradientSlider.style.backgroundImage = null;
			hueSlider.style.backgroundImage = null;
			isOpen = false;

			Object.Destroy(gradientTexture);
			Object.Destroy(hueSliderTexture);

			onSubmit = null;
			base.Hide();
		}

		// ------------------------------------------------------------------------------------------------------------

		private void OnSubmitButton()
		{
			var c = Color.HSVToRGB(H, S, V);
			c.a = A;
			
			onSubmit?.Invoke(c);

			Hide();
		}

		private void OnChancelButton()
		{
			onCancel?.Invoke();
			if(defaultColor != default)
				onColorChange?.Invoke(defaultColor);
		}

		private void SetColorFromGradientSlider(ChangeEvent<Vector2> ev)
		{
			S = ev.newValue.x;
			V = ev.newValue.y;
			OnColorChanged(false, false);
		}

		private void SetColorFromHueSlider(ChangeEvent<float> ev)
		{		
			H = ev.newValue / 360f; // hue slider value 0..360
			OnColorChanged(false, true);
		}
		

		// ------------------------------------------------------------------------------------------------------------

		private void OnColorChanged(bool updateHue, bool updateGradient)
		{
			var c = Color.HSVToRGB(H, S, V);
			hueSliderDragger.style.backgroundColor = Color.HSVToRGB(H, 1f, 1f);
			gradientSliderDragger.style.backgroundColor = c;
			onColorChange?.Invoke(c);


			if (updateHue)
			{
				hueSlider.SetValueWithoutNotify(H * 360f);
			}

			if (updateGradient)
			{
				UpdateGradientTexture();
				gradientSlider.SetValueWithoutNotify(new Vector2(S, V));
			}
		}

		private void CreateTextures()
		{
			gradientTexture = new Texture2D(64, 64, TextureFormat.RGB24, false) { filterMode = FilterMode.Point };
			gradientTexture.hideFlags = HideFlags.HideAndDontSave;
			gradientSlider.style.backgroundImage = gradientTexture;

			hueSliderTexture = new Texture2D(1, 64, TextureFormat.RGB24, false) { filterMode = FilterMode.Point };
			hueSliderTexture.hideFlags = HideFlags.HideAndDontSave;
			hueSlider.style.backgroundImage = hueSliderTexture;
			UpdateHueSliderTexture();
		}

		private void UpdateHueSliderTexture()
		{
			if (hueSliderTexture == null) return;
			for (var i = 0; i < hueSliderTexture.height; i++)
			{
				hueSliderTexture.SetPixel(0, i, Color.HSVToRGB((float)i / (hueSliderTexture.height - 1), 1f, 1f));
			}

			hueSliderTexture.Apply();
			hueSlider.MarkDirtyRepaint();
		}

		private void UpdateGradientTexture()
		{
			if (gradientTexture == null) return;
			var pixels = new Color[gradientTexture.width * gradientTexture.height];

			for (var x = 0; x < gradientTexture.width; x++)
			{
				for (var y = 0; y < gradientTexture.height; y++)
				{
					pixels[x * gradientTexture.width + y] = Color.HSVToRGB(H, (float)y / gradientTexture.height, (float)x / gradientTexture.width);
				}
			}

			gradientTexture.SetPixels(pixels);
			gradientTexture.Apply();
			gradientSlider.MarkDirtyRepaint();
		}
		

		// ============================================================================================================
	}
}