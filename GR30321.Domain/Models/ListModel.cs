﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR30321.Domain.Models
{
    //класс, описывающий данные, используемые при 
    //получении и выводе на страницу списка объектов
    public class ListModel <T>
    {
        // запрошенный список объектов
        public List<T> Items { get; set; } = new();
        // номер текущей страницы
        public int CurrentPage { get; set; } = 1;
        // общее количество страниц
        public int TotalPages { get; set; } = 1;
    }
}
