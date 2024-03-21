namespace RPG_Notes.Models;

public class Note(int id, int listId, string text)
{
    public int Id { get; set; } = id;
    public int ListId { get; set; } = listId;
    public string Text { get; set; } = text;
}
