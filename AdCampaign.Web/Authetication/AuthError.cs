using AdCampaign.Common;

namespace AdCampaign.Authetication
{
    public record AuthError(string Message, string Code) : Error(Message, Code)
    {
        public static AuthError InvalidCredentials() => new ("Неверные данные для входа", "400");

        public static AuthError UserIsBlocked(string blockerName) => new ($"Вы заблокированы пользователем {blockerName}", "400");
    }
}