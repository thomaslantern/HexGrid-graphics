namespace HexGrid
{
    partial class hexGrid
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
            this.Randomizer = new System.Windows.Forms.Button();
            this.PossibleMoves = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Randomizer
            // 
            this.Randomizer.Location = new System.Drawing.Point(22, 82);
            this.Randomizer.Name = "Randomizer";
            this.Randomizer.Size = new System.Drawing.Size(241, 23);
            this.Randomizer.TabIndex = 0;
            this.Randomizer.Text = "Randomize Objects";
            this.Randomizer.UseVisualStyleBackColor = true;
            this.Randomizer.Click += new System.EventHandler(this.Randomizer_Click);
            // 
            // PossibleMoves
            // 
            this.PossibleMoves.Location = new System.Drawing.Point(22, 111);
            this.PossibleMoves.Name = "PossibleMoves";
            this.PossibleMoves.Size = new System.Drawing.Size(241, 23);
            this.PossibleMoves.TabIndex = 1;
            this.PossibleMoves.Text = "Find All Possible Moves";
            this.PossibleMoves.UseVisualStyleBackColor = true;
            this.PossibleMoves.Click += new System.EventHandler(this.PossibleMoves_Click);
            // 
            // hexGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 741);
            this.Controls.Add(this.PossibleMoves);
            this.Controls.Add(this.Randomizer);
            this.Name = "hexGrid";
            this.Text = "Hexgrid Pathfinder";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Randomizer;
        private System.Windows.Forms.Button PossibleMoves;
    }
}

