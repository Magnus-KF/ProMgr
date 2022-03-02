using ProMgr.Models;
using ProMgr.Data;
using Microsoft.EntityFrameworkCore;

namespace ProMgr.Services;

public class MonumentService
{
    //static ICollection<Monument> Monuments { get; }
    //static List<Epic> Epics { get; }
    //static int nextId = 3;
    private readonly MonumentContext _context;
    //private int nesteId; sett Id automatisk?
    public MonumentService(MonumentContext context)
    {
        _context = context;
    }

    public IEnumerable<Monument> GetAll() 
    {
        return _context.Monuments.AsNoTracking().ToList();
    }

    public Monument? GetById(int id)
    {
        return _context.Monuments
        .Include(p => p.Epics)
        .ThenInclude(p => p.Stories)
        .AsNoTracking()
        .SingleOrDefault(p => p.Id == id);
    }
    public Story? GetStoryById(int storyId)
    {
        return _context.Stories
        .SingleOrDefault(p => p.StoryId == storyId);
    }
    public Epic? GetEpicById(int epicId)
    {
        return _context.Epics
        .SingleOrDefault(p => p.EpicId == epicId);
    }
     public Monument? Create(int id, string name)
    {   
        
        Monument newMonument = new Monument {Id = id, Name = name};
        _context.Monuments.Add(newMonument);
        _context.SaveChanges();
        return newMonument;
    }

     public void AddEpic(int id, int epicId, string name)
    {
        var monumentToUpdate = _context.Monuments.Find(id);
        if(monumentToUpdate.Epics is null)
        {
            monumentToUpdate.Epics = new List<Epic>();
        }
        Epic newEpic = new Epic {EpicId = epicId, Name = name, Monument = monumentToUpdate };
        _context.Epics.Add(newEpic); //Unødvendig?
        monumentToUpdate.Epics.Add(newEpic);
        _context.Monuments.Update(monumentToUpdate);
        _context.SaveChanges();

    }
     public void Update(int id, string name, string description, string proman)
    {
        var monumentToUpdate = _context.Monuments.Find(id);
        if (monumentToUpdate is not null) {
            monumentToUpdate.Name = name;
            monumentToUpdate.Description = description;
            monumentToUpdate.ProjectManager = proman;
            _context.Monuments.Update(monumentToUpdate);
            _context.SaveChanges();
        }
        else {
            throw new NullReferenceException("Monument finens ikkje");
        }

    }
     public void UpdateEpic(int id, int epicid, string name, string description, string misc)
    {
        var epicToUpdate = _context.Epics.Find(epicid);
        if (epicToUpdate is not null) {
            epicToUpdate.Name = name;
            epicToUpdate.Description = description;
            epicToUpdate.Misc = misc;
            _context.Epics.Update(epicToUpdate);
            _context.SaveChanges();

        }
        else {
            throw new NullReferenceException("Epic finnes ikkje");

        }


    }
     public void UpdateStory(int storyid, string name, string description, string responsible, string misc)
    {
        var storyToUpdate = _context.Stories.Find(storyid);
        if (storyToUpdate is not null) {
            storyToUpdate.Name = name;
            storyToUpdate.Description = description;
            storyToUpdate.Misc = misc;
            storyToUpdate.Responisble = responsible;
            _context.Stories.Update(storyToUpdate);
            _context.SaveChanges();

        }
        else {
            throw new NullReferenceException("Story finens ikkje");
        }

    }
     public void AddStory(int id, int epicId, int storyId, string name)
    {
       var epicToUpdate = _context.Epics.Find(epicId);
        if(epicToUpdate.Stories is null)
        {
            epicToUpdate.Stories = new List<Story>();
        }
        Story newStory = new Story {StoryId = storyId, Name = name, Epic = epicToUpdate };
        _context.Stories.Add(newStory);
        epicToUpdate.Stories.Add(newStory);
        _context.Epics.Update(epicToUpdate);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var monumentToDelete = _context.Monuments.Find(id);
        if (monumentToDelete is not null)
    {
            _context.Monuments.Remove(monumentToDelete);
            _context.SaveChanges();

    }
    }

    public void DeleteEpic(int epicId)
    {
        // if epic not in M.epics => error
        var epicToDelete = _context.Epics.Find(epicId);
        if (epicToDelete is not null)
    {
            _context.Epics.Remove(epicToDelete);
            _context.SaveChanges();

    }
    }

    public void DeleteStory(int taskId)
    {   
        var storyToDelete = _context.Stories.Find(taskId);
        if (storyToDelete is not null)
    {
            _context.Stories.Remove(storyToDelete);
            _context.SaveChanges();

    }
    }

}