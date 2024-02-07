namespace Api.ResponseModule
{
    public class ApiValidationResponse : ApiExeption
    {
        public ApiValidationResponse() : base(400)
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}
