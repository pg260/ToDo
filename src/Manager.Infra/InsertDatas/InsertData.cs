using Manager.Domain.Entities;
using Manager.Infra.Context;

namespace Manager.Infra.InsertDatas;

public class InsertData
{
    private static void InsertDat()
    {
        using (var context = new ManagerContext())
        {
            var user = new User
            (
                new Guid(),
                "pg",
                "Pg@eu.com",
                "1234"
            );
            context.Users.Add(user);
            
            var task = new Task
            (
                
            )
        }
    }
}