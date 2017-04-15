﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Data.Services.Base;
using WordHunt.DataInterfaces.Categories.Request;

namespace WordHunt.Data.Services.Categories
{
    public interface ICategoryCreatorValidator
    {
        Task<ValidatorResult> ValidateRequest(CategoryCreateRequest request);
    }

    public class CategoryCreatorValidator : ICategoryCreatorValidator
    {
        private readonly AppDbContext context;

        public CategoryCreatorValidator(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<ValidatorResult> ValidateRequest(CategoryCreateRequest request)
        {
            if (request.LanguageId <= 0)
                return new ValidatorResult("Category must have specified language");
            if (string.IsNullOrEmpty(request.Name))
                return new ValidatorResult("Category must have a name");
            if (await CategoryExists(request.LanguageId, request.Name))
                return new ValidatorResult("Category already exists");

            return new ValidatorResult();
        }

        private async Task<bool> CategoryExists(long languageId, string name)
        {
            return await context.Categories.Where(x => x.LanguageId == languageId && x.Name.ToLower() == name.ToLower()).AnyAsync();
        }
    }
}