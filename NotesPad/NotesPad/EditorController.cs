﻿using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using NetSpell.SpellChecker;
using WeifenLuo.WinFormsUI.Docking;

namespace NotesPad
{
    internal class EditorController : IEditorController
    {
        //TODO: cut, copy, paste, select all
        private Form _window;
        internal Spelling SpellChecker;
        public Form Window
        {
            get => _window;
            set => _window = value;
        }

        public DockPanel DockingArea { get; set; }

        public void Setup()
        {
            BuildRTFControl();
            AddSpellingSupport();
        }

        private void BuildRTFControl()
        {
            var richTextBox = new RichTextBox {Dock = DockStyle.Fill};
            Window.Controls.Add(richTextBox);
        }

        public void Show()
        {
            //HACK till i have a better way of dealing with this 
            if (this.Window.IsDisposed)
            {
                this.Window = new Editor(this);
            }

            ((DockContent)this._window).Show(DockingArea, DockState.Document);
        }

        public void SpellCheck()
        {
            SpellChecker.Text = GetSelectedTextControl().Text;
            SpellChecker.SpellCheck();
        }

        public void Save()
        {
            var text = ((RichTextBox)((Editor)Window).ActiveControl);
            SaveFileDialog fileNameDialog = new SaveFileDialog { AddExtension = true, DefaultExt = ".rtf", Filter = "rtf files (*.rtf)|*.rtf|All files (*.*)|*.*" };
            var result = fileNameDialog.ShowDialog();
            if (result == DialogResult.Cancel || result == DialogResult.Cancel)
            {
                return;
            }

            var path = fileNameDialog.FileName;
            text.SaveFile(path);
            var name = (new FileInfo(path)).Name;

            ((Form)Window).Text = name;
        }

        public void OpenFile(string path, string fileName)
        {
            if (path == null) return;
            
            ((Form)Window).Text = fileName;
            var target = (RichTextBox)Window.Controls[0];

            try
            {
                target.LoadFile(path, RichTextBoxStreamType.RichText);
            }
            catch (Exception e)
            {
                target.LoadFile(path, RichTextBoxStreamType.PlainText);
            }
        }

        public void SelectAllText()
        {
            var richText=(RichTextBox) Window.Controls[0];
            if (string.IsNullOrEmpty(richText.Text))
            {
                return;
            }

            richText.SelectAll();
        }

        public void Cut()
        {

            var richText = (RichTextBox)Window.Controls[0];
            if (string.IsNullOrEmpty(richText.SelectedText))
            {
                return;
            }
            richText.Cut();
        }

        public void Copy()
        {
            var richText = (RichTextBox)Window.Controls[0];
            if (string.IsNullOrEmpty(richText.SelectedText))
            {
                return;
            }
            richText.Copy();
        }

        public void Paste()
        {
            var richText = (RichTextBox)Window.Controls[0];
            richText.Paste();
        }

        private void AddSpellingSupport()
        {
            //http://www.loresoft.com/The-NetSpell-project
            SpellChecker = new Spelling { ShowDialog = true };
            SpellChecker.MisspelledWord += new Spelling.MisspelledWordEventHandler(SpellChecker_MisspelledWord);
            SpellChecker.EndOfText += new Spelling.EndOfTextEventHandler(SpellChecker_EndOfText);
            SpellChecker.DoubledWord += new Spelling.DoubledWordEventHandler(SpellChecker_DoubledWord);
       
        }

        private void SpellChecker_DoubledWord(object sender, SpellingEventArgs args)
        {
            // update text  
            GetSelectedTextControl().Text = SpellChecker.Text;
        }

        private RichTextBox GetSelectedTextControl()
        {
            var control = _window.Controls[0];
            return (RichTextBox)control;
        }

        private void SpellChecker_EndOfText(object sender, EventArgs args)
        {
            // update text  
            GetSelectedTextControl().Text = SpellChecker.Text;
        }

        private void SpellChecker_MisspelledWord(object sender, SpellingEventArgs args)
        {
            // update text  
            GetSelectedTextControl().Text = SpellChecker.Text;
        }
    }
}