namespace Infrastructure.StateMachine.Constants
{
    public class StateMachineMoveResult
    {
        public bool CanMove { get; private set; }
        public string Message { get; private set; }

        public static StateMachineMoveResult True => new StateMachineMoveResult()
        {
            CanMove = true, Message = string.Empty
        };

        public static StateMachineMoveResult False(string message)
        {
            return new StateMachineMoveResult()
            {
                CanMove = false, Message = message
            };
        }
    }
}