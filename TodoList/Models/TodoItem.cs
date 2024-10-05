namespace TodoApi.Models;

public class TodoItem
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public DateTime? DueDate { get; set; }
     public DateTime? CompletedDate { get; set; }
    public string? Description { get; set; }
    public bool IsComplete { get; set; }
}