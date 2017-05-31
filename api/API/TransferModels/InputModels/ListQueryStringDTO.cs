using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.TransferModels.InputModels
{
    public class ListQueryStringDto
    {
        private const int MaxPageSize = 50; // max items per page
        private int _limit = 25; // default number of items per page
        private int _page = 1;

        public int Page {
            get => _page;
            set => _page = (value >= 1) ? value : 1;
        }

        public int Limit
        {
            get => _limit;
            set => _limit = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string Search { get; set; }
    }
}
