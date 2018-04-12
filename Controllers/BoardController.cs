using System.Collections.Generic;
using keepr_final.Models;
using keepr_final.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr_final.Controllers
{
    [Route("api/[controller]")] // API/BOARDS/USERID

    public class BoardsController : Controller
    {
        private readonly BoardRepository _repo;
        public BoardsController(BoardRepository repo)
        {
            _repo = repo;
        } 

        // GET BOARDS BY USERID
        [HttpGet("{userId}")]
        public IEnumerable<Board> GetBoardByUser(string userId)
        {
            return _repo.GetBoardByUserId(userId);
        }

        // ADD BOARD
        [HttpPost]
        public string AddBoard([FromBody] CreateBoardModel board)
        { 
            var user = HttpContext.User;
            var id = user.Identity.Name;
            board.UserId = id;

            if (!ModelState.IsValid) { return "Not a valid board"; }

            _repo.Add(board);
            return "Success";
        }

        // GET KEEPS BY ITS BOARD
        [HttpGet("{boardId}/keeps")]
        public IEnumerable<ReturnKeep> GetKeepById(string boardId)
        {
            return _repo.GetKeepsByBoards(boardId);
        }

        // REMOVE KEEP FROM BOARD
        [HttpDelete("{boardId}/keeps/{keepId}")]
        public string DeleteKeepFromBoard(string boardId, string keepId)
        {
            return _repo.DeleteFromBoard(boardId, keepId);
        }
    }
}