using System;
using System.Collections.Generic;
using keepr.Repositories;
using Keepr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsRepository _kr;
    public KeepsController(KeepsRepository kr)
    {
      _kr = kr;
    }

    //getall
    [HttpGet]

    public ActionResult<IEnumerable<Keep>> Get()
    {
      string userId = HttpContext.User.Identity.Name;
      IEnumerable<Keep> results = _kr.GetAll(userId);
      if (results == null)
      {
        return BadRequest();
      }
      return Ok(results);
    }

    //get keeps by user
    [Authorize]
    [HttpGet("user")]
    public ActionResult<IEnumerable<Keep>> GetByUserId()
    {
      string userId = HttpContext.User.Identity.Name;
      IEnumerable<Keep> found = _kr.GetByUserId(userId);
      if (found == null) { return BadRequest(); }
      return Ok(found);
    }

    // getby id
    // [Authorize]
    // [HttpGet("/")]
    // public ActionResult<Keep> GetByUserId()
    // {
    //   string userId = HttpContext.User.Identity.Name;
    //   Keep found = _kr.GetByUserId(userId);
    //   if (found == null) { return BadRequest(); }
    //   return Ok(found);
    // }

    //update keep   keeps/:keepId
    [HttpPut("{id}")]
    public ActionResult<Keep> Edit(int id, [FromBody] Keep editedKeep)
    {
      Keep updatedKeep = _kr.EditKeep(id, editedKeep);
      if (updatedKeep == null) { return BadRequest("Keep not updated"); }
      return Ok(updatedKeep);
    }


    //create
    [Authorize]
    [HttpPost]
    public ActionResult<Keep> Create([FromBody] Keep keep)
    {
      keep.UserId = HttpContext.User.Identity.Name;
      Keep newKeep = _kr.CreateKeep(keep);
      if (newKeep == null) { return BadRequest("Keep was not created"); }
      return Ok(newKeep);
    }

    //Delete
    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<string> Delete(int id)
    {
      bool successful = _kr.Delete(id, HttpContext.User.Identity.Name);
      if (!successful) { return BadRequest(); }
      return Ok();
    }






  }
}