namespace CloudSaaSCodingTask.Dtos.PaginatedTable
{
    public class PaginatedTableGroupDto<T>
    {
        public string Name { get; set; }
        public List<PaginatedTableRowDto<T>> Items { get; set; }
        public bool Show { get; set; } = true;
    }
}
