using System.Text.Json;
using EFakturCallback.Data;
using EFakturCallback.Data.Repositories;
using StoredProcedureEFCore;

using EFakturCallback.Data.Repositories;
using EFakturCallback.Data.Repositories.Interfaces;
using EFakturCallback.Data.Entities;
public class EFakturListenerRepository : RepositoryBase, IEfakturListenerRepository
{
    public EFakturListenerRepository(ApiContext context) : base(context)
    {
    }

    public async Task EFakturListener(FakturDetailStream message)
    {
        await Db.LoadStoredProc("sp_ConsRedpanda_Efaktur")
            .AddParam("jsonFormat", JsonSerializer.Serialize(message)).ExecNonQueryAsync();   
    }
}
