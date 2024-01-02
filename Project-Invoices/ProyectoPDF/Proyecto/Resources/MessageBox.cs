using System;
using System.Windows.Forms;
using System.Drawing;

namespace Proyecto.Resources
{
    internal class MessageBoxHelper
    {
        public static DialogResult ShowCustomMessageBox(string message, string caption)
        {
            // Crea un MessageBox personalizado
            CustomMessageBox customMessageBox = new CustomMessageBox(message, caption);

            // Muestra el MessageBox y devuelve el resultado personalizado
            return customMessageBox.ShowDialog();
        }

        private class CustomMessageBox : Form
        {
            private DialogResult result = DialogResult.Cancel;

            public CustomMessageBox(string message, string caption)
            {
                InitializeComponent();

                lblMessage.Text = message;
                Text = caption;
            }

            private void btnUpdate_Click(object sender, EventArgs e)
            {
                result = DialogResult.Yes;
                Close();
            }

            private void btnDelete_Click(object sender, EventArgs e)
            {
                result = DialogResult.No;
                Close();
            }

            private void btnCancel_Click(object sender, EventArgs e)
            {
                result = DialogResult.Cancel;
                Close();
            }

            public DialogResult ShowDialog()
            {
                base.ShowDialog();
                return result;
            }

            private void InitializeComponent()
            {
                // Diseño de la ventana, puedes personalizar según tus necesidades

                lblMessage = new Label();
                btnUpdate = new Button();
                btnDelete = new Button();
                btnCancel = new Button();

                SuspendLayout();

                lblMessage.Dock = DockStyle.Top;
                lblMessage.TextAlign = ContentAlignment.MiddleCenter;
                lblMessage.Padding = new Padding(10);
                lblMessage.AutoSize = true;

                btnUpdate.Dock = DockStyle.Left;
                btnUpdate.Text = "Update";
                btnUpdate.Click += btnUpdate_Click;

                btnDelete.Dock = DockStyle.Left;
                btnDelete.Text = "Delete";
                btnDelete.Click += btnDelete_Click;

                btnCancel.Dock = DockStyle.Left;
                btnCancel.Text = "Cancel";
                btnCancel.Click += btnCancel_Click;

                Controls.Add(lblMessage);
                Controls.Add(btnUpdate);
                Controls.Add(btnDelete);
                Controls.Add(btnCancel);

                AutoSize = true;
                AutoSizeMode = AutoSizeMode.GrowAndShrink;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                MinimizeBox = false;
                ShowIcon = false;
                ShowInTaskbar = false;
                StartPosition = FormStartPosition.CenterParent;

                ResumeLayout(false);
                PerformLayout();
            }

            private Label lblMessage;
            private Button btnUpdate;
            private Button btnDelete;
            private Button btnCancel;
        }
    }
}
