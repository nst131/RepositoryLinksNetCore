namespace LinkBL.ModelBL.TableWithUrlBL.Dto
{
    public class OutMagnifyCountOfTransitionsDtoBL
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; } = string.Empty;
        public int CountRedirection { get; set; } = 0;
    }
}
