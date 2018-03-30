using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CommonServiceLocator;
using showTracker.Model.API.Dto;

namespace showTracker.ViewModel.MainPage
{
    public class MainViewModel
    {
        public string TestLabel { get; set; } = "Testing";
        public ShowDto Show { get; set; }

        public List<ShowDto> Shows { get; set; } = new List<ShowDto>
        {
            new ShowDto
            {
                Name = "Show_Name1",
                Rating = 5.5,
                Status = "Running",
                Type = "Scripted",
                Premiered = new DateTime(2017, 1, 1)
            },
            new ShowDto
            {
                Name = "Show_Namw2",
                Rating = 3.5,
                Status = "Running",
                Type = "Scripted",
                Premiered = new DateTime(2014, 1, 1)
            },
            new ShowDto
            {
                Name = "Show_Namw3",
                Rating = 4,
                Status = "Running",
                Type = "Scripted",
                Premiered = new DateTime(2000, 1, 1)
            }
        };
        public MainViewModel()
        {
            Show = new ShowDto
            {
                Name = "Show_Name",
                Rating = 5.5,
                Status = "Running",
                Type = "Scripted",
                Premiered = new DateTime(2017, 1,1)
            };

            Shows = new List<ShowDto>(Enumerable.Repeat(Show, 15));
        }
    }
}
