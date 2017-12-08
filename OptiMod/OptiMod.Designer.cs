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
            this.btnSave.Text = "Save";
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
            this.btnImportASCII.Location = new System.Drawing.Point(517, 311);
            this.btnImportASCII.Name = "btnImportASCII";
            this.btnImportASCII.Size = new System.Drawing.Size(158, 23);
            this.btnImportASCII.TabIndex = 3;
            this.btnImportASCII.Text = "Import ASCII Sample Names";
            this.btnImportASCII.UseVisualStyleBackColor = true;
            this.btnImportASCII.Click += new System.EventHandler(this.btnImportASCII_Click);
            // 
            // OptiMod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 652);
            this.Controls.Add(this.lblSizeB);
            this.Controls.Add(this.lblSizeA);
            this.Controls.Add(this.lblPatternsB);
            this.Controls.Add(this.lblPatternsA);
            this.Controls.Add(this.btnRemoveDupePatterns);
            this.Controls.Add(this.btnRemoveUnsedSamples);
            this.Controls.Add(this.btnRemoveUnusedPatterns);
            this.Controls.Add(this.btnImportASCII);
            this.Controls.Add(this.btnFullOptimse);
            this.Controls.Add(this.btnZeroLeadingSamples);
            this.Controls.Add(this.btnOptimiseSampleLengths);
            this.Controls.Add(this.lvwSamples);
            this.Controls.Add(this.lblName);
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
    }
}

