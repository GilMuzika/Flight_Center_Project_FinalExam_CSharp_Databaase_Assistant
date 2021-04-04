using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineManagementSystemDatabasesAssistant
{
    class SizeChanger
    {
        public int MainFormInitialWidth { get; private set; }
        public int MainFormInitialHeight { get; private set; }
        private Control[] _controlsForChangingSize;
        public int[][] ControlsForChangingSizeDimentions { get; private set; }

        public SizeChanger(int mainFormWidth, int mainFormHeight, params Control[] controlsForChangingSize)
        {
            MainFormInitialWidth = mainFormWidth;
            MainFormInitialHeight = mainFormHeight;
            _controlsForChangingSize = controlsForChangingSize;

            ControlsForChangingSizeDimentions = new int[_controlsForChangingSize.Length][];

            for(int i = 0; i < _controlsForChangingSize.Length; i++)
            {
                ControlsForChangingSizeDimentions[i] = new int[] { _controlsForChangingSize[i].Width, _controlsForChangingSize[i].Height };
            }
        }

        public void Resize(int currentmainFormWidth, int currentMainFormHeight, out int resizefactorX, out int resizeFactorY)
        {
            int resizefactorXinternal = currentmainFormWidth - MainFormInitialWidth;
            int resizefactorYinternal = currentMainFormHeight - MainFormInitialHeight;
            resizefactorX = resizefactorXinternal;
            resizeFactorY = resizefactorYinternal;
        }
    }
}
