﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.DataInterfaces.Games.DTO
{
    public class GameTeamCreate
    {
        public string Name { get; set; }
        public int FieldCount { get; set; }
        public int? UserId { get; set; }
    }
}
