using ProMgr.Models;
using ProMgr.Services;
using Microsoft.AspNetCore.Mvc;

namespace ProMgr.Controllers;

[ApiController]
[Route("[controller]")]
public class MonumentController : ControllerBase
{
    MonumentService _service;
    public MonumentController(MonumentService service)
    {
        _service = service;
    }

    [HttpGet]
    public IEnumerable<Monument> GetAll()  {
        return _service.GetAll();
    }
        //MonumentService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Monument> GetById(int id)
        {
            var monument = _service.GetById(id);

            if(monument is not null)
            {
                return monument;
            }
            else
            {
                return NotFound();
            }
        }

    [HttpPost]
    public IActionResult Create(int id, string name)
    {
        var monument = _service.Create(id, name);
        return NoContent();
        //return CreatedAtAction(nameof(GetById), new { id = monument!.Id }, monument);
    }
   /*  [HttpPost]
    public IActionResult Create(Monument newMonument)
    {
        var monument = _service.Create(newMonument);
        return CreatedAtAction(nameof(GetById), new { id = monument!.Id }, monument);
    } */
    [HttpPut("{id}/addepic")]
    public IActionResult AddEpic(int id, int epicId, string name)
    {
        var monumentToUpdate = _service.GetById(id);
        if(monumentToUpdate is not null) {
            _service.AddEpic(id, epicId, name);
            return NoContent();
        }            
        else {
            throw new NullReferenceException("Monument finnes ikkje");
        }
    }
    [HttpPut("{id}/addstory")]
    public IActionResult Addstory(int id, int epicId, int storyId, string name)
    {
        //if epic not in monument.Epics -> Break (service)
        var monumentToUpdate = _service.GetById(id);
        var epicToUpdate = _service.GetEpicById(epicId);
        
        if(monumentToUpdate is not null || epicToUpdate is not null) {
            _service.AddStory(id, epicId, storyId, name);
            return NoContent();
        }            
        else {
            throw new NullReferenceException("Monument eller epic finenes ikkje");
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, string name, string description, string proman)
    {
        var monumentToUpdate = _service.GetById(id);
        //Unødvendig(?) nullsjekk her
        if(monumentToUpdate is not null) {
            _service.Update(id, name, description, proman);
            return NoContent();
        }            
        else {
            throw new NullReferenceException("Monument finens ikkje");
        }
    }
    [HttpPut("{id}/updatestory")]
   public IActionResult UpdateStory(int storyid, string name, string description, string responsible, string misc)
    {

        _service.UpdateStory(storyid, name, description, responsible, misc);
        return NoContent();
    }
    [HttpPut("{id}/updateepic")]
    public IActionResult UpdateEpic(int id, int epicid, string name, string description, string misc)
    {

        _service.UpdateEpic(id, epicid, name, description, misc);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var monument = _service.GetById(id);

        if(monument is not null)
        {
            _service.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
     [HttpDelete("deleteepic")]
    public IActionResult DeleteEpic(int epicId)
    {

        _service.DeleteEpic(epicId);
        return Ok();

    }
    [HttpDelete("deletestory")]
    public IActionResult DeleteStory(int storyId)
    {
        //sjekk om monument.epics.stories er legit
        var story = _service.GetStoryById(storyId);

        if(story is not null)
        {
            _service.DeleteStory(storyId);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}