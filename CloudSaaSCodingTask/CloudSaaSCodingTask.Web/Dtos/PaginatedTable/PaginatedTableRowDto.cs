namespace CloudSaaSCodingTask.Dtos.PaginatedTable
{
    public class PaginatedTableRowDto<T>
    {
        public T Item { get; set; }
        public bool Show { get; set; } = true;
        public bool Selected { get; set; }
    }
}
