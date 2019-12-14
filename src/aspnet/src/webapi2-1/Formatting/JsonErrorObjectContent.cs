namespace Aranasoft.Cobweb.Http.Formatting {
    public class JsonErrorObjectContent : JsonObjectContent {
        public JsonErrorObjectContent(string message) : base(new {message}) {}
    }
}
