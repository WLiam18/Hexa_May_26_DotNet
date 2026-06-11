namespace OrderAPI.Services
{
    public class TransientGuid : ITransientGuid
    {
        private readonly string _guid = Guid.NewGuid().ToString();

        public string GetGuid()
        {
            return _guid;
        }
    }
}