﻿using System;

namespace LearningManagementSystem.Domain
{
    public class BaseModel
    {
        public BaseModel()
        {
            DateCreated = DateTime.Now;
        }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateDeleted { get; set; }
    }
}