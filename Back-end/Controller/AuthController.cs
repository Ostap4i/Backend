using DENMAP_SERVER.Service;
using Nancy;
using Nancy.ModelBinding;
using DENMAP_SERVER.Controller.request;

namespace DENMAP_SERVER.Controller
{
    public class AuthController : NancyModule
    {
        private UserService _userService = new UserService();
        private const string _BASE_PATH = "/auth";

        public AuthController()
        {
            Options("/*", args =>
            {
                return new Response()
                    .WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS")
                    .WithHeader("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization")
                    .WithHeader("Access-Control-Allow-Credentials", "true");
            });

            After.AddItemToEndOfPipeline(ctx =>
            {
                ctx.Response
                    .WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS")
                    .WithHeader("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization")
                    .WithHeader("Access-Control-Allow-Credentials", "true");
            });

            Post(_BASE_PATH + "/login", args =>
            {
                UserRequest request = null;

                try
                {
                    request = this.Bind<UserRequest>();
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { message = e.Message }, HttpStatusCode.BadRequest);
                }

                try
                {
                    int userId = _userService.loginUser(request.Name, request.Password);
                    return Response.AsJson(new { success = true, userId = userId, message = "Login successful" }, HttpStatusCode.OK);
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { success = false, message = e.Message }, HttpStatusCode.BadRequest);
                }
            });

            Post(_BASE_PATH + "/register", args =>
            {
                UserRequest request = null;

                try
                {
                    request = this.Bind<UserRequest>();
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { message = e.Message }, HttpStatusCode.BadRequest);
                }

                try
                {
                    int userId = _userService.RegisterUser(request.Name, request.Password, request.Image, request.Description);
                    return Response.AsJson(new { success = true, userId = userId, message = "User registered successfully" }, HttpStatusCode.Created);
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { success = false, message = e.Message }, HttpStatusCode.BadRequest);
                }
            });
        }
    }
}