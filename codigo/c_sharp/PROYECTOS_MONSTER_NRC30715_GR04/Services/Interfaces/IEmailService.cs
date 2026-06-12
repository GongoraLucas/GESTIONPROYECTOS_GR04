namespace PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

public interface IEmailService
{
    Task SendAsync(string to, string subject, string htmlBody);
}
