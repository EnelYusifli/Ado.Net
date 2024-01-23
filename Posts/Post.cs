using System.Net.Http;
using System.Text.Json;

namespace Posts;

public class Post
{
    public Post(int userId, int id, string title, string body)
    {
        this.userId = userId;
        this.id = id;
        this.title = title;
        this.body = body;
    }

    public int userId { get; set; }
    public int id { get; set; }
    public string title { get; set; }
    public string body { get; set; }
}
public static class Context
{
    public static List<Post> Posts { get; set; } = new List<Post>();
}

         