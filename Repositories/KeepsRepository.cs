
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace keepr.Repositories
{

  public class KeepsRepository
  {
    private readonly IDbConnection _db;
    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    // pass string userID
    //OR userId = @userId
    //, new { userId }
    public IEnumerable<Keep> GetAll(string userId)
    {
      return _db.Query<Keep>("SELECT * From keeps WHERE isPrivate = 0", new { userId });
    }

    public IEnumerable<Keep> GetByUserId(string userId)
    {
      return _db.Query<Keep>("SELECT * FROM keeps WHERE userId = @userId", new { userId });
    }

    public Keep CreateKeep(Keep keep)
    {
      try
      {
        int id = _db.ExecuteScalar<int>(@"INSERT INTO keeps (name, description, userId, img, isPrivate, views, shares, keeps)
        VALUES (@Name, @Description, @UserId, @Img, @IsPrivate, @Views, @Shares, @Keeps);
        SELECT LAST_INSERT_ID();
        ", keep);
        keep.Id = id;
        return keep;
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return null;
      }
    }

    public bool Delete(int id, string userId)
    {
      int success = _db.Execute("DELETE FROM keeps WHERE id = @id AND userId = @userId", new { id, userId });
      return success > 0;
    }

    public Keep EditKeep(int id, Keep editedKeep)
    {
      try
      {
        string query = @"UPDATE keeps SET
          name = @Name,
          description = @Description,
          userId = @UserId,
          img = @Img,
          isprivate = @IsPrivate,
          views = @Views,
          shares = @Shares,
          keeps = @Keeps,
          WHERE id = @Id;
          SELECT * FROM keeps WHERE id = @Id;
          ";
        return _db.QueryFirstOrDefault<Keep>(query, editedKeep);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return null;
      }
    }
  }
}
