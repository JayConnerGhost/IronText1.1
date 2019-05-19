using System.Windows.Forms;
using Unity;

namespace NotesPad
{
    public interface IMainController
    {
        Container Window { get; set; }
        UnityContainer DependencyContainer { get; set; }
        void Setup();
    }
}