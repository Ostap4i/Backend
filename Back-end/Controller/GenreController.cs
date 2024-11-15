using DENMAP_SERVER.Entity;
using DENMAP_SERVER.Service;
using Nancy;
using Nancy.ModelBinding;
using DENMAP_SERVER.Controller.request;

namespace DENMAP_SERVER.Controller
{
    public class GenreController : NancyModule
    {
        private GenreService _genreService = new GenreService();

        private const string _BASE_PATH = "/api/v1/genre";


        public GenreController()
        {

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


            Get(_BASE_PATH + "/", _ =>
            {
                try
                {
                    List<Genre> genres = _genreService.GetAllGenres();

                    return Response.AsJson(genres);
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { Error = ex.Message }, HttpStatusCode.BadRequest);
                }
            });


            Get(_BASE_PATH + "/{id}", parameters =>
            {
                int id = parameters.id;
                try
                {
                    Genre genre = _genreService.GetGenreById(id);

                    return Response.AsJson(genre);
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { Error = ex.Message }, HttpStatusCode.BadRequest);
                }
            });

            Post(_BASE_PATH + "/", args =>
            {
                GenreRequest request = null;

                try
                {
                    request = this.Bind<GenreRequest>();
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { Error = e.Message }, HttpStatusCode.BadRequest);
                }

                try
                {
                    int id = _genreService.AddGenre(request.Name);

                    return Response.AsJson(new { genreId = id });
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { Error = e.Message }, HttpStatusCode.BadRequest);
                }
            });


        }
    }
}
