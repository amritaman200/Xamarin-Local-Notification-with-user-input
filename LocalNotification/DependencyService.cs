using System;
namespace LocalNotification
{
    public interface IDependencyService
    {
        string SendNotification();
        string GetResponse();
    }
}
