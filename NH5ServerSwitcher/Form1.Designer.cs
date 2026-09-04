namespace NH5ServerSwitcher
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnSelectFolder = new Button();
            txtGamePath = new TextBox();
            btnApplyPatch = new Button();
            btnUnlockSeasonPass = new Button();
            btnRestore = new Button();
            lblStatus = new Label();
            SuspendLayout();
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new Point(24, 23);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(125, 23);
            btnSelectFolder.TabIndex = 0;
            btnSelectFolder.Text = "Browse NH5 Folder";
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectFolder_Click;
            // 
            // txtGamePath
            // 
            txtGamePath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtGamePath.Location = new Point(155, 24);
            txtGamePath.Name = "txtGamePath";
            txtGamePath.Size = new Size(621, 23);
            txtGamePath.TabIndex = 1;
            // 
            // btnApplyPatch
            // 
            btnApplyPatch.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnApplyPatch.Location = new Point(24, 415);
            btnApplyPatch.Name = "btnApplyPatch";
            btnApplyPatch.Size = new Size(172, 23);
            btnApplyPatch.TabIndex = 2;
            btnApplyPatch.Text = "Apply Custom Server Patch";
            btnApplyPatch.UseVisualStyleBackColor = true;
            btnApplyPatch.Click += btnApplyPatch_Click;
            // 
            // btnUnlockSeasonPass
            // 
            btnUnlockSeasonPass.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnUnlockSeasonPass.Location = new Point(202, 415);
            btnUnlockSeasonPass.Name = "btnUnlockSeasonPass";
            btnUnlockSeasonPass.Size = new Size(100, 23);
            btnUnlockSeasonPass.TabIndex = 3;
            btnUnlockSeasonPass.Text = "Unlock DLC";
            btnUnlockSeasonPass.UseVisualStyleBackColor = true;
            btnUnlockSeasonPass.Click += btnUnlockSeasonPass_Click;
            // 
            // btnRestore
            // 
            btnRestore.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRestore.Location = new Point(308, 415);
            btnRestore.Name = "btnRestore";
            btnRestore.Size = new Size(86, 23);
            btnRestore.TabIndex = 4;
            btnRestore.Text = "Restore All";
            btnRestore.UseVisualStyleBackColor = true;
            btnRestore.Click += btnRestore_Click;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(400, 419);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(77, 15);
            lblStatus.TabIndex = 5;
            lblStatus.Text = "Status: Ready";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblStatus);
            Controls.Add(btnRestore);
            Controls.Add(btnUnlockSeasonPass);
            Controls.Add(btnApplyPatch);
            Controls.Add(txtGamePath);
            Controls.Add(btnSelectFolder);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "NASCAR Heat 5 Server Utility";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSelectFolder;
        private TextBox txtGamePath;
        private Button btnApplyPatch;
        private Button btnUnlockSeasonPass;
        private Button btnRestore;
        private Label lblStatus;
    }
}