using System;
using System.Collections.Generic;
using keepr_final.Models;
using keepr_final.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr_final.Controllers
{
    [Route("api/[controller]")]

    public class KeepsController : Controller
    {
        private readonly KeepRepository _repo;
        public KeepsController(KeepRepository repo)
        {
            _repo = repo;
        }

        // GENERIC GET ALL KEEPS 'FRONT PAGE'
        [HttpGet]
        public IEnumerable<Keep> GetAllKeeps()
        {
            return _repo.GetKeeps();
        }

        // GET BY USER ID
        [HttpGet("user/{userId}")]
        public IEnumerable<Keep> GetUserKeeps(string userId)
        {
            return _repo.GetAllUserKeeps(userId);
        }

        [HttpGet("{keepId}")]
        public Keep GetSingleKeep(string keepId)
        {
            return _repo.GetById(keepId);
        }

        // ADD KEEP TO REPORT
        [HttpPost("{boardId}")]
        public string AddToKeepsReport([FromBody] KeepsOnBoards ksb)
        {
            if(!ModelState.IsValid) { return "bad KeepsOnBoards Data"; }

            return _repo.AddToReport(ksb);
        }

        // POST KEEP FOR ALL TO SEE
        [HttpPost]
        public string AddKeep([FromBody] CreateKeepModel keep)
        {
            var user = HttpContext.User;
            var id = user.Identity.Name;
            keep.UserId = id;

            if (!ModelState.IsValid) { return "Not a valid keep"; }

            _repo.Add(keep);
            return "Success";
        }

        // UPDATE A KEEP
        [HttpPut("{id}")] // /keeps/id
        public string ChangeKeep(string id, [FromBody] Keep keep)
        {
            // if (useId != keep.UserId) { return "Not your keep to edit"; }

            if (!ModelState.IsValid) { return "Not Valid Keep"; }

            _repo.UpdateKeep(id, keep);
            return "Success Edit";
        }

        // DELETE A SINGLE KEEP
        [HttpDelete("{id}")]
        public string Delete(string id)
        {
            var user = HttpContext.User;
            var checkId = user.Identity.Name;

            return _repo.Delete(checkId, id);
        }
    }
}