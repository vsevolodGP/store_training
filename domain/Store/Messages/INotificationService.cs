namespace Store.Messages
{
    public interface INotificationService
    {
        public void SendConfirmationCode(string cellPhone, int code);
        void StartProcess(Order order);
    }
}
