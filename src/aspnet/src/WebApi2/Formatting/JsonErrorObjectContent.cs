namespace Cobweb.Web.Http.Formatting {
    public class JsonErrorObjectContent : JsonObjectContent {
        public JsonErrorObjectContent(string message) : base(new {message}) {}
    }
}
