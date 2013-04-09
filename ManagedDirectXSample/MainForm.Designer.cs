namespace ManagedDirectX
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelCanvas = new System.Windows.Forms.Panel();
            this.buttonYPlux = new System.Windows.Forms.Button();
            this.buttonYMinus = new System.Windows.Forms.Button();
            this.buttonXMinus = new System.Windows.Forms.Button();
            this.buttonXPlus = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.panelCanvas.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCanvas
            // 
            this.panelCanvas.BackColor = System.Drawing.Color.GhostWhite;
            this.panelCanvas.Controls.Add(this.buttonYPlux);
            this.panelCanvas.Controls.Add(this.buttonYMinus);
            this.panelCanvas.Controls.Add(this.buttonXMinus);
            this.panelCanvas.Controls.Add(this.buttonXPlus);
            this.panelCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCanvas.Location = new System.Drawing.Point(0, 0);
            this.panelCanvas.Name = "panelCanvas";
            this.panelCanvas.Size = new System.Drawing.Size(584, 562);
            this.panelCanvas.TabIndex = 0;
            // 
            // buttonYPlux
            // 
            this.buttonYPlux.Location = new System.Drawing.Point(457, 469);
            this.buttonYPlux.Name = "buttonYPlux";
            this.buttonYPlux.Size = new System.Drawing.Size(75, 23);
            this.buttonYPlux.TabIndex = 2;
            this.buttonYPlux.Text = "Y ↑";
            this.buttonYPlux.UseVisualStyleBackColor = true;
            this.buttonYPlux.Click += new System.EventHandler(this.buttonYPlux_Click);
            // 
            // buttonYMinus
            // 
            this.buttonYMinus.Location = new System.Drawing.Point(457, 527);
            this.buttonYMinus.Name = "buttonYMinus";
            this.buttonYMinus.Size = new System.Drawing.Size(75, 23);
            this.buttonYMinus.TabIndex = 3;
            this.buttonYMinus.Text = "Y ↓";
            this.buttonYMinus.UseVisualStyleBackColor = true;
            this.buttonYMinus.Click += new System.EventHandler(this.buttonYMinus_Click);
            // 
            // buttonXMinus
            // 
            this.buttonXMinus.Location = new System.Drawing.Point(416, 498);
            this.buttonXMinus.Name = "buttonXMinus";
            this.buttonXMinus.Size = new System.Drawing.Size(75, 23);
            this.buttonXMinus.TabIndex = 1;
            this.buttonXMinus.Text = "X ←";
            this.buttonXMinus.UseVisualStyleBackColor = true;
            this.buttonXMinus.Click += new System.EventHandler(this.buttonXMinus_Click);
            // 
            // buttonXPlus
            // 
            this.buttonXPlus.Location = new System.Drawing.Point(497, 498);
            this.buttonXPlus.Name = "buttonXPlus";
            this.buttonXPlus.Size = new System.Drawing.Size(75, 23);
            this.buttonXPlus.TabIndex = 0;
            this.buttonXPlus.Text = "X →";
            this.buttonXPlus.UseVisualStyleBackColor = true;
            this.buttonXPlus.Click += new System.EventHandler(this.buttonXPlus_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.panelCanvas);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Managed DirectX";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelCanvas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCanvas;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Button buttonYPlux;
        private System.Windows.Forms.Button buttonYMinus;
        private System.Windows.Forms.Button buttonXMinus;
        private System.Windows.Forms.Button buttonXPlus;
    }
}

