﻿namespace WebApplication1.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Post
{
    //[DatabaseGenerated(DatabaseGeneratedOption.None)]
    //[Key]
    public int Id { get; set; }
    
    [Display(Name="Автор")]
    public string? Author { get; set; }
    [Display(Name="Заголовок")]
    public string? Title { get; set; }
    [Display(Name="Содержание")]
    public string? Content { get; set; }
    [Display(Name="Категория")]
    public string? Category { get; set; }
    [Display(Name="Картинка")]
    public string? Image { get; set; }
    public DateTime DatePublished { get; set; }
}