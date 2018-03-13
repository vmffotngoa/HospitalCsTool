namespace HopitalCsTool
{
   partial class frmCstoolmain
   {
      /// <summary>
      /// 필수 디자이너 변수입니다.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// 사용 중인 모든 리소스를 정리합니다.
      /// </summary>
      /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form 디자이너에서 생성한 코드

      /// <summary>
      /// 디자이너 지원에 필요한 메서드입니다. 
      /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         this.ttbtnTip = new System.Windows.Forms.ToolTip(this.components);
         this.btnNccgmail = new System.Windows.Forms.Button();
         this.pnThreepacs = new System.Windows.Forms.Panel();
         this.cbMdcagent = new System.Windows.Forms.CheckBox();
         this.cbDtagent = new System.Windows.Forms.CheckBox();
         this.cbDeamon = new System.Windows.Forms.CheckBox();
         this.cbOnpacs = new System.Windows.Forms.CheckBox();
         this.cbCV3 = new System.Windows.Forms.CheckBox();
         this.btnThreeset = new System.Windows.Forms.Button();
         this.cbZolvue = new System.Windows.Forms.CheckBox();
         this.btnNcclogin = new System.Windows.Forms.Button();
         this.btnGrpProgram = new System.Windows.Forms.Button();
         this.btnNccdel = new System.Windows.Forms.Button();
         this.btnHomecheck = new System.Windows.Forms.Button();
         this.cmsPagesel = new System.Windows.Forms.ContextMenuStrip(this.components);
         this.cmsQnA = new System.Windows.Forms.ToolStripMenuItem();
         this.cmsWaitlist = new System.Windows.Forms.ToolStripMenuItem();
         this.cmsMwaitlist = new System.Windows.Forms.ToolStripMenuItem();
         this.계정정보복사ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.Idtsmi = new System.Windows.Forms.ToolStripMenuItem();
         this.Pwtsmi = new System.Windows.Forms.ToolStripMenuItem();
         this.Midtsmi = new System.Windows.Forms.ToolStripMenuItem();
         this.Mpwtsmi = new System.Windows.Forms.ToolStripMenuItem();
         this.cmsPageselclose = new System.Windows.Forms.ToolStripMenuItem();
         this.trDebug = new System.Windows.Forms.Timer(this.components);
         this.stsstrDebug = new System.Windows.Forms.StatusStrip();
         this.tsstslbDebug = new System.Windows.Forms.ToolStripStatusLabel();
         this.bgw = new System.ComponentModel.BackgroundWorker();
         this.btnNoparsing = new System.Windows.Forms.Button();
         this.fswDown = new System.IO.FileSystemWatcher();
         this.btnWindowtempdel = new System.Windows.Forms.Button();
         this.btnCallOfDuty = new System.Windows.Forms.Button();
         this.pnThreepacs.SuspendLayout();
         this.cmsPagesel.SuspendLayout();
         this.stsstrDebug.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.fswDown)).BeginInit();
         this.SuspendLayout();
         // 
         // btnNccgmail
         // 
         this.btnNccgmail.Location = new System.Drawing.Point(104, 12);
         this.btnNccgmail.Name = "btnNccgmail";
         this.btnNccgmail.Size = new System.Drawing.Size(85, 24);
         this.btnNccgmail.TabIndex = 2;
         this.btnNccgmail.Text = "NCC Gmail";
         this.btnNccgmail.UseVisualStyleBackColor = true;
         this.btnNccgmail.Click += new System.EventHandler(this.btnNccgmail_Click);
         // 
         // pnThreepacs
         // 
         this.pnThreepacs.Controls.Add(this.cbMdcagent);
         this.pnThreepacs.Controls.Add(this.cbDtagent);
         this.pnThreepacs.Controls.Add(this.cbDeamon);
         this.pnThreepacs.Controls.Add(this.cbOnpacs);
         this.pnThreepacs.Controls.Add(this.cbCV3);
         this.pnThreepacs.Controls.Add(this.btnThreeset);
         this.pnThreepacs.Controls.Add(this.cbZolvue);
         this.pnThreepacs.Enabled = false;
         this.pnThreepacs.Location = new System.Drawing.Point(23, 69);
         this.pnThreepacs.Name = "pnThreepacs";
         this.pnThreepacs.Size = new System.Drawing.Size(370, 139);
         this.pnThreepacs.TabIndex = 3;
         this.pnThreepacs.Visible = false;
         // 
         // cbMdcagent
         // 
         this.cbMdcagent.AutoSize = true;
         this.cbMdcagent.Location = new System.Drawing.Point(178, 63);
         this.cbMdcagent.Name = "cbMdcagent";
         this.cbMdcagent.Size = new System.Drawing.Size(125, 16);
         this.cbMdcagent.TabIndex = 10;
         this.cbMdcagent.Text = "MedicalAgent설치";
         this.cbMdcagent.UseVisualStyleBackColor = true;
         // 
         // cbDtagent
         // 
         this.cbDtagent.AutoSize = true;
         this.cbDtagent.Location = new System.Drawing.Point(178, 39);
         this.cbDtagent.Name = "cbDtagent";
         this.cbDtagent.Size = new System.Drawing.Size(115, 16);
         this.cbDtagent.TabIndex = 9;
         this.cbDtagent.Text = "DentalAgent설치";
         this.cbDtagent.UseVisualStyleBackColor = true;
         // 
         // cbDeamon
         // 
         this.cbDeamon.AutoSize = true;
         this.cbDeamon.Location = new System.Drawing.Point(178, 17);
         this.cbDeamon.Name = "cbDeamon";
         this.cbDeamon.Size = new System.Drawing.Size(99, 16);
         this.cbDeamon.TabIndex = 8;
         this.cbDeamon.Text = "Deamon 설치";
         this.cbDeamon.UseVisualStyleBackColor = true;
         // 
         // cbOnpacs
         // 
         this.cbOnpacs.AutoSize = true;
         this.cbOnpacs.Location = new System.Drawing.Point(26, 63);
         this.cbOnpacs.Name = "cbOnpacs";
         this.cbOnpacs.Size = new System.Drawing.Size(99, 16);
         this.cbOnpacs.TabIndex = 5;
         this.cbOnpacs.Text = "ONPACS설치";
         this.cbOnpacs.UseVisualStyleBackColor = true;
         // 
         // cbCV3
         // 
         this.cbCV3.AutoSize = true;
         this.cbCV3.Location = new System.Drawing.Point(26, 41);
         this.cbCV3.Name = "cbCV3";
         this.cbCV3.Size = new System.Drawing.Size(71, 16);
         this.cbCV3.TabIndex = 4;
         this.cbCV3.Text = "CV3설치";
         this.cbCV3.UseVisualStyleBackColor = true;
         // 
         // btnThreeset
         // 
         this.btnThreeset.Location = new System.Drawing.Point(91, 107);
         this.btnThreeset.Name = "btnThreeset";
         this.btnThreeset.Size = new System.Drawing.Size(75, 23);
         this.btnThreeset.TabIndex = 4;
         this.btnThreeset.Text = "작업시작";
         this.btnThreeset.UseVisualStyleBackColor = true;
         this.btnThreeset.Click += new System.EventHandler(this.btnThreeset_Click);
         // 
         // cbZolvue
         // 
         this.cbZolvue.AutoSize = true;
         this.cbZolvue.Location = new System.Drawing.Point(26, 14);
         this.cbZolvue.Name = "cbZolvue";
         this.cbZolvue.Size = new System.Drawing.Size(96, 16);
         this.cbZolvue.TabIndex = 3;
         this.cbZolvue.Text = "ZOLVUE설치";
         this.cbZolvue.UseVisualStyleBackColor = true;
         // 
         // btnNcclogin
         // 
         this.btnNcclogin.Location = new System.Drawing.Point(23, 11);
         this.btnNcclogin.Name = "btnNcclogin";
         this.btnNcclogin.Size = new System.Drawing.Size(75, 23);
         this.btnNcclogin.TabIndex = 8;
         this.btnNcclogin.Text = "NCC Con";
         this.btnNcclogin.UseVisualStyleBackColor = true;
         this.btnNcclogin.Click += new System.EventHandler(this.btnNcclogin_Click);
         // 
         // btnGrpProgram
         // 
         this.btnGrpProgram.Location = new System.Drawing.Point(195, 11);
         this.btnGrpProgram.Name = "btnGrpProgram";
         this.btnGrpProgram.Size = new System.Drawing.Size(47, 23);
         this.btnGrpProgram.TabIndex = 10;
         this.btnGrpProgram.Text = "Install";
         this.btnGrpProgram.UseVisualStyleBackColor = true;
         this.btnGrpProgram.Click += new System.EventHandler(this.btnGrpProgram_Click);
         // 
         // btnNccdel
         // 
         this.btnNccdel.Location = new System.Drawing.Point(248, 11);
         this.btnNccdel.Name = "btnNccdel";
         this.btnNccdel.Size = new System.Drawing.Size(68, 23);
         this.btnNccdel.TabIndex = 5;
         this.btnNccdel.Tag = "OLDREQDEL";
         this.btnNccdel.Text = "OldreqDel";
         this.btnNccdel.UseVisualStyleBackColor = true;
         this.btnNccdel.Click += new System.EventHandler(this.btnNccdel_Click);
         // 
         // btnHomecheck
         // 
         this.btnHomecheck.Location = new System.Drawing.Point(322, 11);
         this.btnHomecheck.Name = "btnHomecheck";
         this.btnHomecheck.Size = new System.Drawing.Size(81, 23);
         this.btnHomecheck.TabIndex = 14;
         this.btnHomecheck.Text = "HumanHome";
         this.btnHomecheck.UseVisualStyleBackColor = true;
         this.btnHomecheck.Click += new System.EventHandler(this.btnHomecheck_Click);
         // 
         // cmsPagesel
         // 
         this.cmsPagesel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsQnA,
            this.cmsWaitlist,
            this.cmsMwaitlist,
            this.계정정보복사ToolStripMenuItem,
            this.cmsPageselclose});
         this.cmsPagesel.Name = "cmsPagesel";
         this.cmsPagesel.Size = new System.Drawing.Size(159, 114);
         // 
         // cmsQnA
         // 
         this.cmsQnA.Name = "cmsQnA";
         this.cmsQnA.Size = new System.Drawing.Size(158, 22);
         this.cmsQnA.Tag = "QNA";
         this.cmsQnA.Text = "원장님들답변";
         this.cmsQnA.Click += new System.EventHandler(this.tsmiHumanhome_Click);
         // 
         // cmsWaitlist
         // 
         this.cmsWaitlist.Name = "cmsWaitlist";
         this.cmsWaitlist.Size = new System.Drawing.Size(158, 22);
         this.cmsWaitlist.Tag = "WAITLIST";
         this.cmsWaitlist.Text = "예약확인";
         this.cmsWaitlist.Click += new System.EventHandler(this.tsmiHumanhome_Click);
         // 
         // cmsMwaitlist
         // 
         this.cmsMwaitlist.Name = "cmsMwaitlist";
         this.cmsMwaitlist.Size = new System.Drawing.Size(158, 22);
         this.cmsMwaitlist.Tag = "MWAITLIST";
         this.cmsMwaitlist.Text = "모바일예약확인";
         this.cmsMwaitlist.Click += new System.EventHandler(this.tsmiHumanhome_Click);
         // 
         // 계정정보복사ToolStripMenuItem
         // 
         this.계정정보복사ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Idtsmi,
            this.Pwtsmi,
            this.Midtsmi,
            this.Mpwtsmi});
         this.계정정보복사ToolStripMenuItem.Name = "계정정보복사ToolStripMenuItem";
         this.계정정보복사ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
         this.계정정보복사ToolStripMenuItem.Text = "계정정보복사";
         // 
         // Idtsmi
         // 
         this.Idtsmi.Name = "Idtsmi";
         this.Idtsmi.Size = new System.Drawing.Size(106, 22);
         this.Idtsmi.Tag = "ID";
         this.Idtsmi.Text = "ID";
         this.Idtsmi.Click += new System.EventHandler(this.tsmiHumanhome_Click);
         // 
         // Pwtsmi
         // 
         this.Pwtsmi.Name = "Pwtsmi";
         this.Pwtsmi.Size = new System.Drawing.Size(106, 22);
         this.Pwtsmi.Tag = "PW";
         this.Pwtsmi.Text = "PW";
         this.Pwtsmi.Click += new System.EventHandler(this.tsmiHumanhome_Click);
         // 
         // Midtsmi
         // 
         this.Midtsmi.Name = "Midtsmi";
         this.Midtsmi.Size = new System.Drawing.Size(106, 22);
         this.Midtsmi.Tag = "MID";
         this.Midtsmi.Text = "M.ID";
         this.Midtsmi.Click += new System.EventHandler(this.tsmiHumanhome_Click);
         // 
         // Mpwtsmi
         // 
         this.Mpwtsmi.Name = "Mpwtsmi";
         this.Mpwtsmi.Size = new System.Drawing.Size(106, 22);
         this.Mpwtsmi.Tag = "MPW";
         this.Mpwtsmi.Text = "M.PW";
         this.Mpwtsmi.Click += new System.EventHandler(this.tsmiHumanhome_Click);
         // 
         // cmsPageselclose
         // 
         this.cmsPageselclose.Name = "cmsPageselclose";
         this.cmsPageselclose.Size = new System.Drawing.Size(158, 22);
         this.cmsPageselclose.Tag = "CLOSE";
         this.cmsPageselclose.Text = "점검창닫기";
         this.cmsPageselclose.Click += new System.EventHandler(this.tsmiHumanhome_Click);
         // 
         // trDebug
         // 
         this.trDebug.Tick += new System.EventHandler(this.trDebug_Tick);
         // 
         // stsstrDebug
         // 
         this.stsstrDebug.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsstslbDebug});
         this.stsstrDebug.Location = new System.Drawing.Point(0, 641);
         this.stsstrDebug.Name = "stsstrDebug";
         this.stsstrDebug.Size = new System.Drawing.Size(1094, 22);
         this.stsstrDebug.TabIndex = 15;
         // 
         // tsstslbDebug
         // 
         this.tsstslbDebug.Name = "tsstslbDebug";
         this.tsstslbDebug.Size = new System.Drawing.Size(0, 17);
         // 
         // bgw
         // 
         this.bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_DoWork);
         // 
         // btnNoparsing
         // 
         this.btnNoparsing.Location = new System.Drawing.Point(409, 11);
         this.btnNoparsing.Name = "btnNoparsing";
         this.btnNoparsing.Size = new System.Drawing.Size(102, 23);
         this.btnNoparsing.TabIndex = 16;
         this.btnNoparsing.Text = "NoParsingCopy";
         this.btnNoparsing.UseVisualStyleBackColor = true;
         this.btnNoparsing.Click += new System.EventHandler(this.btnNoparsing_Click);
         // 
         // fswDown
         // 
         this.fswDown.EnableRaisingEvents = true;
         this.fswDown.SynchronizingObject = this;
         // 
         // btnWindowtempdel
         // 
         this.btnWindowtempdel.Location = new System.Drawing.Point(195, 40);
         this.btnWindowtempdel.Name = "btnWindowtempdel";
         this.btnWindowtempdel.Size = new System.Drawing.Size(102, 23);
         this.btnWindowtempdel.TabIndex = 17;
         this.btnWindowtempdel.Text = "WindowTempDel";
         this.btnWindowtempdel.UseVisualStyleBackColor = true;
         this.btnWindowtempdel.Click += new System.EventHandler(this.btnWindowtempdel_Click);
         // 
         // btnCallOfDuty
         // 
         this.btnCallOfDuty.Location = new System.Drawing.Point(304, 40);
         this.btnCallOfDuty.Name = "btnCallOfDuty";
         this.btnCallOfDuty.Size = new System.Drawing.Size(75, 23);
         this.btnCallOfDuty.TabIndex = 18;
         this.btnCallOfDuty.Text = "Duty";
         this.btnCallOfDuty.UseVisualStyleBackColor = true;
         this.btnCallOfDuty.Click += new System.EventHandler(this.btnCallOfDuty_Click);
         // 
         // frmCstoolmain
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1094, 663);
         this.Controls.Add(this.btnCallOfDuty);
         this.Controls.Add(this.btnWindowtempdel);
         this.Controls.Add(this.btnNoparsing);
         this.Controls.Add(this.stsstrDebug);
         this.Controls.Add(this.btnHomecheck);
         this.Controls.Add(this.btnNccdel);
         this.Controls.Add(this.btnGrpProgram);
         this.Controls.Add(this.btnNcclogin);
         this.Controls.Add(this.btnNccgmail);
         this.Controls.Add(this.pnThreepacs);
         this.Location = new System.Drawing.Point(255, 255);
         this.Name = "frmCstoolmain";
         this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
         this.pnThreepacs.ResumeLayout(false);
         this.pnThreepacs.PerformLayout();
         this.cmsPagesel.ResumeLayout(false);
         this.stsstrDebug.ResumeLayout(false);
         this.stsstrDebug.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.fswDown)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion
      private System.Windows.Forms.ToolTip ttbtnTip;
      private System.Windows.Forms.Button btnNccgmail;
      private System.Windows.Forms.Panel pnThreepacs;
      private System.Windows.Forms.Button btnThreeset;
      private System.Windows.Forms.Button btnNcclogin;
      private System.Windows.Forms.CheckBox cbOnpacs;
      private System.Windows.Forms.CheckBox cbCV3;
      private System.Windows.Forms.CheckBox cbZolvue;
      private System.Windows.Forms.CheckBox cbDtagent;
      private System.Windows.Forms.CheckBox cbDeamon;
      private System.Windows.Forms.Button btnGrpProgram;
      private System.Windows.Forms.CheckBox cbMdcagent;
      private System.Windows.Forms.Button btnNccdel;
      private System.Windows.Forms.Button btnHomecheck;
      private System.Windows.Forms.ContextMenuStrip cmsPagesel;
      private System.Windows.Forms.ToolStripMenuItem cmsQnA;
      private System.Windows.Forms.ToolStripMenuItem cmsWaitlist;
      private System.Windows.Forms.ToolStripMenuItem 계정정보복사ToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem Idtsmi;
      private System.Windows.Forms.ToolStripMenuItem Pwtsmi;
      private System.Windows.Forms.ToolStripMenuItem Mpwtsmi;
      private System.Windows.Forms.ToolStripMenuItem cmsMwaitlist;
      private System.Windows.Forms.ToolStripMenuItem cmsPageselclose;
      private System.Windows.Forms.ToolStripMenuItem Midtsmi;
      private System.Windows.Forms.Timer trDebug;
      private System.Windows.Forms.StatusStrip stsstrDebug;
      private System.Windows.Forms.ToolStripStatusLabel tsstslbDebug;
      private System.ComponentModel.BackgroundWorker bgw;
      private System.Windows.Forms.Button btnNoparsing;
      private System.IO.FileSystemWatcher fswDown;
      private System.Windows.Forms.Button btnWindowtempdel;
      private System.Windows.Forms.Button btnCallOfDuty;
   }
}

