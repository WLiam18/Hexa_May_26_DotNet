namespace OrderAPI.Services
{
    public class ScopedGuid : IScopedGuid
    {
        private readonly string _guid = Guid.NewGuid().ToString();

        public string GetGuid()
        {
            return _guid;
        }
    }
}