using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Input;
using TetrisAbstract.Extension;
using TetrisAbstract.GameClasses;

namespace TetrisWPF.Windows
{
    /// <summary>
    /// Interaction logic for OpenGameWindow.xaml
    /// </summary>
    public partial class OpenGameWindow
    {
        public OpenGameWindow(List<GameBoardData> savePoints)
        {
            InitializeComponent();
            Table.ItemsSource = savePoints.ToDataTable().DefaultView;
            _savePoints = savePoints;
        }

        public Action<int> ChooseSavePointEvent;
        private readonly List<GameBoardData> _savePoints;
        private void Table_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var v = (DataRowView)Table.SelectedItem;
            int index = int.Parse(v.Row.ItemArray[0].ToString());
            ChooseSavePointEvent(_savePoints[index - 1].IdSavePoint);
            Close();
        }
    }
}
