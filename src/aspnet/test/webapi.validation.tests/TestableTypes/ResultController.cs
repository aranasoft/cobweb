using System.Web.Http;

namespace Aranasoft.Cobweb.Http.Validation.Tests.TestableTypes {
    public class ResultController : ApiController {
        public AnObject[] Get() {
            return new[] {new AnObject {Name = "Foo"}};
        }

        public AnObject Get(int id) {
            return new AnObject {Name = "Foo"};
        }

        public AnObject Post(AnObject item) {
            return item;
        }

        public AnObject Put(int id, AnObject item) {
            return item;
        }

        public void Delete(int id) {}
    }
}
