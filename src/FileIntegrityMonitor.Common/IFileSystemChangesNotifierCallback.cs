using System.ServiceModel;

namespace FileIntegrityMonitor.Common
{
    [ServiceContract]
    public interface IFileSystemChangesNotifierCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnFileChangeEvent(FileEventArgs e);
    }
}