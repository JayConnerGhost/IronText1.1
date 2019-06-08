using System;
using System.Windows.Forms;

namespace NotesPad
{
    public class EditIdeaDialog : Form
    {
        public Tuple<string, string> ShowDialog(string currentDescription, string currentName)
        {
            TextBox name = new TextBox() { Width = 460, Height = 20, TabIndex = 0, TabStop = true, Multiline = false, Text=currentName };
            TextBox description = new TextBox() { Width = 460, Height = 350, TabIndex = 0, TabStop = true, Multiline = true, Text=currentDescription };

            using (var form = new DialogForm(new FormInfo("Edit Idea", 485, 500)))
            {
                Label nameLabel = new Label() { Top = 4, Left = 4, Height = 15, Text = "Name" };
                name.Top = 20;
                name.Left = 4;
                description.Top = 68;
                description.Left = 4;

                Label descriptionLabel = new Label() { Top = 44, Left = 4, Text = "Description" };
                Button confirmation = new Button() { Text = "Save", TabIndex = 1, TabStop = true };
                Button close = new Button() { Text = "close", TabIndex = 1, TabStop = true };
                confirmation.Click += (sender, e) => { form.Close(); };
                close.Click += (sender, e) => { form.Close(); };


                form.Controls.Add(nameLabel);
                form.Controls.Add(name);
                form.Controls.Add(descriptionLabel);
                form.Controls.Add(description);
                var buttonLayoutPanel = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.RightToLeft,
                    Top = 420,
                    Left = 4,
                    Width = 460
                };
                buttonLayoutPanel.Controls.Add(close);
                buttonLayoutPanel.Controls.Add(confirmation);
                form.Controls.Add(buttonLayoutPanel);
                //TODO build form
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ControlBox = false;
                form.ShowDialog();
            }

            return new Tuple<string, string>(name.Text, description.Text);
        }
    }
}