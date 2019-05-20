using System;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Forms;
using NetSpell.SpellChecker;
using WeifenLuo.WinFormsUI.Docking;

namespace NotesPad
{
    internal class EditorController : IEditorController
    {
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
            //Work to do here to enable netspell
            SpellChecker.Text = GetSelectedTextControl().Text;
            SpellChecker.SpellCheck();
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