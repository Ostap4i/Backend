using DENMAP_SERVER.Controller.request;
using DENMAP_SERVER.Entity;
using DENMAP_SERVER.Entity.dto;
using DENMAP_SERVER.Service;
using Nancy;
using Nancy.ModelBinding;
using System.Text;
using System.Security.Cryptography;

namespace DENMAP_SERVER.Controller
{
    public class UserController : NancyModule
    {
        private UserService _userService = new UserService();
        private PostService _postService = new PostService();
        private CommentService _commentService = new CommentService();
        private MediaService _mediaService = new MediaService();

        private const string _BASE_PATH = "/api/v1/user";
        private const string _BASE_PATH_MEDIA = "http://34.116.253.154/media/";

        public UserController()
        {
            //Get(_basePath + "/", _ => "{\"message\": \"Hello World!\"}");

            Options("/*", args =>
            {
                return new Response()
                    .WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS")
                    .WithHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
            });

            After.AddItemToEndOfPipeline(ctx =>
            {
                ctx.Response
                    .WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS")
                    .WithHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
            });

            Get(_BASE_PATH + "/{id}", parameters =>
            {
                int id = parameters.id;

                try
                {
                    User user = _userService.GetUserById(id);
                    Console.WriteLine("user: " + user);

                    List<Comment> comments = _commentService.GetCommentsByUserId(id);
                    Console.WriteLine("comments: " + comments);

                    List<Post> posts = _postService.GetPostsByUserId(id);
                    Console.WriteLine("posts: " + posts);

                    return Response.AsJson(new UserDTO(user, comments, posts));
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { message = ex.Message }, HttpStatusCode.NotFound);
                }
            });

            Put(_BASE_PATH + "/{id}", async parameters =>
            {
                int id = parameters.id;

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
                    User user = _userService.GetUserById(id);
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { message = ex.Message }, HttpStatusCode.NotFound);
                }

                string hashedFileName = null;
                if (request.Image == null)
                {
                    var file = Request.Files.FirstOrDefault();

                    if (file == null)
                    {
                        return Response.AsJson(new { message = "File not found" }, HttpStatusCode.BadRequest);
                    }

                    hashedFileName = GenerateHash(file.Name) + Path.GetExtension(file.Name);
                    var tempFilePath = Path.Combine(Path.GetTempPath(), hashedFileName);

                    using (var fileStream = File.Create(tempFilePath))
                    {
                        await file.Value.CopyToAsync(fileStream);
                    }

                    await _mediaService.PutObjectAsync("mybucket", hashedFileName, tempFilePath, file.ContentType);
                }

                try
                {
                    int userId = _userService.UpdateUser(id, request.Name, request.Password,
                        request.Image == null ? _BASE_PATH_MEDIA + hashedFileName : request.Image,
                        request.Description);

                    return Response.AsJson(new { message = "User updated successfully", userId });
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { message = ex.Message }, HttpStatusCode.BadRequest);
                }
            });
        }
        private string GenerateHash(string fileName)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(fileName);
                var hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
