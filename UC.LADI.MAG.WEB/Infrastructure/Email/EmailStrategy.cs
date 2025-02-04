namespace UC.LADI.MAG.WEB.Infrastructure.Email
{
    public abstract class EmailStrategy
    {
        public abstract void SetSender(string fromMail, string password);
        public abstract void Send(string toEmail, string subject, string htmlContent, ref string errorMessage);
    }
}
