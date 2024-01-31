namespace Application.Shared
{
    public abstract record PagedFilter
    {
        private int _pageNumber;
        private int _pageSize;

        public int? PageNumber { get { if (_pageNumber <= 0) return 1; else return _pageNumber; } set => _pageNumber = value!.Value; }
        public int? PageLength { get { if (_pageSize <= 0) return 10; else return _pageSize; } set => _pageSize = value!.Value; } 

        public int Take() => PageLength!.Value;
        public int Skip() => (PageNumber!.Value - 1) * PageLength!.Value;
    }
}
