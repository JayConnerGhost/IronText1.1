using Unity;

namespace NotesPad
{
    internal interface IContainer
    {
        UnityContainer DependencyContainer { get; set; }
    }
}