namespace NN
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
			this.Datensatz = new System.Windows.Forms.Button();
			this.Netzwerk = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.panel3 = new System.Windows.Forms.Panel();
			this.button4 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// Datensatz
			// 
			this.Datensatz.Location = new System.Drawing.Point(0, 0);
			this.Datensatz.Name = "Datensatz";
			this.Datensatz.Size = new System.Drawing.Size(213, 114);
			this.Datensatz.TabIndex = 0;
			this.Datensatz.Text = "Datensatz generieren";
			this.Datensatz.UseVisualStyleBackColor = true;
			this.Datensatz.Click += new System.EventHandler(this.datensatzGenerieren);
			// 
			// Netzwerk
			// 
			this.Netzwerk.Location = new System.Drawing.Point(219, 3);
			this.Netzwerk.Name = "Netzwerk";
			this.Netzwerk.Size = new System.Drawing.Size(213, 113);
			this.Netzwerk.TabIndex = 1;
			this.Netzwerk.Text = "Netzwerk ausführen";
			this.Netzwerk.UseVisualStyleBackColor = true;
			this.Netzwerk.Click += new System.EventHandler(this.netzwerkAusfuehren);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.Datensatz);
			this.panel1.Controls.Add(this.Netzwerk);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(451, 133);
			this.panel1.TabIndex = 2;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.textBox1);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Controls.Add(this.textBox4);
			this.panel2.Controls.Add(this.button3);
			this.panel2.Controls.Add(this.button2);
			this.panel2.Controls.Add(this.button1);
			this.panel2.Location = new System.Drawing.Point(12, 151);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(959, 131);
			this.panel2.TabIndex = 3;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(441, 7);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(213, 111);
			this.button3.TabIndex = 2;
			this.button3.Text = "Octa threaded write to array -> disk";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.octaArray);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(222, 7);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(213, 109);
			this.button2.TabIndex = 1;
			this.button2.Text = "Octa threaded write to disk";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.octaFile);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(3, 5);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(213, 111);
			this.button1.TabIndex = 0;
			this.button1.Text = "Single threaded write to disk";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.singleThreaded);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label6);
			this.panel3.Controls.Add(this.textBox6);
			this.panel3.Controls.Add(this.checkBox2);
			this.panel3.Controls.Add(this.checkBox1);
			this.panel3.Controls.Add(this.textBox5);
			this.panel3.Controls.Add(this.label5);
			this.panel3.Controls.Add(this.textBox3);
			this.panel3.Controls.Add(this.button4);
			this.panel3.Controls.Add(this.button6);
			this.panel3.Controls.Add(this.label3);
			this.panel3.Controls.Add(this.label2);
			this.panel3.Controls.Add(this.textBox2);
			this.panel3.Location = new System.Drawing.Point(12, 288);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(1204, 131);
			this.panel3.TabIndex = 4;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(222, 5);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(213, 111);
			this.button4.TabIndex = 2;
			this.button4.Text = "Read from file";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(3, 5);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(213, 111);
			this.button6.TabIndex = 0;
			this.button6.Text = "RealTime Generation";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.realTime);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(441, 5);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(268, 17);
			this.label2.TabIndex = 6;
			this.label2.Text = "Hier die Anzahl der Datensätze eingeben";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(469, 25);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(213, 22);
			this.textBox2.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(480, 52);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(192, 17);
			this.label3.TabIndex = 7;
			this.label3.Text = "Filename (only read from file)";
			this.label3.Click += new System.EventHandler(this.label3_Click);
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(469, 80);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(213, 22);
			this.textBox3.TabIndex = 8;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(688, 82);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(213, 22);
			this.textBox1.TabIndex = 12;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(774, 54);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 17);
			this.label1.TabIndex = 11;
			this.label1.Text = "Seed";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(660, 7);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(268, 17);
			this.label4.TabIndex = 10;
			this.label4.Text = "Hier die Anzahl der Datensätze eingeben";
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(688, 27);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(213, 22);
			this.textBox4.TabIndex = 9;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(774, 5);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(62, 17);
			this.label5.TabIndex = 9;
			this.label5.Text = "Lernrate";
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(755, 25);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(100, 22);
			this.textBox5.TabIndex = 10;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(725, 80);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(152, 21);
			this.checkBox1.TabIndex = 11;
			this.checkBox1.Text = "Write weights to file";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(992, 80);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(123, 21);
			this.checkBox2.TabIndex = 12;
			this.checkBox2.Text = "Import from file";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// textBox6
			// 
			this.textBox6.Location = new System.Drawing.Point(992, 25);
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(100, 22);
			this.textBox6.TabIndex = 13;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(1004, 5);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(76, 17);
			this.label6.TabIndex = 14;
			this.label6.Text = "Dateiname";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1393, 756);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button Datensatz;
		private System.Windows.Forms.Button Netzwerk;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.CheckBox checkBox2;
	}
}