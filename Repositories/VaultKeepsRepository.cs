
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }



    public IEnumerable<Keep> GetByVaultId(int Id)
    {
      return _db.Query<Keep>(@"SELECT * FROM vaultkeeps vk
 INNER JOIN keeps k ON k.id = vk.keepId
 WHERE(vaultId = @Id)", new { Id });
    }

    public Vaultkeep CreateVaultkeep(Vaultkeep vaultkeep)
    {
      try
      {
        int id = _db.ExecuteScalar<int>(@"
                INSERT INTO vaultkeeps (vaultId, keepId, userId)
                    VALUES (@VaultId, @KeepId, @UserId);
                    SELECT LAST_INSERT_ID();
                ", vaultkeep);
        vaultkeep.Id = id;
        return vaultkeep;
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return null;
      }
    }



    public bool Delete(int id, string userId)
    {
      int success = _db.Execute("DELETE FROM vaultkeeps WHERE id = @id and userId = @userId", new { id, userId });
      return success > 0;
    }
  }
}