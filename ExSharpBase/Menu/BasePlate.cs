using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using ExSharpBase.Modules;
using ExSharpBase.Events;
using SharpDX;
using ExSharpBase.Cheat;

namespace ExSharpBase.Menu
{
    public partial class BasePlate : Form
    {
        public BasePlate()
        {
            InitializeComponent();
        }

        private void BasePlate_Load(object sender, EventArgs e)
        {
            DrawPlayerCheckBox.Checked = Drawing.DrawingProperties["DrawPlayers"];
            LootCheckBox.Checked= Drawing.DrawingProperties["DrawLoot"];
            AimbotCheckBox.Checked= Drawing.DrawingProperties["Aimbot"]  ;
            trackBar1.Value= (int)Drawing.FOV;
            chkDrawCrosshair.Checked= Drawing.DrawingProperties["DrawCrosshair"]  ;
            chk2dBox.Checked= Drawing.DrawingProperties["Draw2dBox"] ;
            chk3dBox.Checked= Drawing.DrawingProperties["Draw3dBox"] ;
            chkDrawFOV.Checked= Drawing.DrawingProperties["DrawFOV"]  ;
            chkDrawSkelton.Checked= Drawing.DrawingProperties["DrawSkelton"] ;
            MagicBulletCheckBox.Checked = Drawing.DrawingProperties["MagicBullet"] ;
        }

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NativeImport.ReleaseCapture();
                NativeImport.SendMessage(Handle, NativeImport.WM_NCLBUTTONDOWN, NativeImport.HTCAPTION, 0);
            }
        }
        //{ "DrawPlayers", true },
        //    { "Draw2dBox", true },
        //    { "Draw3dBox", false },
        //    { "DrawSkelton", true },
        //    { "DrawCrosshair", true },
        //    { "Aimbot", true },
        //    { "DrawFOV", true },
        //    { "MagicBullet", true },
        //    { "DrawLoot", true },
        //    { "DrawVehicle", true }
        private void DrawRangeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Drawing.DrawingProperties["DrawPlayers"] = DrawPlayerCheckBox.Checked;
        }

        private void MoveToMouseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Drawing.DrawingProperties["DrawLoot"] = LootCheckBox.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Drawing.DrawingProperties["Aimbot"] = AimbotCheckBox.Checked;
            Aimbot.aimbot = AimbotCheckBox.Checked;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Drawing.FOV = trackBar1.Value;
            Aimbot.raduisFov = trackBar1.Value;
        }

        private void btnBoxColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AllowFullOpen = false;
            colorDlg.AnyColor = true;
            colorDlg.SolidColorOnly = false;

            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                lblBoxColor.ForeColor = colorDlg.Color;
                var c = colorDlg.Color;
                Drawing.BoxColor =new Color(c.R,c.G,c.B,c.A);
            }
        }

        private void btnSkeltonColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AllowFullOpen = false;
            colorDlg.AnyColor = true;
            colorDlg.SolidColorOnly = false;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                lblSkeltonColor.ForeColor = colorDlg.Color;
                var c = colorDlg.Color;
                Drawing.SkeltonColor = new Color(c.R, c.G, c.B, c.A);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Drawing.DrawingProperties["DrawCrosshair"] = chkDrawCrosshair.Checked;
        }

        private void chk2dBox_CheckedChanged(object sender, EventArgs e)
        {
            Drawing.DrawingProperties["Draw2dBox"] = chk2dBox.Checked;
        }

        private void chk3dBox_CheckedChanged(object sender, EventArgs e)
        {
            Drawing.DrawingProperties["Draw3dBox"] = chk3dBox.Checked;
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            Drawing.DrawingProperties["DrawFOV"] = chkDrawFOV.Checked; 
        }

        private void chkDrawSkelton_CheckedChanged(object sender, EventArgs e)
        {
            //DrawSkelton
            Drawing.DrawingProperties["DrawSkelton"] = chkDrawSkelton.Checked;
        }

        private void MagicBulletCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Drawing.DrawingProperties["MagicBullet"] = MagicBulletCheckBox.Checked;
            Aimbot.SilentAim = MagicBulletCheckBox.Checked;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Aimbot.aimPos = (Aimbot.AimPos)radioButton1.Tag;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Aimbot.aimPos = (Aimbot.AimPos)radioButton2.Tag;
        }
    }
}
