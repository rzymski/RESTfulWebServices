﻿namespace DB.Entities
{
    public class Message : BaseEntity
    {
        public string? Author { get; set; }
        public string? Content { get; set; }
        public int Priority { get; set; }
        public DateTime Created { get; set; }

        public override string ToString()
        {
            return $"Message {base.ToString()}";
        }
    }
}
