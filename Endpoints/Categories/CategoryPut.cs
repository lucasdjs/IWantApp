﻿using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/categories/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute]Guid id, CategoryDTO categoryDTO, ApplicationDbContext context)
    {
        var category = context.Categories.Where(x => x.Id == id).FirstOrDefault();

        if(category== null)
            return Results.NotFound();

        category.EditInfo(categoryDTO.Name, categoryDTO.Active);
        if(!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());

        context.SaveChanges();

        return Results.Ok();
    }
}
