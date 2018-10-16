using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TetrisAbstract.Extension;
using TetrisAbstract.GameClasses;

namespace TetrisForms
{
    public partial class OpenGameForm : Form
    {
        public OpenGameForm(List<GameBoardData> savePoints)
        {
            InitializeComponent();
            _savePoints = savePoints;
            SavePointsDGridView.DataSource = savePoints.ToDataTable().DefaultView;
        }

        public event Action<int> ChooseSavePointEvent;

        private void SavePointsDGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = int.Parse(SavePointsDGridView.Rows[e.RowIndex].Cells[0].Value.ToString());

            if (ChooseSavePointEvent != null)
            {
                ChooseSavePointEvent(_savePoints[index - 1].IdSavePoint);
            }
            Close();
        }
        private readonly List<GameBoardData> _savePoints;
    }
}
