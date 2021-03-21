namespace TodoList.Extensions
{
    public static class StateExtention
    {
        public static string Caption(this State state)
        {
            switch (state)
            {
                case State.Open:
                    return Texts.State_Open;
                case State.InProgress:
                    return Texts.State_InProgress;
                case State.Done:
                    return Texts.State_Done;
                default:
                    return string.Empty;
            }
        }
    }
}