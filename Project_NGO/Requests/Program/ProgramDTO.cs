﻿namespace Project_NGO.Requests;

public class ProgramDTO
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? Budget { get; set; }
    public string? Image { get; set; }
    public string? Status { get; set; }
    public int CategoryId { get; set; }
}