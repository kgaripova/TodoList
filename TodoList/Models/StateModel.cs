namespace TodoList
{
    using Extensions;

    public class StateModel
    {
        public State State { get; set; }
        public string Caption { get; set; }

        public StateModel(State state)
        {
            this.State = state;
            this.Caption = state.Caption();
        }
    }
}