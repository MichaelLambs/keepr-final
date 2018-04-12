using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr_final.Models;
using Microsoft.AspNetCore.Http;

namespace keepr_final.Repositories
{
    public class KeepRepository // JOB IS TO ACCESS SQL DATABASE. THINK AS SERVICE.
    {
        private readonly IDbConnection _db;

        public KeepRepository(IDbConnection db)
        {
            _db = db;
        }

        // CREATE KEEP
        public Keep Add(CreateKeepModel keepData)
        {
            Guid g;
            g = Guid.NewGuid();
            string id = g.ToString();

            int views = 0;
            int added = 0;

            Keep keep = new Keep()
            {
                Id = id,
                Name = keepData.Name,
                Description = keepData.Description,
                MediaUrl = keepData.MediaUrl,
                UserId = keepData.UserId,
                AddedToBoard = added,
                KeepViews = views,
                KeepPublic = 0,
            };

            var newKeepSuccess =  _db.Execute(@"
            INSERT INTO keeps (
            id,
            name,
            description,
            mediaUrl,
            userId,
            addedToBoard,
            keepViews,
            keepPublic
            ) VALUES(
                @Id,
                @Name,
                @Description,
                @MediaUrl,
                @UserId,
                @AddedToBoard,
                @KeepViews,
                @KeepPublic
            )", keep);

            if( newKeepSuccess < 1)
            {
                throw new Exception("Bad Keep Data");
            }
            return keep;
        }

        //ADD TO KEEPS REPORT
        public string AddToReport(KeepsOnBoards kbs)
        {
            Guid gu;
            gu = Guid.NewGuid();
            string id2 = gu.ToString();

            KeepsOnBoards keepReport = new KeepsOnBoards()
            {
                Id = id2,
                BoardId = kbs.BoardId,
                KeepId = kbs.KeepId,
                UserId = kbs.UserId
            };

            var addedToReport =  _db.Execute(@"
            INSERT INTO keepsreport (
            id,
            boardId,
            keepId,
            userId
            ) VALUES(
                @Id,
                @BoardId,
                @KeepId,
                @UserId
            )", keepReport);

            if( addedToReport < 1)
            {
                throw new Exception("Didn't Work");
            }
            return "Success";
        }

        // GET KEEP BY ID
        public Keep GetById(string id)
        {
            return _db.QueryFirstOrDefault<Keep>(@"SELECT * FROM keeps WHERE Id = @id", new Keep{Id = id});
        }

        // GET ALL KEEPS BY USER ID
        public IEnumerable<Keep> GetAllUserKeeps(string userId)
        {
            return _db.Query<Keep>(@"
            SELECT * FROM keeps WHERE UserId = @userId", new Keep{UserId = userId});
        }

        // GET ALL KEEPS
        public IEnumerable<Keep> GetKeeps()
        {
            return _db.Query<Keep>("SELECT * FROM keeps WHERE keepPublic = 1");
        }

        // UPDATE A KEEP
        public Keep UpdateKeep(string id, Keep keep)
        {
            if (GetById(id) != null)
            {
                keep.Id = id;
                _db.Execute(@"
                UPDATE keeps SET
                name = @Name,
                description = @Description,
                mediaUrl = @MediaUrl,
                userId = @UserId,
                addedToBoard = @AddedToBoard,
                keepViews = @KeepViews,
                keepPublic = @KeepPublic
                WHERE id = @Id", keep);

                var editKeep = _db.QueryFirstOrDefault<Keep>(@"
                SELECT * FROM keeps WHERE Id = @id", new Keep{Id = id});
                return editKeep;
            }
            else
            {
                return null;
            }
        }

        public string Delete(string checkId, string id)
        {
            var check = GetById(id);
            if (check != null && check.UserId == checkId && check.KeepPublic != 1)
            {
                _db.Execute(@"
                DELETE FROM keeps WHERE Id = @id", new { Id = id});

                return "Delete Success";
            }
            else
            {
                return "Can't Delete";
            }
        }

    }
}