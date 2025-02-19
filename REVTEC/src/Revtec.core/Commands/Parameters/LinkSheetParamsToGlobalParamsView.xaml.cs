using System.Windows;
using System.Windows.Controls;

namespace Revtec.core.Commands.Parameters
{
	/// <summary>
	/// Interaction logic for LinkSheetParamsToGlobalParams.xaml
	/// </summary>
	public partial class LinkSheetParamsToGlobalParamsView : Window
	{
		public LinkSheetParamsToGlobalParamsView()
		{
			InitializeComponent();
			PopulateStackPanel();
			PopulateResults();
		}

		private void PopulateStackPanel()
		{
			foreach (var (sheetParam, globalParam) in LinkSheetParamsToGlobalParamsCommand.sheet_param_to_global_param)
			{
				StackPanel row = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(1) };

				TextBlock sheetTextBlock = new TextBlock
				{
					Text = sheetParam,
					Width = 240,
					VerticalAlignment = VerticalAlignment.Center
				};

				TextBlock globalTextBlock = new TextBlock
				{
					Text = globalParam,
					Width = 240,
					VerticalAlignment = VerticalAlignment.Center
				};

				row.Children.Add(sheetTextBlock);
				row.Children.Add(globalTextBlock);

				ParamStackPanel.Children.Add(row);
			}
		}

		private void PopulateResults()
		{
			Val1.Text = LinkSheetParamsToGlobalParamsCommand.TotalViewSheets.ToString();

			Val2.Text = LinkSheetParamsToGlobalParamsCommand.SheetsThatHaveAllSheetParams.ToString();
			Val3.Text = LinkSheetParamsToGlobalParamsCommand.SheetsThatHaveSomeSheetParams.ToString();
			Val4.Text = LinkSheetParamsToGlobalParamsCommand.SheetsThatHaveNoneSheetParams.ToString();

			Val5.Text = LinkSheetParamsToGlobalParamsCommand.sheet_param_to_global_param.Count.ToString();
			Val6.Text = LinkSheetParamsToGlobalParamsCommand.TotalGlobalParamNamesInTheProject.ToString();

			Val7.Text = LinkSheetParamsToGlobalParamsCommand.TotalPassSheets.ToString();
			Val8.Text = LinkSheetParamsToGlobalParamsCommand.TotalFailSheets.ToString();

		}

	}

}

