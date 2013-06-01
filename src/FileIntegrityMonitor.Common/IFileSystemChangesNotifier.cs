using System.ServiceModel;

namespace FileIntegrityMonitor.Common
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IFileSystemChangesNotifierCallback))]
    public interface IFileSystemChangesNotifier
    {
        [OperationContract(IsInitiating = true)]
        void Subscribe();

        [OperationContract]
        void Ping();

        [OperationContract(IsTerminating = true)]
        void Unsubscribe();
    }
}