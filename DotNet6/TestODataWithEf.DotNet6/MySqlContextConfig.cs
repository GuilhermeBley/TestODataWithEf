namespace TestODataWithEf.DotNet6
{
    public class MySqlContextConfig
    {
        public const string SECTION = "MySqlContext";

        public string ConnectionString { get; set; }
            = string.Empty;
    }
}
