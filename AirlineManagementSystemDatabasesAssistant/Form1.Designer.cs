
namespace AirlineManagementSystemDatabasesAssistant
{
    partial class Form1
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
            this.btnGetAll = new System.Windows.Forms.Button();
            this.cmbTableNames = new System.Windows.Forms.ComboBox();
            this.pnlControlsHolder = new System.Windows.Forms.Panel();
            this.pbxItemImage = new System.Windows.Forms.PictureBox();
            this.rtbItemInfo = new System.Windows.Forms.RichTextBox();
            this.cmbSelectedResult = new System.Windows.Forms.ComboBox();
            this.lblWaitMessage = new System.Windows.Forms.Label();
            this.pnlControlsHolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxItemImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetAll
            // 
            this.btnGetAll.Location = new System.Drawing.Point(3, 3);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(75, 23);
            this.btnGetAll.TabIndex = 0;
            this.btnGetAll.Text = "btnGetAll";
            this.btnGetAll.UseVisualStyleBackColor = true;
            // 
            // cmbTableNames
            // 
            this.cmbTableNames.FormattingEnabled = true;
            this.cmbTableNames.Location = new System.Drawing.Point(84, 5);
            this.cmbTableNames.Name = "cmbTableNames";
            this.cmbTableNames.Size = new System.Drawing.Size(143, 21);
            this.cmbTableNames.TabIndex = 1;
            // 
            // pnlControlsHolder
            // 
            this.pnlControlsHolder.Controls.Add(this.pbxItemImage);
            this.pnlControlsHolder.Controls.Add(this.rtbItemInfo);
            this.pnlControlsHolder.Controls.Add(this.cmbSelectedResult);
            this.pnlControlsHolder.Controls.Add(this.btnGetAll);
            this.pnlControlsHolder.Controls.Add(this.cmbTableNames);
            this.pnlControlsHolder.Location = new System.Drawing.Point(2, 51);
            this.pnlControlsHolder.Name = "pnlControlsHolder";
            this.pnlControlsHolder.Size = new System.Drawing.Size(795, 397);
            this.pnlControlsHolder.TabIndex = 2;
            // 
            // pbxItemImage
            // 
            this.pbxItemImage.Location = new System.Drawing.Point(11, 60);
            this.pbxItemImage.Name = "pbxItemImage";
            this.pbxItemImage.Size = new System.Drawing.Size(120, 106);
            this.pbxItemImage.TabIndex = 4;
            this.pbxItemImage.TabStop = false;
            // 
            // rtbItemInfo
            // 
            this.rtbItemInfo.Location = new System.Drawing.Point(148, 60);
            this.rtbItemInfo.Name = "rtbItemInfo";
            this.rtbItemInfo.Size = new System.Drawing.Size(638, 314);
            this.rtbItemInfo.TabIndex = 3;
            this.rtbItemInfo.Text = "";
            // 
            // cmbSelectedResult
            // 
            this.cmbSelectedResult.FormattingEnabled = true;
            this.cmbSelectedResult.Location = new System.Drawing.Point(3, 32);
            this.cmbSelectedResult.Name = "cmbSelectedResult";
            this.cmbSelectedResult.Size = new System.Drawing.Size(783, 21);
            this.cmbSelectedResult.TabIndex = 2;
            // 
            // lblWaitMessage
            // 
            this.lblWaitMessage.AutoEllipsis = true;
            this.lblWaitMessage.Location = new System.Drawing.Point(2, 9);
            this.lblWaitMessage.Name = "lblWaitMessage";
            this.lblWaitMessage.Size = new System.Drawing.Size(795, 39);
            this.lblWaitMessage.TabIndex = 2;
            this.lblWaitMessage.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblWaitMessage);
            this.Controls.Add(this.pnlControlsHolder);
            this.Name = "Form1";
            this.Text = "AirlineManagementSystemDatabasesAssistant";
            this.pnlControlsHolder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxItemImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGetAll;
        private System.Windows.Forms.ComboBox cmbTableNames;
        private System.Windows.Forms.Panel pnlControlsHolder;
        private System.Windows.Forms.Label lblWaitMessage;
        private System.Windows.Forms.ComboBox cmbSelectedResult;
        private System.Windows.Forms.PictureBox pbxItemImage;
        private System.Windows.Forms.RichTextBox rtbItemInfo;
    }
}

