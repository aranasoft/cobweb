using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace Cobweb.Testing.WebApi.Extensions {
    public static class WithHttpActionContext {
        public static HttpActionContext WithBoundArguments(this HttpActionContext context) {
            var cancellationToken = new CancellationToken();
            var binder = new DefaultActionValueBinder();
            var binding = binder.GetBinding(context.ActionDescriptor);
            binding.ExecuteBindingAsync(context, cancellationToken).Wait(cancellationToken);
            return context;
        }
    }
}
