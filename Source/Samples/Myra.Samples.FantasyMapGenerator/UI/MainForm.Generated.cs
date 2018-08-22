/* Generated by Myra UI Editor at 22.08.2018 4:57:19 */
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;

namespace Myra.Samples.FantasyMapGenerator.UI
{
	partial class MainForm
	{
		private void BuildUI()
		{
			var textBlock1 = new TextBlock();
			textBlock1.Text = "Size:";
			textBlock1.Id = "";

			var listItem1 = new ListItem();
			listItem1.Text = "512x512";

			var listItem2 = new ListItem();
			listItem2.Text = "1024x1024";

			var listItem3 = new ListItem();
			listItem3.Text = "2048x2048";

			var listItem4 = new ListItem();
			listItem4.Text = "4096x4096";

			_comboSize = new ComboBox();
			_comboSize.SelectedIndex = 2;
			_comboSize.Id = "_comboSize";
			_comboSize.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_comboSize.GridPositionX = 1;
			_comboSize.Items.Add(listItem1);
			_comboSize.Items.Add(listItem2);
			_comboSize.Items.Add(listItem3);
			_comboSize.Items.Add(listItem4);

			var textBlock2 = new TextBlock();
			textBlock2.Text = "Variability:";
			textBlock2.GridPositionY = 1;

			_spinVariability = new SpinButton();
			_spinVariability.Value = 1;
			_spinVariability.DrawLinesColor = Color.White;
			_spinVariability.Id = "_spinVariability";
			_spinVariability.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_spinVariability.GridPositionX = 1;
			_spinVariability.GridPositionY = 1;

			_textWaterLand = new TextBlock();
			_textWaterLand.Text = "Water/Land(60%):";
			_textWaterLand.Id = "_textWaterLand";
			_textWaterLand.GridPositionY = 2;

			_sliderWaterLand = new HorizontalSlider();
			_sliderWaterLand.Maximum = 100;
			_sliderWaterLand.Id = "_sliderWaterLand";
			_sliderWaterLand.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_sliderWaterLand.GridPositionX = 1;
			_sliderWaterLand.GridPositionY = 2;

			var grid1 = new Grid();
			grid1.DrawLinesColor = Color.White;
			grid1.ColumnSpacing = 8;
			grid1.RowSpacing = 8;
			grid1.ColumnsProportions.Add(new Grid.Proportion
			{
				Type = Myra.Graphics2D.UI.Grid.ProportionType.Pixels,
				Value = 140,
			});
			grid1.ColumnsProportions.Add(new Grid.Proportion
			{
				Type = Myra.Graphics2D.UI.Grid.ProportionType.Fill,
			});
			grid1.RowsProportions.Add(new Grid.Proportion());
			grid1.RowsProportions.Add(new Grid.Proportion());
			grid1.RowsProportions.Add(new Grid.Proportion());
			grid1.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			grid1.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Stretch;
			grid1.Widgets.Add(textBlock1);
			grid1.Widgets.Add(_comboSize);
			grid1.Widgets.Add(textBlock2);
			grid1.Widgets.Add(_spinVariability);
			grid1.Widgets.Add(_textWaterLand);
			grid1.Widgets.Add(_sliderWaterLand);

			_checkSurrondedByWater = new CheckBox();
			_checkSurrondedByWater.Text = "Surrounded By Water";
			_checkSurrondedByWater.ImageWidthHint = 10;
			_checkSurrondedByWater.ImageHeightHint = 10;
			_checkSurrondedByWater.ContentHorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_checkSurrondedByWater.ContentVerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Stretch;
			_checkSurrondedByWater.Id = "_checkSurrondedByWater";
			_checkSurrondedByWater.GridPositionY = 1;

			_checkSmooth = new CheckBox();
			_checkSmooth.Text = "Smooth";
			_checkSmooth.ImageWidthHint = 10;
			_checkSmooth.ImageHeightHint = 10;
			_checkSmooth.ContentHorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_checkSmooth.ContentVerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Stretch;
			_checkSmooth.Id = "_checkSmooth";
			_checkSmooth.GridPositionY = 2;

			_checkRemoveSmalIslands = new CheckBox();
			_checkRemoveSmalIslands.Text = "Remove Small Islands";
			_checkRemoveSmalIslands.ImageWidthHint = 10;
			_checkRemoveSmalIslands.ImageHeightHint = 10;
			_checkRemoveSmalIslands.ContentHorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_checkRemoveSmalIslands.ContentVerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Stretch;
			_checkRemoveSmalIslands.Id = "_checkRemoveSmalIslands";
			_checkRemoveSmalIslands.GridPositionY = 3;

			_checkRemoveSmallLakes = new CheckBox();
			_checkRemoveSmallLakes.Text = "Remove Small Lakes";
			_checkRemoveSmallLakes.ImageWidthHint = 10;
			_checkRemoveSmallLakes.ImageHeightHint = 10;
			_checkRemoveSmallLakes.ContentHorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_checkRemoveSmallLakes.ContentVerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Stretch;
			_checkRemoveSmallLakes.Id = "_checkRemoveSmallLakes";
			_checkRemoveSmallLakes.GridPositionY = 4;

			_buttonGenerate = new TextButton();
			_buttonGenerate.Text = "Generate";
			_buttonGenerate.ContentHorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;
			_buttonGenerate.ContentVerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			_buttonGenerate.Id = "_buttonGenerate";
			_buttonGenerate.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_buttonGenerate.GridPositionY = 5;

			var textBlock3 = new TextBlock();
			textBlock3.Text = "View Mode:";

			var listItem5 = new ListItem();
			listItem5.Text = "Normal";

			var listItem6 = new ListItem();
			listItem6.Text = "Siluette";

			_comboViewMode = new ComboBox();
			_comboViewMode.SelectedIndex = 0;
			_comboViewMode.Id = "_comboViewMode";
			_comboViewMode.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_comboViewMode.GridPositionX = 1;
			_comboViewMode.Items.Add(listItem5);
			_comboViewMode.Items.Add(listItem6);

			var grid2 = new Grid();
			grid2.DrawLinesColor = Color.White;
			grid2.ColumnSpacing = 8;
			grid2.ColumnsProportions.Add(new Grid.Proportion());
			grid2.ColumnsProportions.Add(new Grid.Proportion
			{
				Type = Myra.Graphics2D.UI.Grid.ProportionType.Fill,
			});
			grid2.RowsProportions.Add(new Grid.Proportion());
			grid2.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			grid2.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Stretch;
			grid2.GridPositionY = 6;
			grid2.Widgets.Add(textBlock3);
			grid2.Widgets.Add(_comboViewMode);

			_buttonSaveAsPng = new TextButton();
			_buttonSaveAsPng.Text = "Save As PNG...";
			_buttonSaveAsPng.ContentHorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;
			_buttonSaveAsPng.ContentVerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			_buttonSaveAsPng.Id = "_buttonSaveAsPng";
			_buttonSaveAsPng.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_buttonSaveAsPng.GridPositionY = 7;

			var grid3 = new Grid();
			grid3.DrawLinesColor = Color.White;
			grid3.RowSpacing = 8;
			grid3.ColumnsProportions.Add(new Grid.Proportion
			{
				Type = Myra.Graphics2D.UI.Grid.ProportionType.Fill,
			});
			grid3.RowsProportions.Add(new Grid.Proportion());
			grid3.RowsProportions.Add(new Grid.Proportion());
			grid3.RowsProportions.Add(new Grid.Proportion());
			grid3.RowsProportions.Add(new Grid.Proportion());
			grid3.RowsProportions.Add(new Grid.Proportion());
			grid3.RowsProportions.Add(new Grid.Proportion());
			grid3.RowsProportions.Add(new Grid.Proportion());
			grid3.RowsProportions.Add(new Grid.Proportion());
			grid3.RowsProportions.Add(new Grid.Proportion());
			grid3.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			grid3.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Stretch;
			grid3.Widgets.Add(grid1);
			grid3.Widgets.Add(_checkSurrondedByWater);
			grid3.Widgets.Add(_checkSmooth);
			grid3.Widgets.Add(_checkRemoveSmalIslands);
			grid3.Widgets.Add(_checkRemoveSmallLakes);
			grid3.Widgets.Add(_buttonGenerate);
			grid3.Widgets.Add(grid2);
			grid3.Widgets.Add(_buttonSaveAsPng);

			_imageGenerated = new Image();
			_imageGenerated.Color = Color.White;
			_imageGenerated.Id = "_imageGenerated";
			_imageGenerated.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_imageGenerated.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Stretch;

			_textGenerating = new TextBlock();
			_textGenerating.Text = "Generating...";
			_textGenerating.Id = "_textGenerating";
			_textGenerating.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;
			_textGenerating.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;

			var panel1 = new Panel();
			panel1.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			panel1.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Stretch;
			panel1.GridPositionX = 2;
			panel1.Widgets.Add(_imageGenerated);
			panel1.Widgets.Add(_textGenerating);

			
			Id = "_splitPane";
			HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Stretch;
			Widgets.Add(grid3);
			Widgets.Add(panel1);
		}

		
		public ComboBox _comboSize;
		public SpinButton _spinVariability;
		public TextBlock _textWaterLand;
		public HorizontalSlider _sliderWaterLand;
		public CheckBox _checkSurrondedByWater;
		public CheckBox _checkSmooth;
		public CheckBox _checkRemoveSmalIslands;
		public CheckBox _checkRemoveSmallLakes;
		public TextButton _buttonGenerate;
		public ComboBox _comboViewMode;
		public TextButton _buttonSaveAsPng;
		public Image _imageGenerated;
		public TextBlock _textGenerating;
	}
}