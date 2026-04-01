namespace OptiMod
{
    partial class OptiMod
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lvwSamples = new System.Windows.Forms.ListView();
            this.Sample = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Volume = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FineTune = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Length = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RepStart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RepLen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOptimiseSampleLengths = new System.Windows.Forms.Button();
            this.btnRemoveUnusedPatterns = new System.Windows.Forms.Button();
            this.lblPatternsA = new System.Windows.Forms.Label();
            this.btnRemoveUnsedSamples = new System.Windows.Forms.Button();
            this.btnRemoveDupePatterns = new System.Windows.Forms.Button();
            this.lblSizeA = new System.Windows.Forms.Label();
            this.lblPatternsB = new System.Windows.Forms.Label();
            this.lblSizeB = new System.Windows.Forms.Label();
            this.btnFullOptimse = new System.Windows.Forms.Button();
            this.btnZeroLeadingSamples = new System.Windows.Forms.Button();
            this.btnImportASCII = new System.Windows.Forms.Button();
            this.btnTruncateToLoop = new System.Windows.Forms.Button();
            this.btnExpandLoops = new System.Windows.Forms.Button();
            this.btnPadSamples = new System.Windows.Forms.Button();
            this.btnClearSampleNames = new System.Windows.Forms.Button();
            this.btnSaveSong = new System.Windows.Forms.Button();
            this.btnSaveSamples = new System.Windows.Forms.Button();
            this.buttonRemoveFilter = new System.Windows.Forms.Button();
            this.btnClearChannel3 = new System.Windows.Forms.Button();
            this.btnClearPatBreak = new System.Windows.Forms.Button();
            this.txtStackFrom = new System.Windows.Forms.TextBox();
            this.txtStackTo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStackSamples = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(600, 12);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(600, 46);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save Mod";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 12);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(30, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Title:";
            // 
            // lvwSamples
            // 
            this.lvwSamples.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Sample,
            this.Volume,
            this.FineTune,
            this.Length,
            this.RepStart,
            this.RepLen});
            this.lvwSamples.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvwSamples.HideSelection = false;
            this.lvwSamples.Location = new System.Drawing.Point(12, 78);
            this.lvwSamples.Name = "lvwSamples";
            this.lvwSamples.Size = new System.Drawing.Size(492, 562);
            this.lvwSamples.TabIndex = 2;
            this.lvwSamples.UseCompatibleStateImageBehavior = false;
            this.lvwSamples.View = System.Windows.Forms.View.Details;
            // 
            // Sample
            // 
            this.Sample.Text = "Sample";
            this.Sample.Width = 174;
            // 
            // Volume
            // 
            this.Volume.Text = "Volume";
            this.Volume.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FineTune
            // 
            this.FineTune.Text = "FineTune";
            this.FineTune.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Length
            // 
            this.Length.Text = "Length";
            this.Length.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // RepStart
            // 
            this.RepStart.Text = "RepStart";
            this.RepStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // RepLen
            // 
            this.RepLen.Text = "RepLen";
            this.RepLen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnOptimiseSampleLengths
            // 
            this.btnOptimiseSampleLengths.Enabled = false;
            this.btnOptimiseSampleLengths.Location = new System.Drawing.Point(517, 223);
            this.btnOptimiseSampleLengths.Name = "btnOptimiseSampleLengths";
            this.btnOptimiseSampleLengths.Size = new System.Drawing.Size(158, 23);
            this.btnOptimiseSampleLengths.TabIndex = 3;
            this.btnOptimiseSampleLengths.Text = "Optimise Sample Lengths";
            this.btnOptimiseSampleLengths.UseVisualStyleBackColor = true;
            this.btnOptimiseSampleLengths.Click += new System.EventHandler(this.btnOptiLength_Click);
            // 
            // btnRemoveUnusedPatterns
            // 
            this.btnRemoveUnusedPatterns.Enabled = false;
            this.btnRemoveUnusedPatterns.Location = new System.Drawing.Point(517, 136);
            this.btnRemoveUnusedPatterns.Name = "btnRemoveUnusedPatterns";
            this.btnRemoveUnusedPatterns.Size = new System.Drawing.Size(158, 23);
            this.btnRemoveUnusedPatterns.TabIndex = 3;
            this.btnRemoveUnusedPatterns.Text = "Remove Unsed Patterns";
            this.btnRemoveUnusedPatterns.UseVisualStyleBackColor = true;
            this.btnRemoveUnusedPatterns.Click += new System.EventHandler(this.btnRemoveUnusedPatterns_Click);
            // 
            // lblPatternsA
            // 
            this.lblPatternsA.AutoSize = true;
            this.lblPatternsA.Location = new System.Drawing.Point(12, 32);
            this.lblPatternsA.Name = "lblPatternsA";
            this.lblPatternsA.Size = new System.Drawing.Size(86, 13);
            this.lblPatternsA.TabIndex = 4;
            this.lblPatternsA.Text = "Patterns Used: 0";
            // 
            // btnRemoveUnsedSamples
            // 
            this.btnRemoveUnsedSamples.Enabled = false;
            this.btnRemoveUnsedSamples.Location = new System.Drawing.Point(517, 194);
            this.btnRemoveUnsedSamples.Name = "btnRemoveUnsedSamples";
            this.btnRemoveUnsedSamples.Size = new System.Drawing.Size(158, 23);
            this.btnRemoveUnsedSamples.TabIndex = 3;
            this.btnRemoveUnsedSamples.Text = "Remove Unsed Samples";
            this.btnRemoveUnsedSamples.UseVisualStyleBackColor = true;
            this.btnRemoveUnsedSamples.Click += new System.EventHandler(this.btnRemoveUnsedSamples_Click);
            // 
            // btnRemoveDupePatterns
            // 
            this.btnRemoveDupePatterns.Enabled = false;
            this.btnRemoveDupePatterns.Location = new System.Drawing.Point(517, 165);
            this.btnRemoveDupePatterns.Name = "btnRemoveDupePatterns";
            this.btnRemoveDupePatterns.Size = new System.Drawing.Size(158, 23);
            this.btnRemoveDupePatterns.TabIndex = 3;
            this.btnRemoveDupePatterns.Text = "Remove Duplicate Patterns";
            this.btnRemoveDupePatterns.UseVisualStyleBackColor = true;
            this.btnRemoveDupePatterns.Click += new System.EventHandler(this.btnRemoveDupePatterns_Click);
            // 
            // lblSizeA
            // 
            this.lblSizeA.AutoSize = true;
            this.lblSizeA.Location = new System.Drawing.Point(12, 52);
            this.lblSizeA.Name = "lblSizeA";
            this.lblSizeA.Size = new System.Drawing.Size(39, 13);
            this.lblSizeA.TabIndex = 4;
            this.lblSizeA.Text = "Size: 0";
            // 
            // lblPatternsB
            // 
            this.lblPatternsB.AutoSize = true;
            this.lblPatternsB.Location = new System.Drawing.Point(124, 32);
            this.lblPatternsB.Name = "lblPatternsB";
            this.lblPatternsB.Size = new System.Drawing.Size(86, 13);
            this.lblPatternsB.TabIndex = 4;
            this.lblPatternsB.Text = "Patterns Used: 0";
            // 
            // lblSizeB
            // 
            this.lblSizeB.AutoSize = true;
            this.lblSizeB.Location = new System.Drawing.Point(124, 52);
            this.lblSizeB.Name = "lblSizeB";
            this.lblSizeB.Size = new System.Drawing.Size(39, 13);
            this.lblSizeB.TabIndex = 4;
            this.lblSizeB.Text = "Size: 0";
            // 
            // btnFullOptimse
            // 
            this.btnFullOptimse.Enabled = false;
            this.btnFullOptimse.Location = new System.Drawing.Point(517, 83);
            this.btnFullOptimse.Name = "btnFullOptimse";
            this.btnFullOptimse.Size = new System.Drawing.Size(158, 23);
            this.btnFullOptimse.TabIndex = 3;
            this.btnFullOptimse.Text = "Full Optimise";
            this.btnFullOptimse.UseVisualStyleBackColor = true;
            this.btnFullOptimse.Click += new System.EventHandler(this.btnFullOptimse_Click);
            // 
            // btnZeroLeadingSamples
            // 
            this.btnZeroLeadingSamples.Enabled = false;
            this.btnZeroLeadingSamples.Location = new System.Drawing.Point(518, 252);
            this.btnZeroLeadingSamples.Name = "btnZeroLeadingSamples";
            this.btnZeroLeadingSamples.Size = new System.Drawing.Size(158, 23);
            this.btnZeroLeadingSamples.TabIndex = 3;
            this.btnZeroLeadingSamples.Text = "Zero Leading Samples";
            this.btnZeroLeadingSamples.UseVisualStyleBackColor = true;
            this.btnZeroLeadingSamples.Click += new System.EventHandler(this.btnZeroLeadingSamples_Click);
            // 
            // btnImportASCII
            // 
            this.btnImportASCII.Enabled = false;
            this.btnImportASCII.Location = new System.Drawing.Point(517, 331);
            this.btnImportASCII.Name = "btnImportASCII";
            this.btnImportASCII.Size = new System.Drawing.Size(158, 23);
            this.btnImportASCII.TabIndex = 3;
            this.btnImportASCII.Text = "Import ASCII Sample Names";
            this.btnImportASCII.UseVisualStyleBackColor = true;
            this.btnImportASCII.Click += new System.EventHandler(this.btnImportASCII_Click);
            // 
            // btnTruncateToLoop
            // 
            this.btnTruncateToLoop.Enabled = false;
            this.btnTruncateToLoop.Location = new System.Drawing.Point(518, 380);
            this.btnTruncateToLoop.Name = "btnTruncateToLoop";
            this.btnTruncateToLoop.Size = new System.Drawing.Size(158, 23);
            this.btnTruncateToLoop.TabIndex = 3;
            this.btnTruncateToLoop.Text = "Truncate Sample To Loop";
            this.btnTruncateToLoop.UseVisualStyleBackColor = true;
            this.btnTruncateToLoop.Click += new System.EventHandler(this.btnTruncateToLoop_Click);
            // 
            // btnExpandLoops
            // 
            this.btnExpandLoops.Enabled = false;
            this.btnExpandLoops.Location = new System.Drawing.Point(518, 452);
            this.btnExpandLoops.Name = "btnExpandLoops";
            this.btnExpandLoops.Size = new System.Drawing.Size(158, 23);
            this.btnExpandLoops.TabIndex = 3;
            this.btnExpandLoops.Text = "Expand Pattern Loops";
            this.btnExpandLoops.UseVisualStyleBackColor = true;
            this.btnExpandLoops.Click += new System.EventHandler(this.btnExpandLoops_Click);
            // 
            // btnPadSamples
            // 
            this.btnPadSamples.Enabled = false;
            this.btnPadSamples.Location = new System.Drawing.Point(518, 545);
            this.btnPadSamples.Name = "btnPadSamples";
            this.btnPadSamples.Size = new System.Drawing.Size(158, 23);
            this.btnPadSamples.TabIndex = 3;
            this.btnPadSamples.Text = "Export Channel Mods";
            this.btnPadSamples.UseVisualStyleBackColor = true;
            this.btnPadSamples.Click += new System.EventHandler(this.btnExportFour);
            // 
            // btnClearSampleNames
            // 
            this.btnClearSampleNames.Enabled = false;
            this.btnClearSampleNames.Location = new System.Drawing.Point(518, 302);
            this.btnClearSampleNames.Name = "btnClearSampleNames";
            this.btnClearSampleNames.Size = new System.Drawing.Size(158, 23);
            this.btnClearSampleNames.TabIndex = 3;
            this.btnClearSampleNames.Text = "Clear Sample Names";
            this.btnClearSampleNames.UseVisualStyleBackColor = true;
            this.btnClearSampleNames.Click += new System.EventHandler(this.btnClearSampleNames_Click);
            // 
            // btnSaveSong
            // 
            this.btnSaveSong.Enabled = false;
            this.btnSaveSong.Location = new System.Drawing.Point(494, 12);
            this.btnSaveSong.Name = "btnSaveSong";
            this.btnSaveSong.Size = new System.Drawing.Size(100, 23);
            this.btnSaveSong.TabIndex = 0;
            this.btnSaveSong.Text = "Save Song Data";
            this.btnSaveSong.UseVisualStyleBackColor = true;
            this.btnSaveSong.Click += new System.EventHandler(this.btnSaveSong_Click);
            // 
            // btnSaveSamples
            // 
            this.btnSaveSamples.Enabled = false;
            this.btnSaveSamples.Location = new System.Drawing.Point(494, 46);
            this.btnSaveSamples.Name = "btnSaveSamples";
            this.btnSaveSamples.Size = new System.Drawing.Size(100, 23);
            this.btnSaveSamples.TabIndex = 0;
            this.btnSaveSamples.Text = "Save Samples";
            this.btnSaveSamples.UseVisualStyleBackColor = true;
            this.btnSaveSamples.Click += new System.EventHandler(this.btnSaveSamples_Click);
            // 
            // buttonRemoveFilter
            // 
            this.buttonRemoveFilter.Enabled = false;
            this.buttonRemoveFilter.Location = new System.Drawing.Point(517, 617);
            this.buttonRemoveFilter.Name = "buttonRemoveFilter";
            this.buttonRemoveFilter.Size = new System.Drawing.Size(158, 23);
            this.buttonRemoveFilter.TabIndex = 6;
            this.buttonRemoveFilter.Text = "Remove Filter Commands";
            this.buttonRemoveFilter.UseVisualStyleBackColor = true;
            this.buttonRemoveFilter.Click += new System.EventHandler(this.buttonRemoveFilter_Click);
            // 
            // btnClearChannel3
            // 
            this.btnClearChannel3.Enabled = false;
            this.btnClearChannel3.Location = new System.Drawing.Point(518, 481);
            this.btnClearChannel3.Name = "btnClearChannel3";
            this.btnClearChannel3.Size = new System.Drawing.Size(158, 23);
            this.btnClearChannel3.TabIndex = 7;
            this.btnClearChannel3.Text = "Clear Channel 3";
            this.btnClearChannel3.UseVisualStyleBackColor = true;
            this.btnClearChannel3.Click += new System.EventHandler(this.btnClearChannel3_Click);
            // 
            // btnClearPatBreak
            // 
            this.btnClearPatBreak.Enabled = false;
            this.btnClearPatBreak.Location = new System.Drawing.Point(517, 409);
            this.btnClearPatBreak.Name = "btnClearPatBreak";
            this.btnClearPatBreak.Size = new System.Drawing.Size(158, 23);
            this.btnClearPatBreak.TabIndex = 8;
            this.btnClearPatBreak.Text = "Clear After Pat Break";
            this.btnClearPatBreak.UseVisualStyleBackColor = true;
            this.btnClearPatBreak.Click += new System.EventHandler(this.btnClearPatBreak_Click);
            // 
            // txtStackFrom
            // 
            this.txtStackFrom.Location = new System.Drawing.Point(340, 14);
            this.txtStackFrom.Name = "txtStackFrom";
            this.txtStackFrom.Size = new System.Drawing.Size(100, 20);
            this.txtStackFrom.TabIndex = 9;
            // 
            // txtStackTo
            // 
            this.txtStackTo.Location = new System.Drawing.Point(340, 48);
            this.txtStackTo.Name = "txtStackTo";
            this.txtStackTo.Size = new System.Drawing.Size(100, 20);
            this.txtStackTo.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(258, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Stack From ($)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(268, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Stack To ($)";
            // 
            // btnStackSamples
            // 
            this.btnStackSamples.Location = new System.Drawing.Point(446, 18);
            this.btnStackSamples.Name = "btnStackSamples";
            this.btnStackSamples.Size = new System.Drawing.Size(25, 50);
            this.btnStackSamples.TabIndex = 11;
            this.btnStackSamples.Text = "GO";
            this.btnStackSamples.UseVisualStyleBackColor = true;
            this.btnStackSamples.Click += new System.EventHandler(this.btnStackSamples_Click);
            // 
            // OptiMod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 652);
            this.Controls.Add(this.btnStackSamples);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtStackTo);
            this.Controls.Add(this.txtStackFrom);
            this.Controls.Add(this.btnClearPatBreak);
            this.Controls.Add(this.btnClearChannel3);
            this.Controls.Add(this.buttonRemoveFilter);
            this.Controls.Add(this.lblSizeB);
            this.Controls.Add(this.lblSizeA);
            this.Controls.Add(this.lblPatternsB);
            this.Controls.Add(this.lblPatternsA);
            this.Controls.Add(this.btnRemoveDupePatterns);
            this.Controls.Add(this.btnRemoveUnsedSamples);
            this.Controls.Add(this.btnRemoveUnusedPatterns);
            this.Controls.Add(this.btnClearSampleNames);
            this.Controls.Add(this.btnImportASCII);
            this.Controls.Add(this.btnPadSamples);
            this.Controls.Add(this.btnExpandLoops);
            this.Controls.Add(this.btnFullOptimse);
            this.Controls.Add(this.btnTruncateToLoop);
            this.Controls.Add(this.btnZeroLeadingSamples);
            this.Controls.Add(this.btnOptimiseSampleLengths);
            this.Controls.Add(this.lvwSamples);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnSaveSamples);
            this.Controls.Add(this.btnSaveSong);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnOpen);
            this.Name = "OptiMod";
            this.Text = "OptiMod";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ListView lvwSamples;
        private System.Windows.Forms.ColumnHeader Sample;
        private System.Windows.Forms.ColumnHeader Volume;
        private System.Windows.Forms.ColumnHeader FineTune;
        private System.Windows.Forms.ColumnHeader Length;
        private System.Windows.Forms.ColumnHeader RepStart;
        private System.Windows.Forms.ColumnHeader RepLen;
        private System.Windows.Forms.Button btnOptimiseSampleLengths;
        private System.Windows.Forms.Button btnRemoveUnusedPatterns;
        private System.Windows.Forms.Label lblPatternsA;
        private System.Windows.Forms.Button btnRemoveUnsedSamples;
        private System.Windows.Forms.Button btnRemoveDupePatterns;
        private System.Windows.Forms.Label lblSizeA;
        private System.Windows.Forms.Label lblPatternsB;
        private System.Windows.Forms.Label lblSizeB;
        private System.Windows.Forms.Button btnFullOptimse;
        private System.Windows.Forms.Button btnZeroLeadingSamples;
        private System.Windows.Forms.Button btnImportASCII;
        private System.Windows.Forms.Button btnTruncateToLoop;
        private System.Windows.Forms.Button btnExpandLoops;
        private System.Windows.Forms.Button btnPadSamples;
        private System.Windows.Forms.Button btnClearSampleNames;
        private System.Windows.Forms.Button btnSaveSong;
        private System.Windows.Forms.Button btnSaveSamples;
        private System.Windows.Forms.Button buttonRemoveFilter;
        private System.Windows.Forms.Button btnClearChannel3;
        private System.Windows.Forms.Button btnClearPatBreak;
        private System.Windows.Forms.TextBox txtStackFrom;
        private System.Windows.Forms.TextBox txtStackTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStackSamples;
    }
}

