namespace _GardenOfDreams.Scripts.Interfaces
{
    public interface IProgressData
    {
        System.Type DataType { get; }
        object GetProgressModel();
        void SetProgressModel(object model);
    }
}