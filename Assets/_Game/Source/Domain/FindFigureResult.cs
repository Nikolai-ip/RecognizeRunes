namespace _Game.Source.Domain
{
    public struct FindFigureResult
    {
        public string FigureID { get; private set; }
        public bool HasFound { get; private set; }
        public float ErrorValue { get; private set; }

        public FindFigureResult(string figureID, bool hasFound, float errorValue)
        {
            FigureID = figureID;
            HasFound = hasFound;
            ErrorValue = errorValue;
        }

        public override string ToString()
        {
            return $"{FigureID}: Has found {HasFound} Error: {ErrorValue}";
        }
    }
}