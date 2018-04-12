using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr_final.Models;
using Microsoft.AspNetCore.Http;

namespace keepr_final.Repositories
{
    public class BoardRepository // JOB IS TO ACCESS SQL DATABASE. THINK AS SERVICE.
    {
        private readonly IDbConnection _db;

        public BoardRepository(IDbConnection db)
        {
            _db = db;
        }

        // CREATE BOARD
        public Board Add(CreateBoardModel boardData)
        {
            Guid g;
            g = Guid.NewGuid();
            string id = g.ToString();

            Board board = new Board()
            {
                Id = id,
                Name = boardData.Name,
                UserId = boardData.UserId
            };

            var boardSuccess =  _db.Execute(@"
            INSERT INTO boards (
            id,
            name,
            userId
            ) VALUES(
                @Id,
                @Name,
                @UserId
            )", board);

            if( boardSuccess < 1)
            {
                throw new Exception("Bad Board Data");
            }
            return board;
        }

        // GET BOARD BY USERID
        public IEnumerable<Board> GetBoardByUserId(string userId)
        {
            return _db.Query<Board>(@"
            SELECT * FROM boards
            WHERE (userId = @id)", new {Id = userId});
        }

        internal string DeleteFromBoard(string boardId, string keepId)
        {
            if(GetById(boardId) != null)
            {
                var deleteKeep = _db.Execute(@"
                DELETE FROM keepsreport
                WHERE keepId = @keepId AND boardId = @boardId;
                ", new {keepId = keepId, boardId = boardId});

                return "Removed from board";
            }
            else
            {
                return "Bad Board Id";
            }
        }

        // GET BOARD BY ID. A HELPER METHOD
        private Board GetById(string id)
        {
            return _db.QueryFirstOrDefault<Board>(@"SELECT * FROM boards WHERE Id = @id", new Board{Id = id});
        }

        // GETS KEEPS BY BOARD ID AND RETURNS A RETURN KEEP
        internal IEnumerable<ReturnKeep> GetKeepsByBoards(string boardId)
        {
            return _db.Query<ReturnKeep>(@"
            SELECT
            k.name,
            k.description,
            k.mediaUrl,
            k.addedToBoard,
            k.keepViews,
            k.id,
            k.userId

            FROM keepsreport kr
            JOIN keeps k ON k.id = kr.keepId
            JOIN boards b ON b.id = kr.boardId
            WHERE boardId = @boardId", new {boardId = boardId});       
        }
    }
}