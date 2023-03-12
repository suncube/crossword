namespace WW.Models
{
    public enum DirectionType
    {
        Rigth,
        Down
    }

    public class LinkedLetter
    {
        public bool IsEmpty { get; protected set; }

        public Letter Right { get; private set; }
        public Letter Down { get; private set; }

        public bool IsRightEmpty { get; private set; } = true;
        public bool IsDownEmpty { get; private set; } = true;

        public bool IsRightUsed { get; private set; } = false;
        public bool IsDownUsed { get; private set; } = false;

        public void SetRightLink(Letter letter)
        {
            Right = letter;
            IsRightEmpty = (Right == null || Right.IsEmpty);
        }
        public void SetDownLink(Letter letter)
        {
            Down = letter;
            IsDownEmpty = (Down == null || Down.IsEmpty);
        }

        public Letter GetNextIfCanMove(DirectionType direction)
        {
            if (IsNextEmptyOrUsed(direction))
                return null;

            if (direction == DirectionType.Down)
                return Down;

            return Right;
        }

        private bool IsNextEmptyOrUsed(DirectionType direction)
        {
            if (direction == DirectionType.Down)
                return IsDownEmpty;

            return IsRightEmpty;
        }

        public void MarkerUsedDirection(DirectionType direction)
        {
            if (direction == DirectionType.Down)
                IsDownUsed = true;
            else
                IsRightUsed = true;
        }
    }
}
