namespace OrderAPI.Services
{
    public class SingletonGuid : ISingletonGuid
    {
        private readonly string _guid = Guid.NewGuid().ToString();

        public string GetGuid()
        {
            return _guid;
        }
    }
}