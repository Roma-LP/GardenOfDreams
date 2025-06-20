using _GardenOfDreams.Scripts.Interfaces;

namespace _GardenOfDreams.Scripts.SaveTools
{
    public abstract class ProgressData<T> : IProgressData where T : class, new()
    {
        public abstract T GetProgressModel();
        public abstract void SetProgressModel(T state);

        object IProgressData.GetProgressModel() => GetProgressModel();
        void IProgressData.SetProgressModel(object model) => SetProgressModel(model as T);
    
        public System.Type DataType => typeof(T);
    }
}