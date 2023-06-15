using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GitUsers.API.Models
{
    public class ModelBinderUsers : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var bindedUsernames = bindingContext.HttpContext.Request.Query;

            var data = bindedUsernames.TryGetValue("usernames", out var usernames);

            if(data)
            {
                var listUsernames = usernames.ToList<string>();
                bindingContext.Result = ModelBindingResult.Success(listUsernames);

            }

            return Task.CompletedTask;
        }
    }
}
