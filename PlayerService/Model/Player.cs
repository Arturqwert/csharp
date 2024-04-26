﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerService.PlayerService.Model
{
    internal class Player
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Points { get; set; }

        public Player(int id, int points, string name)
        {
            Id = id;
            Points = points;
            Name = name;
        }

        public Player()
        {
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return $"{{ Id: {Id}, Name: {Name}, Points: {Points}.}}";
        }
    }
}
