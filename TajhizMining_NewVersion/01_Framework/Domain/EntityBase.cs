﻿namespace _01_Framework.Domain
{
    public class EntityBase
    {
        public int Id { get; private set; }
        public DateTime CretionDate { get; set; }

        public EntityBase()
        {
            this.CretionDate = DateTime.Now;
        }
    }

}
