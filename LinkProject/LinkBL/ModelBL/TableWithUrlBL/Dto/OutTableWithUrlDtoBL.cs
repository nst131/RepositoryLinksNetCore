namespace LinkBL.ModelBL.TableWithUrlBL.Dto
{
    public class OutTableWithUrlDtoBL
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; } = string.Empty;
        public string ShortUrl { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public int CountRedirection { get; set; } = 0;
    }
}
