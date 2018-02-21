namespace Filtres
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.фильтрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.точечныйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.инверсияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.чбToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сепияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.яркостьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.метричныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.размытиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.гауссаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.резкостьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.градиетToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поОсиYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поОсиXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выделениеГраницToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.операторЩарраToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поОсиYToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.поОсиXToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.операторПрюиттаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поОсиYToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.поОсиXToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.перемещениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.переносToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.другиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.серыйМирToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.морфологияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.расширениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сужениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.закрытиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(572, 384);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.сохранитьToolStripMenuItem});
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.aToolStripMenuItem.Text = "Файл";
            this.aToolStripMenuItem.Click += new System.EventHandler(this.aToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.openToolStripMenuItem.Text = "Открыть";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // фильтрыToolStripMenuItem
            // 
            this.фильтрыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.точечныйToolStripMenuItem,
            this.метричныеToolStripMenuItem,
            this.перемещениеToolStripMenuItem,
            this.другиеToolStripMenuItem});
            this.фильтрыToolStripMenuItem.Name = "фильтрыToolStripMenuItem";
            this.фильтрыToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.фильтрыToolStripMenuItem.Text = "Фильтры";
            // 
            // точечныйToolStripMenuItem
            // 
            this.точечныйToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.инверсияToolStripMenuItem,
            this.чбToolStripMenuItem,
            this.сепияToolStripMenuItem,
            this.яркостьToolStripMenuItem});
            this.точечныйToolStripMenuItem.Name = "точечныйToolStripMenuItem";
            this.точечныйToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.точечныйToolStripMenuItem.Text = "Точечный";
            // 
            // инверсияToolStripMenuItem
            // 
            this.инверсияToolStripMenuItem.Name = "инверсияToolStripMenuItem";
            this.инверсияToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.инверсияToolStripMenuItem.Text = "Инверсия";
            this.инверсияToolStripMenuItem.Click += new System.EventHandler(this.инверсияToolStripMenuItem_Click);
            // 
            // чбToolStripMenuItem
            // 
            this.чбToolStripMenuItem.Name = "чбToolStripMenuItem";
            this.чбToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.чбToolStripMenuItem.Text = "Ч/б";
            this.чбToolStripMenuItem.Click += new System.EventHandler(this.чбToolStripMenuItem_Click);
            // 
            // сепияToolStripMenuItem
            // 
            this.сепияToolStripMenuItem.Name = "сепияToolStripMenuItem";
            this.сепияToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.сепияToolStripMenuItem.Text = "Сепия";
            this.сепияToolStripMenuItem.Click += new System.EventHandler(this.сепияToolStripMenuItem_Click);
            // 
            // яркостьToolStripMenuItem
            // 
            this.яркостьToolStripMenuItem.Name = "яркостьToolStripMenuItem";
            this.яркостьToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.яркостьToolStripMenuItem.Text = "Яркость";
            this.яркостьToolStripMenuItem.Click += new System.EventHandler(this.яркостьToolStripMenuItem_Click);
            // 
            // метричныеToolStripMenuItem
            // 
            this.метричныеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.размытиеToolStripMenuItem,
            this.резкостьToolStripMenuItem,
            this.градиетToolStripMenuItem,
            this.выделениеГраницToolStripMenuItem});
            this.метричныеToolStripMenuItem.Name = "метричныеToolStripMenuItem";
            this.метричныеToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.метричныеToolStripMenuItem.Text = "Матричные";
            // 
            // размытиеToolStripMenuItem
            // 
            this.размытиеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.гауссаToolStripMenuItem});
            this.размытиеToolStripMenuItem.Name = "размытиеToolStripMenuItem";
            this.размытиеToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.размытиеToolStripMenuItem.Text = "Размытие";
            this.размытиеToolStripMenuItem.Click += new System.EventHandler(this.размытиеToolStripMenuItem_Click);
            // 
            // гауссаToolStripMenuItem
            // 
            this.гауссаToolStripMenuItem.Name = "гауссаToolStripMenuItem";
            this.гауссаToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.гауссаToolStripMenuItem.Text = "Гаусса";
            this.гауссаToolStripMenuItem.Click += new System.EventHandler(this.гауссаToolStripMenuItem_Click);
            // 
            // резкостьToolStripMenuItem
            // 
            this.резкостьToolStripMenuItem.Name = "резкостьToolStripMenuItem";
            this.резкостьToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.резкостьToolStripMenuItem.Text = "Резкость";
            this.резкостьToolStripMenuItem.Click += new System.EventHandler(this.резкостьToolStripMenuItem_Click);
            // 
            // градиетToolStripMenuItem
            // 
            this.градиетToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.поОсиYToolStripMenuItem,
            this.поОсиXToolStripMenuItem});
            this.градиетToolStripMenuItem.Name = "градиетToolStripMenuItem";
            this.градиетToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.градиетToolStripMenuItem.Text = "Градиет";
            // 
            // поОсиYToolStripMenuItem
            // 
            this.поОсиYToolStripMenuItem.Name = "поОсиYToolStripMenuItem";
            this.поОсиYToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.поОсиYToolStripMenuItem.Text = "По оси Y";
            this.поОсиYToolStripMenuItem.Click += new System.EventHandler(this.поОсиYToolStripMenuItem_Click);
            // 
            // поОсиXToolStripMenuItem
            // 
            this.поОсиXToolStripMenuItem.Name = "поОсиXToolStripMenuItem";
            this.поОсиXToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.поОсиXToolStripMenuItem.Text = "По оси X";
            this.поОсиXToolStripMenuItem.Click += new System.EventHandler(this.поОсиXToolStripMenuItem_Click);
            // 
            // выделениеГраницToolStripMenuItem
            // 
            this.выделениеГраницToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.операторЩарраToolStripMenuItem,
            this.операторПрюиттаToolStripMenuItem});
            this.выделениеГраницToolStripMenuItem.Name = "выделениеГраницToolStripMenuItem";
            this.выделениеГраницToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.выделениеГраницToolStripMenuItem.Text = "Выделение границ";
            // 
            // операторЩарраToolStripMenuItem
            // 
            this.операторЩарраToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.поОсиYToolStripMenuItem1,
            this.поОсиXToolStripMenuItem1});
            this.операторЩарраToolStripMenuItem.Name = "операторЩарраToolStripMenuItem";
            this.операторЩарраToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.операторЩарраToolStripMenuItem.Text = "Оператор Щарра";
            // 
            // поОсиYToolStripMenuItem1
            // 
            this.поОсиYToolStripMenuItem1.Name = "поОсиYToolStripMenuItem1";
            this.поОсиYToolStripMenuItem1.Size = new System.Drawing.Size(123, 22);
            this.поОсиYToolStripMenuItem1.Text = "По оси Y";
            this.поОсиYToolStripMenuItem1.Click += new System.EventHandler(this.поОсиYToolStripMenuItem1_Click);
            // 
            // поОсиXToolStripMenuItem1
            // 
            this.поОсиXToolStripMenuItem1.Name = "поОсиXToolStripMenuItem1";
            this.поОсиXToolStripMenuItem1.Size = new System.Drawing.Size(123, 22);
            this.поОсиXToolStripMenuItem1.Text = "По оси X";
            this.поОсиXToolStripMenuItem1.Click += new System.EventHandler(this.поОсиXToolStripMenuItem1_Click);
            // 
            // операторПрюиттаToolStripMenuItem
            // 
            this.операторПрюиттаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.поОсиYToolStripMenuItem2,
            this.поОсиXToolStripMenuItem2});
            this.операторПрюиттаToolStripMenuItem.Name = "операторПрюиттаToolStripMenuItem";
            this.операторПрюиттаToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.операторПрюиттаToolStripMenuItem.Text = "Оператор Прюитта";
            // 
            // поОсиYToolStripMenuItem2
            // 
            this.поОсиYToolStripMenuItem2.Name = "поОсиYToolStripMenuItem2";
            this.поОсиYToolStripMenuItem2.Size = new System.Drawing.Size(123, 22);
            this.поОсиYToolStripMenuItem2.Text = "По оси Y";
            this.поОсиYToolStripMenuItem2.Click += new System.EventHandler(this.поОсиYToolStripMenuItem2_Click);
            // 
            // поОсиXToolStripMenuItem2
            // 
            this.поОсиXToolStripMenuItem2.Name = "поОсиXToolStripMenuItem2";
            this.поОсиXToolStripMenuItem2.Size = new System.Drawing.Size(123, 22);
            this.поОсиXToolStripMenuItem2.Text = "По оси X";
            this.поОсиXToolStripMenuItem2.Click += new System.EventHandler(this.поОсиXToolStripMenuItem2_Click);
            // 
            // перемещениеToolStripMenuItem
            // 
            this.перемещениеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.переносToolStripMenuItem});
            this.перемещениеToolStripMenuItem.Name = "перемещениеToolStripMenuItem";
            this.перемещениеToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.перемещениеToolStripMenuItem.Text = "Перемещение";
            // 
            // переносToolStripMenuItem
            // 
            this.переносToolStripMenuItem.Name = "переносToolStripMenuItem";
            this.переносToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.переносToolStripMenuItem.Text = "Перенос";
            this.переносToolStripMenuItem.Click += new System.EventHandler(this.переносToolStripMenuItem_Click);
            // 
            // другиеToolStripMenuItem
            // 
            this.другиеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.серыйМирToolStripMenuItem});
            this.другиеToolStripMenuItem.Name = "другиеToolStripMenuItem";
            this.другиеToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.другиеToolStripMenuItem.Text = "Другие";
            // 
            // серыйМирToolStripMenuItem
            // 
            this.серыйМирToolStripMenuItem.Name = "серыйМирToolStripMenuItem";
            this.серыйМирToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.серыйМирToolStripMenuItem.Text = "Серый мир";
            this.серыйМирToolStripMenuItem.Click += new System.EventHandler(this.серыйМирToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aToolStripMenuItem,
            this.фильтрыToolStripMenuItem,
            this.морфологияToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(594, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(9, 440);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(489, 19);
            this.progressBar1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(503, 440);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 19);
            this.button1.TabIndex = 3;
            this.button1.Text = "Отмена";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // морфологияToolStripMenuItem
            // 
            this.морфологияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.расширениеToolStripMenuItem,
            this.сужениеToolStripMenuItem,
            this.открытиеToolStripMenuItem,
            this.закрытиеToolStripMenuItem});
            this.морфологияToolStripMenuItem.Name = "морфологияToolStripMenuItem";
            this.морфологияToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.морфологияToolStripMenuItem.Text = "Морфология";
            // 
            // расширениеToolStripMenuItem
            // 
            this.расширениеToolStripMenuItem.Name = "расширениеToolStripMenuItem";
            this.расширениеToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.расширениеToolStripMenuItem.Text = "Расширение";
            this.расширениеToolStripMenuItem.Click += new System.EventHandler(this.расширениеToolStripMenuItem_Click);
            // 
            // сужениеToolStripMenuItem
            // 
            this.сужениеToolStripMenuItem.Name = "сужениеToolStripMenuItem";
            this.сужениеToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.сужениеToolStripMenuItem.Text = "Сужение";
            // 
            // открытиеToolStripMenuItem
            // 
            this.открытиеToolStripMenuItem.Name = "открытиеToolStripMenuItem";
            this.открытиеToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.открытиеToolStripMenuItem.Text = "Открытие";
            // 
            // закрытиеToolStripMenuItem
            // 
            this.закрытиеToolStripMenuItem.Name = "закрытиеToolStripMenuItem";
            this.закрытиеToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.закрытиеToolStripMenuItem.Text = "Закрытие";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 469);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem фильтрыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem точечныйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem инверсияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem метричныеToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem размытиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem гауссаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem чбToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сепияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem яркостьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem резкостьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem градиетToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поОсиYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поОсиXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выделениеГраницToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem операторЩарраToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поОсиYToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem поОсиXToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem операторПрюиттаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поОсиYToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem поОсиXToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem перемещениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem переносToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem другиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem серыйМирToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem морфологияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem расширениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сужениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem закрытиеToolStripMenuItem;
    }
}

