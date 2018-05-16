namespace Porssisimulaattori {
    partial class Porssisimulaattori {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Porssisimulaattori));
            this.Title = new System.Windows.Forms.Label();
            this.txtLogIn = new System.Windows.Forms.Label();
            this.lblEnterUsername = new System.Windows.Forms.Label();
            this.lblEnterPassword = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.LogIn = new System.Windows.Forms.Button();
            this.username = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Title
            // 
            resources.ApplyResources(this.Title, "Title");
            this.Title.Name = "Title";
            this.Title.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtLogIn
            // 
            resources.ApplyResources(this.txtLogIn, "txtLogIn");
            this.txtLogIn.Name = "txtLogIn";
            this.txtLogIn.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblEnterUsername
            // 
            resources.ApplyResources(this.lblEnterUsername, "lblEnterUsername");
            this.lblEnterUsername.Name = "lblEnterUsername";
            this.lblEnterUsername.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblEnterPassword
            // 
            resources.ApplyResources(this.lblEnterPassword, "lblEnterPassword");
            this.lblEnterPassword.Name = "lblEnterPassword";
            // 
            // password
            // 
            resources.ApplyResources(this.password, "password");
            this.password.Name = "password";
            // 
            // LogIn
            // 
            resources.ApplyResources(this.LogIn, "LogIn");
            this.LogIn.Name = "LogIn";
            this.LogIn.UseVisualStyleBackColor = true;
            this.LogIn.Click += new System.EventHandler(this.LogIn_Click);
            // 
            // username
            // 
            resources.ApplyResources(this.username, "username");
            this.username.Name = "username";
            this.username.TextChanged += new System.EventHandler(this.username_TextChanged_1);
            // 
            // Porssisimulaattori
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Porssisimulaattori.Properties.Resources.backround;
            this.Controls.Add(this.username);
            this.Controls.Add(this.LogIn);
            this.Controls.Add(this.password);
            this.Controls.Add(this.lblEnterPassword);
            this.Controls.Add(this.lblEnterUsername);
            this.Controls.Add(this.txtLogIn);
            this.Controls.Add(this.Title);
            this.Name = "Porssisimulaattori";
            this.Load += new System.EventHandler(this.Porssisimulaattori_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label txtLogIn;
        private System.Windows.Forms.Label lblEnterUsername;
        private System.Windows.Forms.Label lblEnterPassword;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Button LogIn;
        private System.Windows.Forms.TextBox username;
    }
}

