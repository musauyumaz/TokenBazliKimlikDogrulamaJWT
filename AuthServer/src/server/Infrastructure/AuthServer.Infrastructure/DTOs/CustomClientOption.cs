namespace AuthServer.Infrastructure.DTOs
{
    public record CustomClientOption
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public List<string> Audiences { get; set; }
    }

}
