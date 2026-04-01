using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ProTrackerTools;

namespace OptiMod
{
    public partial class OptiMod : Form
    {
        private Module _mod;
        private string _fileName;

        public OptiMod()
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
                    _mod = Serializer.DeSerializeMod(ofd.FileName);
                    lblName.Text = _mod.Name;
                    RefreshDisplay();
                    _fileName = Path.GetFileName(ofd.FileName);
                    lblPatternsA.Text = string.Format("Patterns used: {0}", _mod.Patterns.Count);
                    lblSizeA.Text = string.Format("Size: {0}", new FileInfo(ofd.FileName).Length);
 
                    btnFullOptimse.Enabled = true;
                    btnRemoveDupePatterns.Enabled = true;
                    btnOptimiseSampleLengths.Enabled = true;
                    btnRemoveUnsedSamples.Enabled = true;
                    btnRemoveUnusedPatterns.Enabled = true;
                    btnSave.Enabled = true;
                    btnZeroLeadingSamples.Enabled = true;
                    btnImportASCII.Enabled = true;
                    btnTruncateToLoop.Enabled = true;
                    btnExpandLoops.Enabled = true;
                    btnPadSamples.Enabled = true;
                    btnClearSampleNames.Enabled = true;
                    buttonRemoveFilter.Enabled = true;
                    btnClearChannel3.Enabled = true;
                    btnSaveSamples.Enabled = true;
                    btnSaveSong.Enabled = true;
                    btnClearPatBreak.Enabled = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("Error loading module: {0}", e.Message));
            }
        }

        private void RefreshDisplay()
        {
            lvwSamples.Items.Clear();
            foreach (var sample in _mod.Samples)
            {
                lvwSamples.Items.Add(GetSampleDetail(sample));
            }

            lblPatternsB.Text = string.Format("Patterns used: {0}", _mod.Patterns.Count);
            var data = Serializer.SerializeMod(_mod);
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
            _mod.OptimseLoopedSamples();
            RefreshDisplay();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveMod();
        }

        private void SaveMod()
        {
            try
            {
                var sfd = new SaveFileDialog();
                sfd.FileName = _fileName;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(sfd.FileName, Serializer.SerializeMod(_mod));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("Whoops: {0}", e.Message));
            }
        }

        private void SaveSong()
        {
            try
            {
                var sfd = new SaveFileDialog();
                sfd.FileName = _fileName;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(sfd.FileName, Serializer.SerializePatternData(_mod));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("Whoops: {0}", e.Message));
            }
        }

        private void SaveSamples()
        {
            try
            {
                var sfd = new SaveFileDialog();
                sfd.FileName = _fileName;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(sfd.FileName, Serializer.SerializeSampleData(_mod));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("Whoops: {0}", e.Message));
            }
        }

        private void btnRemoveUnsedSamples_Click(object sender, EventArgs e)
        {
            _mod.RemoveUnusedSamples();
            RefreshDisplay();
        }

        private void btnRemoveDupePatterns_Click(object sender, EventArgs e)
        {
            _mod.RemoveDuplicatePatterns();
            RefreshDisplay();
        }

        private void btnFullOptimse_Click(object sender, EventArgs e)
        {
            _mod.FullOptimise();
            RefreshDisplay();
        }

        private void btnZeroLeadingSamples_Click(object sender, EventArgs e)
        {
            _mod.ZeroLeadingSamples();
            Refresh();
        }

        private void btnRemoveUnusedPatterns_Click(object sender, EventArgs e)
        {
            _mod.RemoveUnusedPatterns();
            RefreshDisplay();
        }

        private void btnImportASCII_Click(object sender, EventArgs e)
        {
            ImportASCII();
            RefreshDisplay();
        }



        private void ImportASCII()
        {
            try
            {
                var ofd = new OpenFileDialog();
                ofd.Filter = "All Files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (new FileInfo(ofd.FileName).Length > 4000)
                    {
                        MessageBox.Show("You are having a laugh right?");
                    }
                    else
                    {
                        var ascii = File.ReadAllLines(ofd.FileName);
                        for (var i = 0; i < 31; i++)
                        {
                            if (ascii.Length > i)
                            {
                                if (ascii[i].Length > 22)
                                    _mod.Samples[i].Name = ascii[i].Substring(0, 22);
                                else
                                    _mod.Samples[i].Name = ascii[i];
                            }
                            else
                            {
                                _mod.Samples[i].Name = "";
                            }
                        }
                    }
                }
                RefreshDisplay();
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("Whoops: ", e.Message));
            }
        }

        private void btnTruncateToLoop_Click(object sender, EventArgs e)
        {
            if (lvwSamples.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Select at least one sample first");
                return;
            }

            var samples = "Are you sure you want to truncate the following samples to loop data only?\n\n";
            foreach (int i in lvwSamples.SelectedIndices)
            {
                samples += lvwSamples.Items[i].Text.Replace("\0","") + "\n";
            }

            if (MessageBox.Show(samples, "Are you sure?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                foreach (int i in lvwSamples.SelectedIndices)
                {
                    if (!_mod.TruncateToLoop(i))
                    {
                        MessageBox.Show(string.Format("Unable to truncate {0}", _mod.Samples[i].Name));
                    }
                }
            }

            RefreshDisplay();
        }

        private void btnExpandLoops_Click(object sender, EventArgs e)
        {
            _mod.ExpandPatternLoops();
            RefreshDisplay();
        }

        private void btnExportFour(object sender, EventArgs e)
        {
            var results = new List<byte[]>();

            for (var chanid = 0; chanid < 4; chanid ++)
            {
                var mod = Serializer.DeSerializeMod(Serializer.SerializeMod(_mod));
                foreach (var pattern in mod.Patterns)
                {
                    for (var i = 0; i < 4; i++)
                    {
                        if (i == chanid)
                        {
                            continue;
                        }
                        
                        foreach (var patitem in pattern.Channels[i].PatternItems)
                        {
                            if (patitem.Command != CommandType.SetSpeed)
                            {
                                patitem.CommandValue = 0;
                                patitem.Command = 0;
                            }
                            patitem.SampleNumber = 0;
                            patitem.Period = 0;
                        }
                    }
                }
                
                results.Add(Serializer.SerializeMod(mod));
            }
            var ofd = new FolderBrowserDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var folder = ofd.SelectedPath;
                var modid = 0;
                foreach (var modbin in results)
                {
                    File.WriteAllBytes(Path.Combine(folder, $"{modid}.mod"), modbin);
                    modid++;
                }
            }

        }

        private void btnClearSampleNames_Click(object sender, EventArgs e)
        {
            _mod.ClearSampleNames();
            RefreshDisplay();
        }

        private void btnSaveSong_Click(object sender, EventArgs e)
        {
            SaveSong();
        }

        private void btnSaveSamples_Click(object sender, EventArgs e)
        {
            SaveSamples();
        }

        private void buttonRemoveFilter_Click(object sender, EventArgs e)
        {
            foreach (var pattern in _mod.Patterns)
            {
                foreach (var chan in pattern.Channels)
                {
                    foreach (var item in chan.PatternItems)
                    {
                        if (item.Command == CommandType.Extended && (item.CommandValue & 0xf0) == 0)
                        {
                            item.Command = CommandType.ArpNone;
                            item.CommandValue = 0x00;
                        }
                    }
                }
            }

        }

        private void btnClearChannel3_Click(object sender, EventArgs e)
        {
            _mod.ClearChannel(3);
        }

        private void btnClearPatBreak_Click(object sender, EventArgs e)
        {
            _mod.CleanAfterPatternBreaks(); 
        }

        private void btnStackSamples_Click(object sender, EventArgs e)
        {
            StackSamples();
        }

        private void StackSamples()
        {
            var sampleIdFrom = Convert.ToInt32(txtStackFrom.Text);
            var sampleIdTo = Convert.ToInt32(txtStackTo.Text);

            var sampleFrom = _mod.Samples[sampleIdFrom - 1];
            var sampleTo = _mod.Samples[sampleIdTo - 1];

            // prep new sample
            var sampleData = sampleTo.Data.ToList();
            var remain = 256 - (sampleData.Count % 256);
            sampleData.AddRange(new byte[remain]);
            var posOffset = sampleData.Count / 256;
            sampleData.AddRange(sampleFrom.Data);
            sampleTo.Data = sampleData.ToArray();
            sampleTo.Length = sampleData.Count;

            var newName = sampleTo.Name.Replace("\0", "");
            newName += $"{posOffset}";
            if (newName.Length > 20)
            {
                newName = newName.Substring(newName.Length - 20, 20);
            }
            sampleTo.Name = newName;


            // clean old sample
            sampleFrom.Data = new byte[0];
            sampleFrom.Length = 0;
            sampleFrom.Name = "----";


            foreach (var pat in _mod.Patterns)
            {
                foreach (var chan in pat.Channels)
                {
                    foreach (var item in chan.PatternItems)
                    {
                        if (item.SampleNumber == sampleIdFrom)
                        {
                            if (item.Command == CommandType.SampleOffset)
                            {
                                item.SampleNumber = sampleIdTo;
                                item.CommandValue = (byte)(item.CommandValue + posOffset); 
                            }
                            else if (item.Command == CommandType.ArpNone && item.CommandValue == 0)
                            {
                                item.SampleNumber = sampleIdTo;
                                item.Command = CommandType.SampleOffset;
                                item.CommandValue = (byte)posOffset;

                            }
                            else
                            {
                                MessageBox.Show("SHIT!");
                                break;
                            }
                        }
                    }
                }
            }



        }
    }
}
