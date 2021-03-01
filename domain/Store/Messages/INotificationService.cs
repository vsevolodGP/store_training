using System.Threading.Tasks;

namespace Store.Messages
{
    public interface INotificationService
    {
        public void SendConfirmationCode(string cellPhone, int code);

        Task SendConfirmationCodeAsync(string cellPhone, int code);

        void StartProcess(Order order);

        Task StartProcessAsync(Order order);
    }
}
