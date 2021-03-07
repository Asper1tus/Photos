namespace Photos.API.DataTransferObjects
{
    public record CreateCommentDTO
    {
        public string Text { get; init; }
    }
}
