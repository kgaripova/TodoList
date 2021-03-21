namespace TodoList
{
    using System;
    using SQLite;
    
    public class TodoItem
    {
        public TodoItem()
        {
        }

        public TodoItem(int id, string name, string description, DateTime createdDate, State state)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.CreatedDate = createdDate;
            this.State = state;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public State State { get; set; }
    }
}