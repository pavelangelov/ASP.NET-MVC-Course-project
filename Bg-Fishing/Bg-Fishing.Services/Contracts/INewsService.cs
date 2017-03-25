﻿using Bg_Fishing.DTOs;
using Bg_Fishing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg_Fishing.Services.Contracts
{
    public interface INewsService
    {
        News FindById(string id);

        IEnumerable<NewsDTO> GetNews(int skip, int take);

        int Save();
    }
}