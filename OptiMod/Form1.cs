using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PTSerializer;

namespace OptiMod
{
    public partial class Form1 : Form
    {
        private Module _mod;
        private string _fileName;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            LoadMod();
        }

        private void LoadMod()
        {
            try
            {
                var ofd = new OpenFileDialog();
                ofd.Filter = "Protacker Module|*.mod;mod.*|All files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var ser = new PTSerializer.PTSerializer();
                    _mod = ser.DeSerializeMod(ofd.FileName);
                    lblName.Text = _mod.Name;
                    RefreshDisplay();
                    _fileName = Path.GetFileName(ofd.FileName);
                    lblPatternsA.Text = string.Format("Patterns used: {0}", _mod.SongPositions.Max() + 1);
                    lblSizeA.Text = string.Format("Size: {0}", new FileInfo(ofd.FileName).Length);
 
                    btnFullOptimse.Enabled = true;
                    btnRemoveDupePatterns.Enabled = true;
                    btnOptimiseSampleLengths.Enabled = true;
                    btnRemoveUnsedSamples.Enabled = true;
                    btnRemoveUnusedPatterns.Enabled = true;
                    btnSave.Enabled = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("Error loading module: ", e.Message));
            }
        }

        private void RefreshDisplay()
        {
            lvwSamples.Items.Clear();
            foreach (var sample in _mod.Samples)
            {
                lvwSamples.Items.Add(GetSampleDetail(sample));
            }

            lblPatternsB.Text = string.Format("Patterns used: {0}", _mod.SongPositions.Max() + 1);
            var ser = new PTSerializer.PTSerializer();
            var data = ser.SerializeMod(_mod);
            lblSizeB.Text = string.Format("Size: {0}", data.Length);
        }

        private ListViewItem GetSampleDetail(Sample sample)
        {
            var detail = new List<string>();
            detail.Add(sample.Name);
            detail.Add(sample.Volume.ToString());
            detail.Add(sample.FineTune.ToString());
            detail.Add(sample.Length.ToString());
            detail.Add(sample.RepeatStart.ToString());
            detail.Add(sample.RepeatLength.ToString());
            return (new ListViewItem(detail.ToArray()));
        }

        private void btnOptiLength_Click(object sender, EventArgs e)
        {
            OptimiseSampleLenghts();
        }

        private void OptimiseSampleLenghts()
        {
            _mod.OptimseLoopedSamples();
            RefreshDisplay();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RemoveUnsedPatterns();
        }

        private void RemoveUnsedPatterns()
        {
            _mod.RemoveUnusedPatterns();
            RefreshDisplay();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveMod();
        }

        private void SaveMod()
        {
            var sfd = new SaveFileDialog();
            sfd.FileName = _fileName;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var ser = new PTSerializer.PTSerializer();
                File.WriteAllBytes(sfd.FileName, ser.SerializeMod(_mod));
            }
        }

        private void btnRemoveUnsedSamples_Click(object sender, EventArgs e)
        {
            RemoveUnusedSamples();
        }

        private void RemoveUnusedSamples()
        {
            _mod.RemoteUnusedSamples();
            RefreshDisplay();
        }

        private void btnRemoveDupePatterns_Click(object sender, EventArgs e)
        {
            RemoveDupePatterns();
        }

        private void RemoveDupePatterns()
        {
            _mod.RemoveDuplicatePatterns();
            RefreshDisplay();
        }

        private void btnFullOptimse_Click(object sender, EventArgs e)
        {
            RemoveUnsedPatterns();
            RemoveDupePatterns();
            RemoveUnusedSamples();
            OptimiseSampleLenghts();
        }
    }
}
